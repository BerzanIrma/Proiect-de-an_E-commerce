namespace Proiect__de_an.Core.Lab2.AbstractFactory
{
    public class StandardECommerceFactory : IECommerceFactory
    {
        public IOrder CreateOrder(string orderId, decimal total) => new StandardOrder(orderId, total);
        public IPaymentProcessor CreatePaymentProcessor() => new CardPaymentProcessor();
        public IShippingService CreateShippingService() => new StandardShippingService();
    }
}
