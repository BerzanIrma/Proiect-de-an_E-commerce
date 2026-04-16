using Proiect__de_an.Services;

namespace Proiect__de_an.Core.Lab6.Command;

/// <summary>Comandă: setează tipul de livrare. Undo: revine la livrarea anterioară.</summary>
public sealed class SetDeliveryCommand : ICartCommand
{
    private readonly string _newType;
    private string? _previous;

    public SetDeliveryCommand(string newType)
    {
        _newType = newType;
    }

    public void Execute(ICartService cart)
    {
        _previous = cart.GetCartViewModel().DeliveryType;
        cart.SetDeliveryType(_newType);
    }

    public CartUndoPayload? CreateUndoPayload()
    {
        if (string.IsNullOrEmpty(_previous)) return null;
        return new CartUndoPayload
        {
            Kind = "delivery",
            PreviousDelivery = _previous
        };
    }
}
