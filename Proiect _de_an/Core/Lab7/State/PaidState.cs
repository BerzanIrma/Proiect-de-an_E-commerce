namespace Proiect__de_an.Core.Lab7.State;

public class PaidState : OrderStateBase
{
    public override string Name => "Paid";

    public override void Ship(OrderStateContext context)
    {
        context.TransitionTo(new ShippedState(), "Comanda a fost predată curierului.");
    }
}
