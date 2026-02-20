namespace Proiect__de_an.Models;

public class CartItem
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public int Quantity { get; set; } = 1;
    public decimal LineTotal => Price * Quantity;
}
