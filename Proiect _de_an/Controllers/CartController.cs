using System.Globalization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Proiect__de_an.Core.Lab7.Mediator;
using Proiect__de_an.Core.Lab7.State;
using Proiect__de_an.Core.Lab6.Command;
using Proiect__de_an.Core.Lab6.Memento;
using Proiect__de_an.Models;
using Proiect__de_an.Services;

namespace Proiect__de_an.Controllers;

public class CartController : Controller
{
    private const string CheckoutStateSessionKey = "CheckoutOrderState";
    private readonly ICartService _cart;
    private readonly ICheckoutMediator _checkoutMediator;
    private readonly CartOriginator _cartOriginator;
    private readonly CartCaretaker _cartCaretaker;
    private readonly CartCommandInvoker _cartCommands;

    public CartController(ICartService cart, ICheckoutMediator checkoutMediator, CartOriginator cartOriginator, CartCaretaker cartCaretaker, CartCommandInvoker cartCommands)
    {
        _cart = cart;
        _checkoutMediator = checkoutMediator;
        _cartOriginator = cartOriginator;
        _cartCaretaker = cartCaretaker;
        _cartCommands = cartCommands;
    }

    /// <summary>După POST din coșul încărcat prin fetch, returnUrl nu trebuie să rămână GetCartFragment?fragment=1.</summary>
    private static bool IsCartFragmentFetchUrl(string? url) =>
        !string.IsNullOrEmpty(url)
        && url.Contains("GetCartFragment", StringComparison.OrdinalIgnoreCase)
        && (url.Contains("fragment=1", StringComparison.OrdinalIgnoreCase) || url.Contains("fragment%3D1", StringComparison.OrdinalIgnoreCase));

    private string? SafeRefererPath()
    {
        var referer = Request.Headers.Referer.FirstOrDefault();
        if (string.IsNullOrEmpty(referer) || !Uri.TryCreate(referer, UriKind.Absolute, out var u)) return null;
        if (!string.Equals(u.Host, Request.Host.Host, StringComparison.OrdinalIgnoreCase)) return null;
        return string.IsNullOrEmpty(u.PathAndQuery) ? "/" : u.PathAndQuery;
    }

    private string RedirectAfterCartPost(string? returnUrl, string ifMissingUrl)
    {
        if (IsCartFragmentFetchUrl(returnUrl))
            return SafeRefererPath() ?? "/";
        return string.IsNullOrEmpty(returnUrl) ? ifMissingUrl : returnUrl;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddToCart(string id, string name, string price, int quantity, string? returnUrl)
    {
        if (string.IsNullOrWhiteSpace(id)) id = Guid.NewGuid().ToString("N")[..8];
        if (string.IsNullOrWhiteSpace(name)) name = "Produs";
        if (!decimal.TryParse(price, NumberStyles.Any, CultureInfo.InvariantCulture, out var priceValue))
            decimal.TryParse(price?.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out priceValue);
        var qty = quantity < 1 ? 1 : quantity;
        if (priceValue < 0)
        {
            TempData["CartError"] = "Produsul are un preț invalid și nu a fost adăugat în coș.";
        }
        else
        {
            if (!User.IsInRole("Admin") && qty > 10)
                TempData["CartWarning"] = "Cantitatea maximă per produs este 10 pentru conturi standard.";
            _cartCommands.Run(new AddToCartCommand(id, name, priceValue, qty));
            ResetCheckoutState();
        }
        var vm = _cart.GetCartViewModel();
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            return Json(new { success = true, totalItems = vm.TotalItems, message = $"{name} a fost adăugat în coș." });
        }
        TempData["CartMessage"] = $"{name} a fost adăugat în coș.";
        TempData["CartPreview"] = JsonSerializer.Serialize(vm);
        return Redirect(string.IsNullOrEmpty(returnUrl) ? Url.Action("Index", "Home") ?? "/" : returnUrl);
    }

    /// <summary>
    /// Pagină completă (cu _Layout) la navigare directă; pentru actualizarea offcanvas-ului folosește ?fragment=1 (doar HTML-ul componentei).
    /// </summary>
    [HttpGet]
    public IActionResult GetCartFragment()
    {
        if (string.Equals(Request.Query["fragment"], "1", StringComparison.Ordinal))
            return ViewComponent("Cart");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int index, string? returnUrl)
    {
        _cartCommands.Run(new RemoveCartItemCommand(index));
        ResetCheckoutState();
        var back = RedirectAfterCartPost(returnUrl, SafeRefererPath() ?? "/");
        return Redirect(back);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SetDelivery(string deliveryType, string? returnUrl)
    {
        if (string.Equals(deliveryType, "Express", StringComparison.OrdinalIgnoreCase)
            && !(User.Identity?.IsAuthenticated ?? false))
        {
            TempData["CartWarning"] = "Livrarea Express este disponibilă doar pentru utilizatori autentificați.";
            var backUrl = RedirectAfterCartPost(returnUrl, SafeRefererPath() ?? "/");
            return RedirectToAction("Login", "Account", new { returnUrl = backUrl });
        }

        _cartCommands.Run(new SetDeliveryCommand(deliveryType));
        ResetCheckoutState();
        var effectiveType = _cart.GetCartViewModel().DeliveryType;
        if (string.Equals(deliveryType, "Express", StringComparison.OrdinalIgnoreCase)
            && !string.Equals(effectiveType, "Express", StringComparison.OrdinalIgnoreCase))
        {
            TempData["CartWarning"] = "Livrarea Express este disponibilă doar pentru utilizatori autentificați. S-a păstrat Standard.";
        }
        return Redirect(RedirectAfterCartPost(returnUrl, SafeRefererPath() ?? "/"));
    }

    public IActionResult Index()
    {
        var vm = _cart.GetCartViewModel();
        ViewBag.HasCartSnapshot = _cartCaretaker.HasSavedSnapshot();
        ViewBag.CanUndoCartCommand = _cartCommands.CanUndo;
        return View(vm);
    }

    /// <summary>Command: anulează ultima operație pe coș (Undo din stiva persistată în sesiune).</summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UndoLastCartCommand(string? returnUrl)
    {
        if (_cartCommands.TryUndoLast())
            TempData["CartMessage"] = "Ultima operație pe coș a fost anulată (Command — Undo).";
        else
            TempData["CartWarning"] = "Nu există operație de anulat.";
        return Redirect(RedirectAfterCartPost(returnUrl, Url.Action(nameof(Index), "Cart") ?? "/"));
    }

    /// <summary>Memento: salvează snapshot-ul coșului în session (Caretaker).</summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SaveCartSnapshot(string? returnUrl)
    {
        _cartCaretaker.SaveMemento(_cartOriginator.CreateMemento());
        TempData["CartMessage"] = "Starea coșului a fost salvată (Memento). Poți restaura mai târziu.";
        return Redirect(RedirectAfterCartPost(returnUrl, Url.Action(nameof(Index), "Cart") ?? "/"));
    }

    /// <summary>Memento: restaurează coșul din snapshot-ul salvat.</summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult RestoreCartSnapshot(string? returnUrl)
    {
        var memento = _cartCaretaker.LoadMemento();
        if (memento == null)
        {
            TempData["CartError"] = "Nu există un snapshot salvat.";
            return Redirect(RedirectAfterCartPost(returnUrl, Url.Action(nameof(Index), "Cart") ?? "/"));
        }
        _cartOriginator.RestoreMemento(memento);
        TempData["CartMessage"] = "Coșul a fost restaurat la starea salvată (Memento).";
        return Redirect(RedirectAfterCartPost(returnUrl, Url.Action(nameof(Index), "Cart") ?? "/"));
    }

    [HttpGet]
    public IActionResult Checkout()
    {
        var vm = _cart.GetCartViewModel();
        if (vm.TotalItems == 0)
            return RedirectToAction(nameof(Index));

        var orderState = new OrderStateContext(GetCheckoutStateName());
        SaveCheckoutStateName(orderState.CurrentStateName);

        vm.FinalTotal = vm.Total;
        ViewBag.SelectedPaymentMethod = "Card";
        ViewBag.OrderPaymentStatus = GetPaymentStatusLabel(orderState.CurrentStateName);
        ViewBag.OrderPaymentStatusClass = GetPaymentStatusClass(orderState.CurrentStateName);
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Checkout(
        bool giftWrap = false,
        bool applyDiscount = false,
        string paymentMethod = "Card",
        string actionType = "update",
        string? cardHolder = null,
        string? cardNumber = null,
        string? cardExpiry = null,
        string? cardCvv = null)
    {
        var vm = _cart.GetCartViewModel();
        if (vm.TotalItems == 0)
            return RedirectToAction(nameof(Index));

        ViewBag.SelectedPaymentMethod = paymentMethod;
        ViewBag.CardHolder = cardHolder;
        ViewBag.CardNumber = cardNumber;
        ViewBag.CardExpiry = cardExpiry;
        ViewBag.CardCvv = cardCvv;

        vm.GiftWrapRequested = giftWrap;
        vm.DiscountApplied = applyDiscount;
        vm.GiftWrapFee = giftWrap ? vm.GiftWrapFee : 0m;

        var mediatorResult = _checkoutMediator.Handle(new CheckoutMediatorRequest
        {
            Subtotal = vm.Subtotal,
            DeliveryCost = vm.DeliveryCost,
            DeliveryType = vm.DeliveryType,
            GiftWrapRequested = giftWrap,
            GiftWrapFee = vm.GiftWrapFee,
            DiscountApplied = applyDiscount,
            PaymentMethod = paymentMethod,
            ActionType = actionType,
            CurrentOrderStateName = GetCheckoutStateName(),
            CardHolder = cardHolder,
            CardNumber = cardNumber,
            CardExpiry = cardExpiry,
            CardCvv = cardCvv
        });

        SaveCheckoutStateName(mediatorResult.NextOrderStateName);
        vm.GiftWrapFee = giftWrap ? mediatorResult.GiftWrapFeeApplied : 0m;
        vm.FinalTotal = mediatorResult.FinalTotal;
        ViewBag.PaymentMessage = mediatorResult.PaymentMessage;
        ViewBag.PaymentError = mediatorResult.PaymentError;
        ViewBag.PaymentProcessorName = mediatorResult.PaymentProcessorName;
        ViewBag.CheckoutTemplateName = mediatorResult.CheckoutTemplateName;
        ViewBag.OrderPaymentStatus = GetPaymentStatusLabel(mediatorResult.NextOrderStateName);
        ViewBag.OrderPaymentStatusClass = GetPaymentStatusClass(mediatorResult.NextOrderStateName);

        return View(vm);
    }

    private string GetCheckoutStateName()
    {
        return HttpContext.Session.GetString(CheckoutStateSessionKey) ?? "CartOpen";
    }

    private void SaveCheckoutStateName(string stateName)
    {
        HttpContext.Session.SetString(CheckoutStateSessionKey, stateName);
    }

    private void ResetCheckoutState()
    {
        SaveCheckoutStateName("CartOpen");
    }

    private static string GetPaymentStatusLabel(string stateName)
    {
        return stateName switch
        {
            "Paid" or "Shipped" => "Achitat",
            "Cancelled" => "Anulată",
            _ => "Neachitat"
        };
    }

    private static string GetPaymentStatusClass(string stateName)
    {
        return stateName switch
        {
            "Paid" or "Shipped" => "success",
            "Cancelled" => "danger",
            _ => "secondary"
        };
    }
}
