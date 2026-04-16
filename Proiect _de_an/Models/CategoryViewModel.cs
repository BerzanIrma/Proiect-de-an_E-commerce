namespace Proiect__de_an.Models
{
    public class CategoryViewModel
    {
        public string CategoryName { get; set; } = "";
        public List<Proiect__de_an.Core.Lab2.FactoryMethod.IProduct> Products { get; set; } = new();
        /// <summary>Cheie sortare Strategy (Lab6): goală, priceAsc, priceDesc, nameAsc.</summary>
        public string? CurrentSort { get; set; }

        /// <summary>
        /// Haine femei/bărbați: <c>product-item-femei-1.jpg</c>, <c>product-item-barbati-1.jpg</c>.
        /// Accesorii pe gen: <c>product-item-acc-femei-1.jpg</c>, <c>product-item-acc-barbati-1.jpg</c>.
        /// Null = <c>product-item-{n}.jpg</c>.
        /// </summary>
        public string? ProductImageFilePrefix { get; set; }
    }
}
