namespace Proiect__de_an.Core.Lab7.TemplateMethod;

/// <summary>
/// Datele de intrare pentru fluxul de checkout.
/// </summary>
public class CheckoutOptions
{
    public decimal Subtotal { get; init; }
    public bool GiftWrap { get; init; }
    public decimal GiftWrapFee { get; init; } = 5m;
    public bool ApplyDiscount { get; init; }
    public string PaymentMethod { get; init; } = "Card";
}
