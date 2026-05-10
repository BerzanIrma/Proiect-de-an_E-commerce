namespace Proiect__de_an.Core.Lab7.Mediator;

public class CheckoutMediatorRequest
{
    public decimal Subtotal { get; init; }
    public decimal DeliveryCost { get; init; }
    public string DeliveryType { get; init; } = "Standard";
    public bool GiftWrapRequested { get; init; }
    public decimal GiftWrapFee { get; init; }
    public bool DiscountApplied { get; init; }
    public string PaymentMethod { get; init; } = "Card";
    public string ActionType { get; init; } = "update";
    public string CurrentOrderStateName { get; init; } = "CartOpen";
    public string? CardHolder { get; init; }
    public string? CardNumber { get; init; }
    public string? CardExpiry { get; init; }
    public string? CardCvv { get; init; }
}
