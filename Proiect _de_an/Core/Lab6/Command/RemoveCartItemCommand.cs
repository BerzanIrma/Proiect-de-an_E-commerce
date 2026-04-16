using Proiect__de_an.Models;
using Proiect__de_an.Services;

namespace Proiect__de_an.Core.Lab6.Command;

/// <summary>Comandă: șterge linia la index. Undo: readaugă aceeași linie.</summary>
public sealed class RemoveCartItemCommand : ICartCommand
{
    private readonly int _index;
    private CartItem? _removed;

    public RemoveCartItemCommand(int index)
    {
        _index = index;
    }

    public void Execute(ICartService cart)
    {
        var vm = cart.GetCartViewModel();
        if (_index < 0 || _index >= vm.Items.Count)
            return;
        var line = vm.Items[_index];
        _removed = new CartItem
        {
            Id = line.Id,
            Name = line.Name,
            Price = line.Price,
            Quantity = line.Quantity
        };
        cart.RemoveAt(_index);
    }

    public CartUndoPayload? CreateUndoPayload()
    {
        if (_removed == null) return null;
        return new CartUndoPayload
        {
            Kind = "remove",
            ProductId = _removed.Id,
            Name = _removed.Name,
            Price = _removed.Price,
            Quantity = _removed.Quantity
        };
    }
}
