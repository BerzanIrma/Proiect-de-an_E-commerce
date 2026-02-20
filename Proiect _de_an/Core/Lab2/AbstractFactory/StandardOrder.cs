namespace Proiect__de_an.Core.Lab2.AbstractFactory
{
    // --- Familia STANDARD ---
    public class StandardOrder : IOrder
    {
        public string OrderId { get; }
        public decimal Total { get; }
        public StandardOrder(string orderId, decimal total) { OrderId = orderId; Total = total; }
        public string GetSummary() => $"Comandă #{OrderId} - {Total:C}";
    }
}
