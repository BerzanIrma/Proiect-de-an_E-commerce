namespace Proiect__de_an.Core.Lab2.FactoryMethod
{
    public class PhysicalProduct : IProduct
    {
        public string Id { get; }
        public string Name { get; }
        public decimal Price { get; }
        public string Category { get; }

        public PhysicalProduct(string id, string name, decimal price, string category)
        {
            Id = id;
            Name = name;
            Price = price;
            Category = category;
        }

        public string GetDescription() => $"{Name} - {Price:C} ({Category})";
    }
}
