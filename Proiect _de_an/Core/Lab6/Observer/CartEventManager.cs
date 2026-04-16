namespace Proiect__de_an.Core.Lab6.Observer;

/// <summary>Analog EventManager din diagrama Observer: listă de ascultători + notificare pe tip de eveniment.</summary>
public class CartEventManager
{
    /// <summary>Tipul de eveniment folosit când se modifică coșul (salvare articole sau livrare).</summary>
    public const string CartChanged = "CartChanged";

    private readonly List<ICartObserver> _listeners = new();

    public void Subscribe(ICartObserver listener)
    {
        ArgumentNullException.ThrowIfNull(listener);
        if (!_listeners.Contains(listener))
            _listeners.Add(listener);
    }

    public void Unsubscribe(ICartObserver listener)
    {
        _listeners.Remove(listener);
    }

    public void Notify(string eventType, CartChangeEvent data)
    {
        if (eventType != CartChanged)
            return;
        foreach (var listener in _listeners)
            listener.Update(data);
    }
}
