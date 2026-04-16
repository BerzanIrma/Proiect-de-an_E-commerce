using Proiect__de_an.Core.Lab2.FactoryMethod;

namespace Proiect__de_an.Core.Lab6.Strategy;

/// <summary>
/// Strategy: algoritm interschimbabil de sortare pentru lista de produse.
/// </summary>
public interface IProductSortStrategy
{
    /// <summary>Returnează o nouă listă sortată (nu modifică lista sursă).</summary>
    List<IProduct> Sort(IReadOnlyList<IProduct> products);
}
