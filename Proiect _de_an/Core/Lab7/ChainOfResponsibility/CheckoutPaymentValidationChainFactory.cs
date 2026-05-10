namespace Proiect__de_an.Core.Lab7.ChainOfResponsibility;

/// <summary>
/// Construiește lanțul de validare pentru plata unei comenzi (Chain of Responsibility).
/// </summary>
public class CheckoutPaymentValidationChainFactory
{
    public ICheckoutValidationHandler Create()
    {
        var cancelled = new CancelledOrderValidationHandler();
        var alreadyPaid = new AlreadyPaidValidationHandler();
        var cardInput = new CardInputValidationHandler();

        cancelled.SetNext(alreadyPaid)
            .SetNext(cardInput);

        return cancelled;
    }
}
