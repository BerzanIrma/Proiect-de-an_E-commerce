namespace Proiect__de_an.Core.Lab2.FactoryMethod
{
    public interface IProduct
    {
        string Id { get; }
        string Name { get; }
        decimal Price { get; }
        string Category { get; }
        string GetDescription();
    }
}
