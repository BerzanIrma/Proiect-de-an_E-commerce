namespace Proiect__de_an.Core.Lab7.Mediator;

public interface ICheckoutMediator
{
    CheckoutMediatorResult Handle(CheckoutMediatorRequest request);
}
