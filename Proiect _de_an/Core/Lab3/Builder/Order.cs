using System.Collections.Generic;
using System.Linq;
using Proiect__de_an.Core.Lab2.AbstractFactory;
using Proiect__de_an.Core.Lab2.FactoryMethod;

namespace Proiect__de_an.Core.Lab3.Builder
{
    /// <summary>
    /// Produsul construit de Builder: o comandă care conține produse,
    /// un serviciu de livrare și un procesator de plată.
    /// </summary>
    public class Order
    {
        private readonly List<IProduct> _products = new();

        public IReadOnlyList<IProduct> Products => _products;

        public IShippingService? Shipping { get; private set; }

        public IPaymentProcessor? Payment { get; private set; }

        public decimal Total => _products.Sum(p => p.Price);

        internal void AddProduct(IProduct product)
        {
            _products.Add(product);
        }

        internal void SetShipping(IShippingService shipping)
        {
            Shipping = shipping;
        }

        internal void SetPayment(IPaymentProcessor payment)
        {
            Payment = payment;
        }

        public string GetSummary()
        {
            var productsPart = _products.Count == 0
                ? "Fără produse"
                : string.Join(", ", _products.Select(p => p.Name));

            var shippingInfo = Shipping?.GetInfo() ?? "Fără livrare";
            var paymentInfo = Payment?.GetInfo() ?? "Fără plată";

            return $"Produse: {productsPart}; Livrare: {shippingInfo}; Plată: {paymentInfo}; Total: {Total:C}";
        }
    }
}

