namespace Proiect__de_an.Core.Lab3.Builder
{
    using Proiect__de_an.Core.Lab2.FactoryMethod;
    using Proiect__de_an.Core.Lab2.AbstractFactory;

    /// <summary>
    /// Interfața Builder-ului pentru construirea comenzilor.
    /// </summary>
    public interface IOrderBuilder
    {
        void Reset();

        void AddProduct(IProduct product);

        void SetShipping(IShippingService shippingService);

        void SetPayment(IPaymentProcessor paymentProcessor);

        Order Build();
    }
}

