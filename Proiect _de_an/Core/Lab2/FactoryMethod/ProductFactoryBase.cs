namespace Proiect__de_an.Core.Lab2.FactoryMethod
{
    public abstract class ProductFactoryBase
    {
        public abstract IProduct CreateProduct(string id, string name, decimal price);
    }
}
