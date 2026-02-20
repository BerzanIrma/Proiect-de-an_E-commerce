using System.Globalization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Proiect__de_an.Models;
using Proiect__de_an.Services;

namespace Proiect__de_an.Controllers;

public class CartController : Controller
{
    private readonly CartService _cart;

    public CartController(CartService cart)
    {
        _cart = cart;
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
        _cart.AddItem(id, name, priceValue, qty);
        TempData["CartMessage"] = $"{name} a fost adăugat în coș.";
        TempData["CartPreview"] = JsonSerializer.Serialize(_cart.GetCartViewModel());
        return Redirect(string.IsNullOrEmpty(returnUrl) ? Url.Action("Index", "Home") ?? "/" : returnUrl);
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
        _cart.SetDeliveryType(deliveryType);
        return Redirect(returnUrl ?? (Request.Headers.Referer.FirstOrDefault() ?? "/"));
    }

    public IActionResult Index()
    {
        return View(_cart.GetCartViewModel());
    }

    public IActionResult Checkout()
    {
        var vm = _cart.GetCartViewModel();
        if (vm.TotalItems == 0)
            return RedirectToAction(nameof(Index));
        return View(vm);
    }
}
