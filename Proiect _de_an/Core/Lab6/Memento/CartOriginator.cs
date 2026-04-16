using Proiect__de_an.Models;
using Proiect__de_an.Services;

namespace Proiect__de_an.Core.Lab6.Memento;

/// <summary>
/// Originator: coșul curent; creează și aplică memento-uri fără a expune detaliile interne ale sesiunii.
/// Folosește <see cref="CartService"/> (subiectul real) pentru citire/scriere brută în session.
/// </summary>
public class CartOriginator
{
    private readonly CartService _cart;

    public CartOriginator(CartService cart)
    {
        _cart = cart;
    }

    public CartMemento CreateMemento()
    {
        var copy = _cart.GetCart()
            .Select(i => new CartItem
            {
                Id = i.Id,
                Name = i.Name,
                Price = i.Price,
                Quantity = i.Quantity
            })
            .ToList();
        return new CartMemento
        {
            Items = copy,
            DeliveryType = _cart.GetDeliveryType()
        };
    }

    public void RestoreMemento(CartMemento memento)
    {
        var copy = memento.Items
            .Select(i => new CartItem
            {
                Id = i.Id,
                Name = i.Name,
                Price = i.Price,
                Quantity = i.Quantity
            })
            .ToList();
        _cart.SaveCart(copy);
        _cart.SetDeliveryType(memento.DeliveryType);
    }
}
