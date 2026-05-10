namespace Proiect__de_an.Core.Lab7.ChainOfResponsibility;

public class CardInputValidationHandler : CheckoutValidationHandlerBase
{
    public override CheckoutValidationResult Handle(CheckoutValidationContext context)
    {
        if (!string.Equals(context.PaymentMethod, "Card", StringComparison.OrdinalIgnoreCase))
            return base.Handle(context);

        var valid = !string.IsNullOrWhiteSpace(context.CardHolder)
            && !string.IsNullOrWhiteSpace(context.CardExpiry)
            && !string.IsNullOrWhiteSpace(context.CardNumber)
            && context.CardNumber.Replace(" ", string.Empty).Length >= 12
            && !string.IsNullOrWhiteSpace(context.CardCvv)
            && context.CardCvv.Length is 3 or 4;

        if (!valid)
            return CheckoutValidationResult.Failure("Datele cardului sunt incomplete. Completează toate câmpurile pentru plata demo.");

        return base.Handle(context);
    }
}
