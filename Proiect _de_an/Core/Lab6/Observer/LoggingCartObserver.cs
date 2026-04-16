using Microsoft.Extensions.Logging;

namespace Proiect__de_an.Core.Lab6.Observer;

public class LoggingCartObserver : ICartObserver
{
    private readonly ILogger<LoggingCartObserver> _logger;

    public LoggingCartObserver(ILogger<LoggingCartObserver> logger)
    {
        _logger = logger;
    }

    public void Update(CartChangeEvent data)
    {
        _logger.LogInformation(
            "[Observer][Cart] articole totale: {Total}, livrare: {Delivery}",
            data.TotalItems, data.DeliveryType);
    }
}
