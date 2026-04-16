namespace Proiect__de_an.Core.Lab6.Observer;

/// <summary>Interfața observatorilor (echivalent «interface» EventListeners / update din diagrama Observer).</summary>
public interface ICartObserver
{
    void Update(CartChangeEvent data);
}
