namespace Proiect__de_an.Core.Lab7.ChainOfResponsibility;

public class CheckoutValidationContext
{
    public string OrderStateName { get; init; } = "CartOpen";
    public string PaymentMethod { get; init; } = "Card";
    public string? CardHolder { get; init; }
    public string? CardNumber { get; init; }
    public string? CardExpiry { get; init; }
    public string? CardCvv { get; init; }
}
