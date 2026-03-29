using Proiect__de_an.Core.Lab2.AbstractFactory;

namespace Proiect__de_an.Core.Lab5.Bridge
{
    /// <summary>
    /// ConcreteImplementor: procesator pentru plăți cu cardul.
    /// </summary>
    public class CardPaymentProcessor : IPaymentProcessor
    {
        public string Name => "Card";

        public string ProcessPayment(IOrder order)
        {
            return $"Plata pentru comanda {order.OrderId} în valoare de {order.Total:C} a fost procesată prin CARD.";
        }
    }
}

