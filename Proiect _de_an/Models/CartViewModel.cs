namespace Proiect__de_an.Models;

public class CartViewModel
{
    public List<CartItem> Items { get; set; } = new();
    public string DeliveryType { get; set; } = "Standard"; // "Standard" | "Express"
    public decimal Subtotal => Items.Sum(i => i.LineTotal);
    public decimal DeliveryCost => DeliveryType == "Express" ? 15m : 5m;
    public decimal Total => Subtotal + DeliveryCost;
    public int TotalItems => Items.Sum(i => i.Quantity);
}
