namespace Proiect__de_an.Core.Lab7.State;

public class PaymentPendingState : OrderStateBase
{
    public override string Name => "PaymentPending";

    public override void ConfirmPayment(OrderStateContext context)
    {
        context.TransitionTo(new PaidState(), "Plata a fost confirmată.");
    }

    public override void Cancel(OrderStateContext context)
    {
        context.TransitionTo(new CancelledState(), "Plata a eșuat, comanda a fost anulată.");
    }
}
