using Proiect__de_an.Core.Lab4.Facade;

namespace Proiect__de_an.Core.Lab7.TemplateMethod;

/// <summary>
/// Selectează implementarea concretă de checkout în funcție de tipul livrării.
/// </summary>
public class CheckoutTemplateFactory
{
    private readonly ECommerceFacade _facade;

    public CheckoutTemplateFactory(ECommerceFacade facade)
    {
        _facade = facade;
    }

    public ICheckoutTemplate GetTemplate(string? deliveryType)
    {
        return string.Equals(deliveryType, "Express", StringComparison.OrdinalIgnoreCase)
            ? new ExpressCheckoutTemplate(_facade)
            : new StandardCheckoutTemplate(_facade);
    }
}
