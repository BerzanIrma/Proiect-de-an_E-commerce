namespace Proiect__de_an.Core.Lab7.State;

public interface IOrderState
{
    string Name { get; }
    void BeginCheckout(OrderStateContext context);
    void StartPayment(OrderStateContext context);
    void ConfirmPayment(OrderStateContext context);
    void Ship(OrderStateContext context);
    void Cancel(OrderStateContext context);
}
