using Proiect__de_an.Core.Lab2.AbstractFactory;

namespace Proiect__de_an.Core.Lab5.Decorator
{
    /// <summary>
    /// Decorator concret: aplică o reducere procentuală la totalul comenzii.
    /// Reducerea aplicată pe site este 10% (SiteDiscountPercent).
    /// </summary>
    public class DiscountOrderDecorator : OrderDecorator
    {
        /// <summary>Procentul de reducere aplicat pe site (fix).</summary>
        public const decimal SiteDiscountPercent = 10m;

        private readonly decimal _percent;
        private readonly decimal _totalAfterDiscount;

        /// <summary>Constructor pentru reducerea de site (10%).</summary>
        public DiscountOrderDecorator(IOrder order) : this(order, SiteDiscountPercent) { }

        public DiscountOrderDecorator(IOrder order, decimal discountPercent) : base(order)
        {
            _percent = discountPercent;
            _totalAfterDiscount = _order.Total * (1 - _percent / 100);
        }

        public override decimal Total => _totalAfterDiscount;

        public override string GetSummary() =>
            $"{_order.GetSummary()} | Reducere {_percent}%: {Total:C}";
    }
}
