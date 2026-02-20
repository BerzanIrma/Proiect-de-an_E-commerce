namespace Proiect__de_an.Core.Lab2.AbstractFactory
{
    public class ExpressECommerceFactory : IECommerceFactory
    {
        public IOrder CreateOrder(string orderId, decimal total) => new ExpressOrder(orderId, total);
        public IPaymentProcessor CreatePaymentProcessor() => new InstantPaymentProcessor();
        public IShippingService CreateShippingService() => new ExpressShippingService();
    }
}
