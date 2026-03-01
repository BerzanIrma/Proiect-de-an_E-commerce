using System;
using Proiect__de_an.Core.Lab2.AbstractFactory;
using Proiect__de_an.Core.Lab2.FactoryMethod;

namespace Proiect__de_an.Core.Lab3.Builder
{
    /// <summary>
    /// Implementare concretă de Builder pentru comenzi.
    /// </summary>
    public class DefaultOrderBuilder : IOrderBuilder
    {
        private Order _order = new();

        public void Reset()
        {
            _order = new Order();
        }

        public void AddProduct(IProduct product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            _order.AddProduct(product);
        }

        public void SetShipping(IShippingService shippingService)
        {
            _order.SetShipping(shippingService ?? throw new ArgumentNullException(nameof(shippingService)));
        }

        public void SetPayment(IPaymentProcessor paymentProcessor)
        {
            _order.SetPayment(paymentProcessor ?? throw new ArgumentNullException(nameof(paymentProcessor)));
        }

        public Order Build()
        {
            // Returnează comanda curentă și resetează builder-ul
            var built = _order;
            _order = new Order();
            return built;
        }
    }
}

