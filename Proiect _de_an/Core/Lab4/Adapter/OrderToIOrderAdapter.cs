using System;
using Proiect__de_an.Core.Lab2.AbstractFactory;
using Proiect__de_an.Core.Lab3.Builder;

namespace Proiect__de_an.Core.Lab4.Adapter
{
    /// <summary>
    /// Adapter: adapteaza Order (Builder) la interfata IOrder (Abstract Factory).
    /// Clientul poate trata toate comenzile uniform prin IOrder.
    /// </summary>
    public class OrderToIOrderAdapter : IOrder
    {
        private readonly Order _order;

        public OrderToIOrderAdapter(Order order, string? orderId = null)
        {
            _order = order ?? throw new ArgumentNullException(nameof(order));
            OrderId = orderId ?? $"B-{Guid.NewGuid():N}"[..12];
        }

        public string OrderId { get; }

        public decimal Total => _order.Total;

        public string GetSummary() => _order.GetSummary();
    }
}
