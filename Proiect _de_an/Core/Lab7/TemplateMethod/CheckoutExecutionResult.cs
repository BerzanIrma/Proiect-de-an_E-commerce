namespace Proiect__de_an.Core.Lab7.TemplateMethod;

/// <summary>
/// Rezultatul obținut după executarea șablonului de checkout.
/// </summary>
public class CheckoutExecutionResult
{
    public decimal FinalTotal { get; init; }
    public decimal GiftWrapFeeApplied { get; init; }
    public string PaymentMessage { get; init; } = string.Empty;
    public string PaymentProcessorName { get; init; } = string.Empty;
    public string TemplateName { get; init; } = string.Empty;
}
