namespace Proiect__de_an.Models
{
    public class HomeViewModel
    {
        public List<Proiect__de_an.Core.Lab2.FactoryMethod.IProduct> OverviewProducts { get; set; } = new();

        /// <summary>Aliniat cu <see cref="OverviewProducts"/> — același nume ca în paginile Shop (ex. product-item-femei-1.jpg).</summary>
        public List<string> OverviewImageFileNames { get; set; } = new();
    }
}
