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
            haineFactory.CreateProduct("3", "Rochie", 159m),
            haineFactory.CreateProduct("4", "Tricou", 45m),
            accesoriiFactory.CreateProduct("5", "Curea", 75m),
            accesoriiFactory.CreateProduct("6", "Gentă", 129m),
            accesoriiFactory.CreateProduct("7", "Eșarfă", 55m),
            accesoriiFactory.CreateProduct("8", "Pandantiv", 45m)
        };
        return View(new HomeViewModel { OverviewProducts = overviewProducts });
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
