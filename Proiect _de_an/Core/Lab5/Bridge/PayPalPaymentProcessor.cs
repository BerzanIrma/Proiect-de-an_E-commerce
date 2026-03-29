using Proiect__de_an.Core.Lab2.AbstractFactory;

namespace Proiect__de_an.Core.Lab5.Bridge
{
    /// <summary>
    /// ConcreteImplementor: procesator pentru plăți prin PayPal.
    /// </summary>
    public class PayPalPaymentProcessor : IPaymentProcessor
    {
        public string Name => "PayPal";

        public string ProcessPayment(IOrder order)
        {
            return $"Plata pentru comanda {order.OrderId} în valoare de {order.Total:C} a fost procesată prin PAYPAL.";
        }
    }
}

