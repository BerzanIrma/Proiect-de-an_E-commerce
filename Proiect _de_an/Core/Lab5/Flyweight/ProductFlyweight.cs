namespace Proiect__de_an.Core.Lab5.Flyweight;

/// <summary>
/// Flyweight: starea intrinsică (partajată) a unui produs.
/// Id, Name, Price sunt comune tuturor liniilor care referă același produs.
/// </summary>
public class ProductFlyweight
{
    public string Id { get; }
    public string Name { get; }
    public decimal Price { get; }

    public ProductFlyweight(string id, string name, decimal price)
    {
        Id = id ?? "";
        Name = name ?? "";
        Price = price;
    }
}
