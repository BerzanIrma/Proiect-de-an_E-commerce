using System;
using Proiect__de_an.Core.Lab2.AbstractFactory;

namespace Proiect__de_an.Core.Lab5.Decorator
{
    /// <summary>
    /// Decorator (abstract): înfășoară un IOrder și adaugă comportament (reducere, ambalaj etc.)
    /// păstrând aceeași interfață IOrder.
    /// </summary>
    public abstract class OrderDecorator : IOrder
    {
        protected readonly IOrder _order;

        protected OrderDecorator(IOrder order)
        {
            _order = order ?? throw new ArgumentNullException(nameof(order));
        }

        public virtual string OrderId => _order.OrderId;
        public virtual decimal Total => _order.Total;
        public virtual string GetSummary() => _order.GetSummary();
    }
}
