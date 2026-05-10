namespace Proiect__de_an.Core.Lab7.ChainOfResponsibility;

public class CancelledOrderValidationHandler : CheckoutValidationHandlerBase
{
    public override CheckoutValidationResult Handle(CheckoutValidationContext context)
    {
        if (string.Equals(context.OrderStateName, "Cancelled", StringComparison.Ordinal))
            return CheckoutValidationResult.Failure("Comanda este anulată și nu mai poate fi achitată.");

        return base.Handle(context);
    }
}
