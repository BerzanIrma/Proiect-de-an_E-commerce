using Proiect__de_an.Core.Lab2.AbstractFactory;

namespace Proiect__de_an.Core.Lab5.Bridge
{
    /// <summary>
    /// Implementor (Bridge): definește interfața pentru procesatoarele de plată concrete.
    /// Abstracția (Payment) delegă aici detaliile specifice de plată.
    /// </summary>
    public interface IPaymentProcessor
    {
        string Name { get; }

        /// <summary>
        /// Procesează plata pentru o comandă și întoarce un mesaj descriptiv .
        /// </summary>
        string ProcessPayment(IOrder order);
    }
}

