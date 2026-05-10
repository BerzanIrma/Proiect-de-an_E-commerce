namespace Proiect__de_an.Core.Lab7.ChainOfResponsibility;

public abstract class CheckoutValidationHandlerBase : ICheckoutValidationHandler
{
    private ICheckoutValidationHandler? _next;

    public ICheckoutValidationHandler SetNext(ICheckoutValidationHandler next)
    {
        _next = next;
        return next;
    }

    public virtual CheckoutValidationResult Handle(CheckoutValidationContext context)
    {
        return _next?.Handle(context) ?? CheckoutValidationResult.Success();
    }
}
