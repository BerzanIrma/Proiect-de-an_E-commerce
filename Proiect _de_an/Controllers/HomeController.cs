using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Proiect__de_an.Models;
using Proiect__de_an.Core.Lab2.FactoryMethod;

namespace Proiect__de_an.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var haineFactory = new HaineProductFactory();
        var accesoriiFactory = new AccesoriiProductFactory();
        var overviewProducts = new List<IProduct>
        {
            haineFactory.CreateProduct("1", "Bluză", 89m),
            haineFactory.CreateProduct("2", "Pantaloni", 120m),
            haineFactory.CreateProduct("3", "Camasa", 99m),
            haineFactory.CreateProduct("4", "Pantaloni barbati", 130m),
            accesoriiFactory.CreateProduct("5", "Geantă Eleganța", 129m),
            accesoriiFactory.CreateProduct("6", "Geantă Chic Neagră", 149m),
            accesoriiFactory.CreateProduct("7", "Eșarfă Elegantă Mătase", 55m),
            accesoriiFactory.CreateProduct("8", "Eșarfă Casual", 65m)
        };
        var overviewImages = new List<string>
        {
            "product-item-femei-1.jpg",
            "product-item-femei-2.jpg",
            "product-item-barbati-1.jpg",
            "product-item-barbati-2.jpg",
            "product-item-acc-barbati-1.jpg",
            "product-item-acc-barbati-2.jpg",
            "product-item-acc-femei-1.jpg",
            "product-item-acc-femei-2.jpg"
        };
        return View(new HomeViewModel
        {
            OverviewProducts = overviewProducts,
            OverviewImageFileNames = overviewImages
        });
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
