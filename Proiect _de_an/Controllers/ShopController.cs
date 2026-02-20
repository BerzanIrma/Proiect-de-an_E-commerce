using Microsoft.AspNetCore.Mvc;
using Proiect__de_an.Core.Lab2.FactoryMethod;
using Proiect__de_an.Models;

namespace Proiect__de_an.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Haine()
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
            return View("Category", new CategoryViewModel { CategoryName = Categorii.Haine, Products = products });
        }

        public IActionResult Accesorii()
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
            return View("Category", new CategoryViewModel { CategoryName = Categorii.Accesorii, Products = products });
        }

        public IActionResult HaineBarbati()
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
            return View("Category", new CategoryViewModel { CategoryName = "Haine Bărbați", Products = products });
        }

        public IActionResult HaineFemei()
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
            return View("Category", new CategoryViewModel { CategoryName = "Haine Femei", Products = products });
        }

        public IActionResult AccesoriiBarbati()
        {
            var factory = new AccesoriiProductFactory();
            var products = new List<IProduct>
            {
                factory.CreateProduct("1", "Curea", 75m),
                factory.CreateProduct("2", "Portofel", 89m),
                factory.CreateProduct("3", "Ceas", 299m),
                factory.CreateProduct("4", "Ochelari soare", 120m),
                factory.CreateProduct("5", "Eșarfă", 55m)
            };
            return View("Category", new CategoryViewModel { CategoryName = "Accesorii Bărbați", Products = products });
        }

        public IActionResult AccesoriiFemei()
        {
            var factory = new AccesoriiProductFactory();
            var products = new List<IProduct>
            {
                factory.CreateProduct("1", "Gentă", 129m),
                factory.CreateProduct("2", "Eșarfă", 55m),
                factory.CreateProduct("3", "Pandantiv", 45m),
                factory.CreateProduct("4", "Căciulă", 89m),
                factory.CreateProduct("5", "Colier", 65m)
            };
            return View("Category", new CategoryViewModel { CategoryName = "Accesorii Femei", Products = products });
        }
    }
}
