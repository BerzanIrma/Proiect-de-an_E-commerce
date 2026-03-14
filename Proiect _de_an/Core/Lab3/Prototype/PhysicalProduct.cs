namespace Proiect__de_an.Core.Lab3.Prototype
{

    public class PhysicalProduct : IPrototype
    {
        private readonly string id;
        private readonly string name;
        private readonly decimal price;
        private readonly string category;

        public PhysicalProduct(string id, string name, decimal price, string category)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.category = category;
        }

        public IPrototype Clone()
        {
            return new PhysicalProduct(id, name, price, category);
        }
    }
}
