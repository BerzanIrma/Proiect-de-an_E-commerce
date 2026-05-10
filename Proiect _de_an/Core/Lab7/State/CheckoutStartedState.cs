namespace Proiect__de_an.Core.Lab7.State;

public class CheckoutStartedState : OrderStateBase
{
    public override string Name => "CheckoutStarted";

    public override void StartPayment(OrderStateContext context)
    {
        context.TransitionTo(new PaymentPendingState(), "A fost inițiată procesarea plății.");
    }

    public override void Cancel(OrderStateContext context)
    {
        context.TransitionTo(new CancelledState(), "Comanda a fost anulată în checkout.");
    }
}
