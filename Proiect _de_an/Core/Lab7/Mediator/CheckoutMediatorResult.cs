namespace Proiect__de_an.Core.Lab7.Mediator;

public class CheckoutMediatorResult
{
    public string NextOrderStateName { get; init; } = "CartOpen";
    public decimal FinalTotal { get; init; }
    public decimal GiftWrapFeeApplied { get; init; }
    public string? PaymentMessage { get; init; }
    public string? PaymentError { get; init; }
    public string? PaymentProcessorName { get; init; }
    public string? CheckoutTemplateName { get; init; }
}
