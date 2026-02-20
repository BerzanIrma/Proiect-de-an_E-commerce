using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Proiect__de_an.Models;

namespace Proiect__de_an.Services;

public class CartService
{
    private const string CookieName = "StylishCart";
    private const int CookieMaxAgeDays = 30;
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private HttpContext Context => _httpContextAccessor.HttpContext
        ?? throw new InvalidOperationException("HttpContext not available");

    private static string Encode(string json)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
    }

    private static string? Decode(string? encoded)
    {
        if (string.IsNullOrWhiteSpace(encoded)) return null;
        try
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(encoded));
        }
        catch { return null; }
    }

    private CookieCartData GetData()
    {
        var raw = Context.Request.Cookies[CookieName];
        var json = Decode(raw);
        if (string.IsNullOrEmpty(json)) return new CookieCartData();
        try
        {
            var data = JsonSerializer.Deserialize<CookieCartData>(json, JsonOptions);
            return data ?? new CookieCartData();
        }
        catch { return new CookieCartData(); }
    }

    private void SetData(CookieCartData data)
    {
        var json = JsonSerializer.Serialize(data, JsonOptions);
        var value = Encode(json);
        Context.Response.Cookies.Append(CookieName, value, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Lax,
            Secure = Context.Request.IsHttps,
            Path = "/",
            MaxAge = TimeSpan.FromDays(CookieMaxAgeDays),
            IsEssential = true
        });
    }

    public List<CartItem> GetCart()
    {
        return GetData().Items ?? new List<CartItem>();
    }

    public void SaveCart(List<CartItem> items)
    {
        var data = GetData();
        data.Items = items ?? new List<CartItem>();
        SetData(data);
    }

    public string GetDeliveryType()
    {
        return GetData().DeliveryType ?? "Standard";
    }

    public void SetDeliveryType(string type)
    {
        if (type is not "Express" and not "Standard") return;
        var data = GetData();
        data.DeliveryType = type;
        SetData(data);
    }

    public void AddItem(string id, string name, decimal price, int quantity = 1)
    {
        var cart = GetCart();
        var existing = cart.FirstOrDefault(i => i.Id == id);
        if (existing != null)
            existing.Quantity += quantity;
        else
            cart.Add(new CartItem { Id = id, Name = name, Price = price, Quantity = quantity });
        SaveCart(cart);
    }

    public void RemoveAt(int index)
    {
        var cart = GetCart();
        if (index >= 0 && index < cart.Count)
        {
            cart.RemoveAt(index);
            SaveCart(cart);
        }
    }

    public CartViewModel GetCartViewModel()
    {
        return new CartViewModel
        {
            Items = GetCart(),
            DeliveryType = GetDeliveryType()
        };
    }

    private class CookieCartData
    {
        public List<CartItem>? Items { get; set; }
        public string? DeliveryType { get; set; }
    }
}
