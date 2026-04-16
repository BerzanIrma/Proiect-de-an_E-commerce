using Proiect__de_an.Services;

namespace Proiect__de_an.Core.Lab6.Command;

/// <summary>Comandă: adaugă produs în coș. Undo: scade aceeași cantitate de pe linie.</summary>
public sealed class AddToCartCommand : ICartCommand
{
    private readonly string _id;
    private readonly string _name;
    private readonly decimal _price;
    private readonly int _quantity;

    public AddToCartCommand(string id, string name, decimal price, int quantity)
    {
        _id = id;
        _name = name;
        _price = price;
        _quantity = quantity < 1 ? 1 : quantity;
    }

    public void Execute(ICartService cart)
    {
        cart.AddItem(_id, _name, _price, _quantity);
    }

    public CartUndoPayload? CreateUndoPayload()
    {
        return new CartUndoPayload
        {
            Kind = "add",
            ProductId = _id,
            Quantity = _quantity
        };
    }
}
