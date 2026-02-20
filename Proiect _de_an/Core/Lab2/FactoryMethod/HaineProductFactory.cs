namespace Proiect__de_an.Core.Lab2.FactoryMethod
{
    public class HaineProductFactory : ProductFactoryBase
    {
        public override IProduct CreateProduct(string id, string name, decimal price)
            => new PhysicalProduct(id, name, price, Categorii.Haine);
    }
}
