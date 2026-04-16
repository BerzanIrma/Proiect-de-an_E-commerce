using Proiect__de_an.Services;

namespace Proiect__de_an.Core.Lab6.Command;

/// <summary>Date serializabile pentru Undo ultima operație (Command + istoric în sesiune).</summary>
public sealed class CartUndoPayload
{
    public string Kind { get; init; } = ""; // add | remove | delivery

    public string? ProductId { get; init; }
    public int? Quantity { get; init; }
    public string? Name { get; init; }
    public decimal? Price { get; init; }
    public string? PreviousDelivery { get; init; }

    public void ApplyUndo(ICartService cart)
    {
        switch (Kind)
        {
            case "add":
                if (!string.IsNullOrEmpty(ProductId) && Quantity is > 0)
                    cart.RemoveProductQuantity(ProductId, Quantity.Value);
                break;
            case "remove":
                if (!string.IsNullOrEmpty(ProductId) && Quantity is > 0 && Price.HasValue)
                    cart.AddItem(ProductId!, Name ?? "", Price.Value, Quantity.Value);
                break;
            case "delivery":
                if (!string.IsNullOrEmpty(PreviousDelivery))
                    cart.SetDeliveryType(PreviousDelivery);
                break;
        }
    }
}
