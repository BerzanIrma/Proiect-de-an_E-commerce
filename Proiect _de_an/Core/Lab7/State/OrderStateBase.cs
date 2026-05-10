namespace Proiect__de_an.Core.Lab7.State;

public abstract class OrderStateBase : IOrderState
{
    public abstract string Name { get; }

    public virtual void BeginCheckout(OrderStateContext context) => ThrowInvalidTransition(nameof(BeginCheckout));
    public virtual void StartPayment(OrderStateContext context) => ThrowInvalidTransition(nameof(StartPayment));
    public virtual void ConfirmPayment(OrderStateContext context) => ThrowInvalidTransition(nameof(ConfirmPayment));
    public virtual void Ship(OrderStateContext context) => ThrowInvalidTransition(nameof(Ship));
    public virtual void Cancel(OrderStateContext context) => ThrowInvalidTransition(nameof(Cancel));

    protected void ThrowInvalidTransition(string action)
    {
        throw new InvalidOperationException($"Acțiunea '{action}' nu este permisă în starea '{Name}'.");
    }
}
