using Proiect__de_an.Core.Lab6.Iterator;

namespace Proiect__de_an.Models;

public class CartViewModel
{
    public List<CartItem> Items { get; set; } = new();
    public string DeliveryType { get; set; } = "Standard"; // "Standard" | "Express"
    /// <summary>Subtotal calculat cu Iterator (Lab6) peste liniile coșului.</summary>
    public decimal Subtotal => CartLineSubtotalCalculator.SumLineTotals(Items);
    public decimal DeliveryCost => DeliveryType == "Express" ? 15m : 5m;
    public decimal Total => Subtotal + DeliveryCost;
    /// <summary>Număr total articole (cantități) prin Iterator (Lab6).</summary>
    public int TotalItems => CartLineSubtotalCalculator.SumQuantities(Items);

    // Opțiuni Decorator (ambalaj cadou, reducere fixă 10%)
    public bool GiftWrapRequested { get; set; }
    public decimal GiftWrapFee { get; set; } = 5m;
    public bool DiscountApplied { get; set; }
    public decimal DisplayDiscountPercent => Proiect__de_an.Core.Lab5.Decorator.DiscountOrderDecorator.SiteDiscountPercent;
    /// <summary>Total după aplicarea ambalajului cadou și/sau reducerii. Dacă 0, se afișează Total.</summary>
    public decimal FinalTotal { get; set; }

    /// <summary>Flyweight: număr de produse unice partajate în cache (demonstrare optimizare).</summary>
    public int? FlyweightCacheSize { get; set; }
    /// <summary>Flyweight: număr de linii în coș (extrinsic).</summary>
    public int? CartLinesCount { get; set; }
}
