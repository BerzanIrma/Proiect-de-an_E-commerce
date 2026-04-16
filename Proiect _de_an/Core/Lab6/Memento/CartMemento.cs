using Proiect__de_an.Models;

namespace Proiect__de_an.Core.Lab6.Memento;

/// <summary>
/// Memento: snapshot imuabil al stării coșului (linii + livrare), fără logică de business.
/// </summary>
public class CartMemento
{
    public List<CartItem> Items { get; init; } = new();
    public string DeliveryType { get; init; } = "Standard";
}
