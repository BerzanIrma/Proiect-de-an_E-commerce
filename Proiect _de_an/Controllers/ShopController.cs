using Microsoft.AspNetCore.Mvc;
using Proiect__de_an.Core.Lab2.FactoryMethod;
using Proiect__de_an.Core.Lab6.Strategy;
using Proiect__de_an.Models;

namespace Proiect__de_an.Controllers
{
    public class ShopController : Controller
    {
        private readonly ProductSortStrategyFactory _sortFactory;

        public ShopController(ProductSortStrategyFactory sortFactory)
        {
            _sortFactory = sortFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        private CategoryViewModel BuildCategory(string categoryName, List<IProduct> products, string? sort, string? productImageFilePrefix = null)
        {
            var strategy = _sortFactory.GetStrategy(sort);
            var sorted = strategy.Sort(products);
            return new CategoryViewModel
            {
                CategoryName = categoryName,
                Products = sorted,
                CurrentSort = sort,
                ProductImageFilePrefix = productImageFilePrefix
            };
        }

        public IActionResult Haine(string? sort = null)
        {
            var factory = new HaineProductFactory();
            var products = new List<IProduct>
            {
                factory.CreateProduct("1", "Bluză", 89m),
                factory.CreateProduct("2", "Pantaloni", 120m),
                factory.CreateProduct("3", "Rochie", 159m),
                factory.CreateProduct("4", "Tricou", 45m),
                factory.CreateProduct("5", "Geacă", 199m)
            };
            return View("Category", BuildCategory(Categorii.Haine, products, sort));
        }

        public IActionResult Accesorii(string? sort = null)
        {
            var factory = new AccesoriiProductFactory();
            var products = new List<IProduct>
            {
                factory.CreateProduct("1", "Curea", 75m),
                factory.CreateProduct("2", "Gentă", 129m),
                factory.CreateProduct("3", "Eșarfă", 55m),
                factory.CreateProduct("4", "Pandantiv", 45m),
                factory.CreateProduct("5", "Căciulă", 89m)
            };
            return View("Category", BuildCategory(Categorii.Accesorii, products, sort));
        }

        public IActionResult HaineBarbati(string? sort = null)
        {
            var factory = new HaineProductFactory();
            var products = new List<IProduct>
            {
                factory.CreateProduct("1", "Camasa", 99m),
                factory.CreateProduct("2", "Pantaloni barbati", 130m),
                factory.CreateProduct("3", "Blazer", 229m),
                factory.CreateProduct("4", "Tricou", 49m),
                factory.CreateProduct("5", "Geaca", 189m)
            };
            return View("Category", BuildCategory("Haine Bărbați", products, sort, "barbati"));
        }

        public IActionResult HaineFemei(string? sort = null)
        {
            var factory = new HaineProductFactory();
            var products = new List<IProduct>
            {
                factory.CreateProduct("1", "Bluză", 89m),
                factory.CreateProduct("2", "Pantaloni", 120m),
                factory.CreateProduct("3", "Rochie", 159m),
                factory.CreateProduct("4", "Tricou", 45m),
                factory.CreateProduct("5", "Geacă", 199m)
            };
            return View("Category", BuildCategory("Haine Femei", products, sort, "femei"));
        }

        public IActionResult AccesoriiBarbati(string? sort = null)
        {
            var factory = new AccesoriiProductFactory();
            var products = new List<IProduct>
            {
                factory.CreateProduct("1", "Geantă Eleganța", 129m),
                factory.CreateProduct("2", "Geantă Chic Neagră", 149m),
                factory.CreateProduct("3", "Geantă Crossbody", 99m),
                factory.CreateProduct("4", "Rucsac Modern", 189m),
                factory.CreateProduct("5", "Geantă de Seară", 159m)
            };
            return View("Category", BuildCategory("Genti", products, sort, "acc-barbati"));
        }

        public IActionResult AccesoriiFemei(string? sort = null)
        {
            var factory = new AccesoriiProductFactory();
            var products = new List<IProduct>
            {
                factory.CreateProduct("1", "Eșarfă Elegantă Mătase", 55m),
                factory.CreateProduct("2", "Eșarfă Casual", 65m),
                factory.CreateProduct("3", "Eșarfă Florală", 45m),
                factory.CreateProduct("4", "Eșarfă Oversized", 49m),
                factory.CreateProduct("5", "Eșarfă lungă imprimeu", 59m)
            };
            return View("Category", BuildCategory("Eșarfe", products, sort, "acc-femei"));
        }
    }
}
