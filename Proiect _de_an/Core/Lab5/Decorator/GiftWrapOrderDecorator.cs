using Proiect__de_an.Core.Lab2.AbstractFactory;

namespace Proiect__de_an.Core.Lab5.Decorator
{
    /// <summary>
    /// Decorator concret: adaugă cost pentru ambalaj cadou la comandă.
    /// </summary>
    public class GiftWrapOrderDecorator : OrderDecorator
    {
        private readonly decimal _giftWrapFee;
        private readonly decimal _totalWithGiftWrap;

        public GiftWrapOrderDecorator(IOrder order, decimal giftWrapFee) : base(order)
        {
            _giftWrapFee = giftWrapFee;
            _totalWithGiftWrap = _order.Total + _giftWrapFee;
        }

        public override decimal Total => _totalWithGiftWrap;

        public override string GetSummary() =>
            $"{_order.GetSummary()} | Ambalaj cadou: +{_giftWrapFee:C} = {Total:C}";
    }
}
