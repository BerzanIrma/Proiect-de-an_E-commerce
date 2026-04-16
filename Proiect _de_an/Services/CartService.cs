using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Proiect__de_an.Core.Lab5.Flyweight;
using Proiect__de_an.Core.Lab6.Observer;
using Proiect__de_an.Models;

namespace Proiect__de_an.Services;

public class CartService : ICartService
{
    private const string CartKey = "Cart";
    private const string DeliveryKey = "DeliveryType";
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ProductFlyweightFactory _flyweightFactory;
    private readonly CartEventManager _cartEvents;

    public CartService(IHttpContextAccessor httpContextAccessor, ProductFlyweightFactory flyweightFactory, CartEventManager cartEvents)
    {
        _httpContextAccessor = httpContextAccessor;
        _flyweightFactory = flyweightFactory;
        _cartEvents = cartEvents;
    }

    private ISession Session => _httpContextAccessor.HttpContext?.Session
        ?? throw new InvalidOperationException("Session not available");

    public List<CartItem> GetCart()
    {
        var json = Session.GetString(CartKey);
        if (string.IsNullOrEmpty(json)) return new List<CartItem>();
        try
        {
            return JsonSerializer.Deserialize<List<CartItem>>(json, JsonOptions) ?? new List<CartItem>();
        }
        catch { return new List<CartItem>(); }
    }

    public void SaveCart(List<CartItem> items)
    {
        var list = items ?? new List<CartItem>();
        Session.SetString(CartKey, JsonSerializer.Serialize(list));
        NotifyCartObservers(list);
    }

    public string GetDeliveryType()
    {
        return Session.GetString(DeliveryKey) ?? "Standard";
    }

    public void SetDeliveryType(string type)
    {
        if (type is "Express" or "Standard")
        {
            Session.SetString(DeliveryKey, type);
            NotifyCartObservers();
        }
    }

    private void NotifyCartObservers(List<CartItem>? items = null)
    {
        var list = items ?? GetCart();
        var total = list.Sum(i => i.Quantity);
        _cartEvents.Notify(CartEventManager.CartChanged, new CartChangeEvent(total, GetDeliveryType()));
    }

    public void AddItem(string id, string name, decimal price, int quantity = 1)
    {
        _flyweightFactory.GetFlyweight(id, name, price); // înregistrează/partajează flyweight (intrinsic)
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

    public void RemoveProductQuantity(string productId, int quantity)
    {
        if (quantity <= 0 || string.IsNullOrEmpty(productId)) return;
        var cart = GetCart();
        var item = cart.FirstOrDefault(i => i.Id == productId);
        if (item == null) return;
        item.Quantity -= quantity;
        if (item.Quantity <= 0)
            cart.Remove(item);
        SaveCart(cart);
    }

    public CartViewModel GetCartViewModel()
    {
        var rawCart = GetCart();
        var items = new List<CartItem>();
        foreach (var line in rawCart)
        {
            var fw = _flyweightFactory.GetFlyweight(line.Id, line.Name, line.Price);
            items.Add(new CartItem
            {
                Id = fw.Id,
                Name = fw.Name,
                Price = fw.Price,
                Quantity = line.Quantity
            });
        }
        return new CartViewModel
        {
            Items = items,
            DeliveryType = GetDeliveryType(),
            FlyweightCacheSize = _flyweightFactory.GetCacheSize(),
            CartLinesCount = items.Count
        };
    }
}
