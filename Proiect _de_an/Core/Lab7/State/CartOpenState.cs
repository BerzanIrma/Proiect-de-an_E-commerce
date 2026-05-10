namespace Proiect__de_an.Core.Lab7.State;

public class CartOpenState : OrderStateBase
{
    public override string Name => "CartOpen";

    public override void BeginCheckout(OrderStateContext context)
    {
        context.TransitionTo(new CheckoutStartedState(), "Utilizatorul a început checkout-ul.");
    }

    public override void Cancel(OrderStateContext context)
    {
        context.TransitionTo(new CancelledState(), "Comanda a fost anulată înainte de checkout.");
    }
}
