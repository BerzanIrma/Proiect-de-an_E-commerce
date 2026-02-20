namespace Proiect__de_an.Core.Lab2.AbstractFactory
{
    public interface IECommerceFactory
    {
        IOrder CreateOrder(string orderId, decimal total);
        IPaymentProcessor CreatePaymentProcessor();
        IShippingService CreateShippingService();
    }
}
