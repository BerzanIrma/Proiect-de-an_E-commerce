using Proiect__de_an.Core.Lab2.FactoryMethod;

namespace Proiect__de_an.Core.Lab6.Strategy;

public class PriceDescendingSortStrategy : IProductSortStrategy
{
    public List<IProduct> Sort(IReadOnlyList<IProduct> products) =>
        products.OrderByDescending(p => p.Price).ToList();
}
