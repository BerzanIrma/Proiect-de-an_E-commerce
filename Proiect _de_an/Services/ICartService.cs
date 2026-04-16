using Proiect__de_an.Models;

namespace Proiect__de_an.Services;

/// <summary>
/// Interfața serviciului de coș (pentru Proxy: RealSubject și Proxy o implementează).
/// </summary>
public interface ICartService
{
    void AddItem(string id, string name, decimal price, int quantity = 1);
    void RemoveAt(int index);
    /// <summary>Scade cantitatea de pe o linie sau elimină linia (pentru Undo la Command Add).</summary>
    void RemoveProductQuantity(string productId, int quantity);
    void SetDeliveryType(string type);
    CartViewModel GetCartViewModel();
}
