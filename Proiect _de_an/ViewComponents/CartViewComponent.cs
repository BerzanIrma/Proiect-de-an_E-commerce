using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Proiect__de_an.Models;
using Proiect__de_an.Services;

namespace Proiect__de_an.ViewComponents;

public class CartViewComponent : ViewComponent
{
    private readonly CartService _cart;
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

    public CartViewComponent(CartService cart)
    {
        _cart = cart;
    }

    public IViewComponentResult Invoke()
    {
        if (ViewContext.TempData["CartPreview"] is string json)
        {
            try
            {
                var vm = JsonSerializer.Deserialize<CartViewModel>(json, JsonOptions);
                if (vm != null) return View(vm);
            }
            catch { /* fall through to cookie */ }
        }
        return View(_cart.GetCartViewModel());
    }
}
