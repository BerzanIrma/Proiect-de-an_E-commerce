using Proiect__de_an.Core.Lab2.AbstractFactory;
using Proiect__de_an.Core.Lab4.Facade;

namespace Proiect__de_an.Core.Lab7.TemplateMethod;

public class StandardCheckoutTemplate : CheckoutTemplate
{
    public StandardCheckoutTemplate(ECommerceFacade facade) : base(facade)
    {
    }

    protected override IOrder CreateBaseOrder(decimal subtotal)
    {
        return Facade.CreateStandardOrder(subtotal);
    }
}
