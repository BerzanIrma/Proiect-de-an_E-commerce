using Proiect__de_an.Models;

namespace Proiect__de_an.Core.Lab6.Iterator;

/// <summary>
/// Client helper: folosește Iterator pentru a calcula subtotal și număr de articole fără foreach direct pe List.
/// </summary>
public static class CartLineSubtotalCalculator
{
    public static decimal SumLineTotals(IReadOnlyList<CartItem> items)
    {
        var aggregate = new CartLineAggregate(items);
        var it = aggregate.CreateIterator();
        decimal sum = 0;
        while (it.HasNext())
            sum += it.Next().LineTotal;
        return sum;
    }

    public static int SumQuantities(IReadOnlyList<CartItem> items)
    {
        var aggregate = new CartLineAggregate(items);
        var it = aggregate.CreateIterator();
        var n = 0;
        while (it.HasNext())
            n += it.Next().Quantity;
        return n;
    }
}
