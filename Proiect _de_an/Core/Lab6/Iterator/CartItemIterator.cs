using Proiect__de_an.Models;

namespace Proiect__de_an.Core.Lab6.Iterator;

/// <summary>
/// Concrete Iterator: parcurgere înainte peste o listă de linii de coș.
/// </summary>
public class CartItemIterator : ICartIterator
{
    private readonly IReadOnlyList<CartItem> _items;
    private int _index;

    public CartItemIterator(IReadOnlyList<CartItem> items)
    {
        _items = items;
        _index = 0;
    }

    public bool HasNext() => _index < _items.Count;

    public CartItem Next()
    {
        if (!HasNext())
            throw new InvalidOperationException("Nu mai există linii în coș.");
        return _items[_index++];
    }
}
