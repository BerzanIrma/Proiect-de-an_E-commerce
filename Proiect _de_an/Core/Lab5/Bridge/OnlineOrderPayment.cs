using Proiect__de_an.Core.Lab2.AbstractFactory;

namespace Proiect__de_an.Core.Lab5.Bridge
{
    /// <summary>
    /// Refined Abstraction: plată online pentru o comandă existentă.
    /// Poate lucra cu orice IPaymentProcessor (card, PayPal etc.).
    /// </summary>
    public class OnlineOrderPayment : Payment
    {
        public OnlineOrderPayment(IOrder order, IPaymentProcessor processor)
            : base(order, processor)
        {
        }

        public override string Execute()
        {
            return Processor.ProcessPayment(Order);
        }
    }
}

