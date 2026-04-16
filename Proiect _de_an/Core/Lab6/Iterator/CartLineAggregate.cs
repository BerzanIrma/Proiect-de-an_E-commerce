using Proiect__de_an.Models;

namespace Proiect__de_an.Core.Lab6.Iterator;

/// <summary>
/// Concrete Aggregate: încapsulează lista de linii și creează iteratorul corespunzător.
/// </summary>
public class CartLineAggregate : ICartLineAggregate
{
    private readonly IReadOnlyList<CartItem> _items;

    public CartLineAggregate(IReadOnlyList<CartItem> items)
    {
        _items = items ?? Array.Empty<CartItem>();
    }

    public ICartIterator CreateIterator() => new CartItemIterator(_items);
}
