using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Proiect__de_an.Core.Lab6.Memento;
using Proiect__de_an.Models;
using Proiect__de_an.Services;

namespace Proiect__de_an.ViewComponents;

public class CartViewComponent : ViewComponent
{
    private readonly ICartService _cart;
    private readonly CartCaretaker _cartCaretaker;
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

    public CartViewComponent(ICartService cart, CartCaretaker cartCaretaker)
    {
        _cart = cart;
        _cartCaretaker = cartCaretaker;
    }

    private static string ComputeCartReturnUrl(HttpContext context)
    {
        var path = context.Request.Path.Value ?? "";
        if (string.Equals(context.Request.Query["fragment"], "1", StringComparison.Ordinal)
            && path.Contains("GetCartFragment", StringComparison.OrdinalIgnoreCase))
        {
            var referer = context.Request.Headers["Referer"].FirstOrDefault();
            if (!string.IsNullOrEmpty(referer) && Uri.TryCreate(referer, UriKind.Absolute, out var u)
                && string.Equals(u.Host, context.Request.Host.Host, StringComparison.OrdinalIgnoreCase))
                return string.IsNullOrEmpty(u.PathAndQuery) ? "/" : u.PathAndQuery;
            return "/";
        }

        return $"{context.Request.Path}{context.Request.QueryString}";
    }

    public IViewComponentResult Invoke()
    {
        ViewData["HasCartSnapshot"] = _cartCaretaker.HasSavedSnapshot();
        ViewData["CartReturnUrl"] = ComputeCartReturnUrl(HttpContext);
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
