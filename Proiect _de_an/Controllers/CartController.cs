using System.Globalization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Proiect__de_an.Core.Lab2.AbstractFactory;
using Proiect__de_an.Core.Lab5.Decorator;
using Proiect__de_an.Core.Lab5.Bridge;
using Proiect__de_an.Core.Lab4.Facade;
using Proiect__de_an.Models;
using Proiect__de_an.Services;

namespace Proiect__de_an.Controllers;

public class CartController : Controller
{
    private readonly ICartService _cart;
    private readonly ECommerceFacade _facade;

    public CartController(ICartService cart, ECommerceFacade facade)
    {
        _cart = cart;
        _facade = facade;
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
        else if (!User.IsInRole("Admin") && qty > 10)
        {
            TempData["CartWarning"] = "Cantitatea maximă per produs este 10 pentru conturi standard.";
        }
        _cart.AddItem(id, name, priceValue, qty);
        var vm = _cart.GetCartViewModel();
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            return Json(new { success = true, totalItems = vm.TotalItems, message = $"{name} a fost adăugat în coș." });
        }
        TempData["CartMessage"] = $"{name} a fost adăugat în coș.";
        TempData["CartPreview"] = JsonSerializer.Serialize(vm);
        return Redirect(string.IsNullOrEmpty(returnUrl) ? Url.Action("Index", "Home") ?? "/" : returnUrl);
    }

    [HttpGet]
    public IActionResult GetCartFragment()
    {
        return ViewComponent("Cart");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int index, string? returnUrl)
    {
        _cart.RemoveAt(index);
        return Redirect(returnUrl ?? (Request.Headers.Referer.FirstOrDefault() ?? "/"));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SetDelivery(string deliveryType, string? returnUrl)
    {
        if (string.Equals(deliveryType, "Express", StringComparison.OrdinalIgnoreCase)
            && !(User.Identity?.IsAuthenticated ?? false))
        {
            TempData["CartWarning"] = "Livrarea Express este disponibilă doar pentru utilizatori autentificați.";
            var backUrl = returnUrl ?? (Request.Headers.Referer.FirstOrDefault() ?? "/");
            return RedirectToAction("Login", "Account", new { returnUrl = backUrl });
        }

        _cart.SetDeliveryType(deliveryType);
        var effectiveType = _cart.GetCartViewModel().DeliveryType;
        if (string.Equals(deliveryType, "Express", StringComparison.OrdinalIgnoreCase)
            && !string.Equals(effectiveType, "Express", StringComparison.OrdinalIgnoreCase))
        {
            TempData["CartWarning"] = "Livrarea Express este disponibilă doar pentru utilizatori autentificați. S-a păstrat Standard.";
        }
        return Redirect(returnUrl ?? (Request.Headers.Referer.FirstOrDefault() ?? "/"));
    }

    public IActionResult Index()
    {
        return View(_cart.GetCartViewModel());
    }

    [HttpGet]
    public IActionResult Checkout()
    {
        var vm = _cart.GetCartViewModel();
        if (vm.TotalItems == 0)
            return RedirectToAction(nameof(Index));
        vm.FinalTotal = vm.Total;
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Checkout(bool giftWrap = false, bool applyDiscount = false, string paymentMethod = "Card")
    {
        var vm = _cart.GetCartViewModel();
        if (vm.TotalItems == 0)
            return RedirectToAction(nameof(Index));

        IOrder order = vm.DeliveryType == "Express"
            ? _facade.CreateExpressOrder(vm.Subtotal)
            : _facade.CreateStandardOrder(vm.Subtotal);

        if (applyDiscount)
            order = new DiscountOrderDecorator(order);
        if (giftWrap)
        {
            vm.GiftWrapFee = 5m;
            order = _facade.CreateOrderWithGiftWrap(order, vm.GiftWrapFee);
        }

        vm.GiftWrapRequested = giftWrap;
        vm.DiscountApplied = applyDiscount;
        vm.FinalTotal = order.Total;

        // Bridge (Lab5): abstracția Payment + implementare IPaymentProcessor (Card / PayPal).
        Proiect__de_an.Core.Lab5.Bridge.IPaymentProcessor processor = paymentMethod == "PayPal"
            ? new PayPalPaymentProcessor()
            : new Proiect__de_an.Core.Lab5.Bridge.CardPaymentProcessor();

        Payment payment = new OnlineOrderPayment(order, processor);
        ViewBag.PaymentMessage = payment.Execute();
        ViewBag.PaymentProcessorName = processor.Name;

        return View(vm);
    }
}
