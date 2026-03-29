using System.Collections.Concurrent;

namespace Proiect__de_an.Core.Lab5.Flyweight;

/// <summary>
/// Clasă centralizată care gestionează reutilizarea instanțelor ProductFlyweight.
/// Pentru același Id returnează aceeași instanță (cache); reduce consumul de memorie.
/// </summary>
public class ProductFlyweightFactory
{
    private readonly ConcurrentDictionary<string, ProductFlyweight> _cache = new();

    /// <summary>
    /// Returnează flyweight-ul pentru produsul dat. Dacă există deja în cache, îl reutilizează.
    /// </summary>
    public ProductFlyweight GetFlyweight(string id, string name, decimal price)
    {
        var key = id ?? "";
        return _cache.GetOrAdd(key, _ => new ProductFlyweight(id, name, price));
    }

    /// <summary>
    /// Numărul de instanțe partajate din cache (pentru demonstrare impact Flyweight).
    /// </summary>
    public int GetCacheSize() => _cache.Count;
}
