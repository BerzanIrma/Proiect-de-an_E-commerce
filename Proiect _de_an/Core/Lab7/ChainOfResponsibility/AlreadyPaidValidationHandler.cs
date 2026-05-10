namespace Proiect__de_an.Core.Lab7.ChainOfResponsibility;

public class AlreadyPaidValidationHandler : CheckoutValidationHandlerBase
{
    public override CheckoutValidationResult Handle(CheckoutValidationContext context)
    {
        if (string.Equals(context.OrderStateName, "Paid", StringComparison.Ordinal)
            || string.Equals(context.OrderStateName, "Shipped", StringComparison.Ordinal))
        {
            return CheckoutValidationResult.Failure("Comanda este deja achitată.");
        }

        return base.Handle(context);
    }
}
