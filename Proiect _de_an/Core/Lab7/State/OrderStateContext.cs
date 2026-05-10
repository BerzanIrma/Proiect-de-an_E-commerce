namespace Proiect__de_an.Core.Lab7.State;

/// <summary>
/// Context-ul State: deține starea curentă și delegă comportamentul către obiectul de stare.
/// </summary>
public class OrderStateContext
{
    private IOrderState _state;
    private readonly List<string> _transitionHistory = new();

    public OrderStateContext(string? initialStateName = null)
    {
        _state = CreateState(initialStateName);
        _transitionHistory.Add($"Stare inițială: {_state.Name}");
    }

    public string CurrentStateName => _state.Name;
    public IReadOnlyList<string> TransitionHistory => _transitionHistory;

    public void BeginCheckout() => _state.BeginCheckout(this);
    public void StartPayment() => _state.StartPayment(this);
    public void ConfirmPayment() => _state.ConfirmPayment(this);
    public void Ship() => _state.Ship(this);
    public void Cancel() => _state.Cancel(this);

    internal void TransitionTo(IOrderState nextState, string reason)
    {
        var previous = _state.Name;
        _state = nextState;
        _transitionHistory.Add($"{previous} -> {nextState.Name}: {reason}");
    }

    private static IOrderState CreateState(string? stateName)
    {
        return stateName switch
        {
            "CheckoutStarted" => new CheckoutStartedState(),
            "PaymentPending" => new PaymentPendingState(),
            "Paid" => new PaidState(),
            "Shipped" => new ShippedState(),
            "Cancelled" => new CancelledState(),
            _ => new CartOpenState()
        };
    }
}
