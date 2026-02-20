namespace Proiect__de_an.Models
{
    public class CategoryViewModel
    {
        public string CategoryName { get; set; } = "";
        public List<Proiect__de_an.Core.Lab2.FactoryMethod.IProduct> Products { get; set; } = new();
    }
}
