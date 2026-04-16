using Proiect__de_an.Core.Lab2.FactoryMethod;

namespace Proiect__de_an.Core.Lab6.Strategy;

public class NameAscendingSortStrategy : IProductSortStrategy
{
    public List<IProduct> Sort(IReadOnlyList<IProduct> products) =>
        products.OrderBy(p => p.Name, StringComparer.OrdinalIgnoreCase).ToList();
}
