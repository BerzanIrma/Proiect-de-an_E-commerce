using Proiect__de_an.Core.Lab2.AbstractFactory;

namespace Proiect__de_an.Core.Lab5.Bridge
{
    /// <summary>
    /// Abstracția (Bridge): reprezintă o plată pentru o comandă.
    /// Conține o referință către implementorul IPaymentProcessor.
    /// </summary>
    public abstract class Payment
    {
        protected readonly IOrder Order;
        protected readonly IPaymentProcessor Processor;

        protected Payment(IOrder order, IPaymentProcessor processor)
        {
            Order = order;
            Processor = processor;
        }

        /// <summary>
        /// Execută plata și întoarce un mesaj pentru UI.
        /// </summary>
        public abstract string Execute();
    }
}

