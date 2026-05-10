namespace Proiect__de_an.Core.Lab7.ChainOfResponsibility;

public interface ICheckoutValidationHandler
{
    ICheckoutValidationHandler SetNext(ICheckoutValidationHandler next);
    CheckoutValidationResult Handle(CheckoutValidationContext context);
}
