namespace Proiect__de_an.Core.Lab2.AbstractFactory
{
    // --- Familia EXPRESS ---
    public class ExpressOrder : IOrder
    {
        public string OrderId { get; }
        public decimal Total { get; }
        public ExpressOrder(string orderId, decimal total) { OrderId = orderId; Total = total; }
        public string GetSummary() => $"Comandă EXPRESS #{OrderId} - {Total:C}";
    }
}
