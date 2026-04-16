using Proiect__de_an.Core.Lab2.FactoryMethod;

namespace Proiect__de_an.Core.Lab6.Strategy;

/// <summary>Ordinea inițială (fără sortare suplimentară).</summary>
public class DefaultOrderSortStrategy : IProductSortStrategy
{
    public List<IProduct> Sort(IReadOnlyList<IProduct> products) => products.ToList();
}
