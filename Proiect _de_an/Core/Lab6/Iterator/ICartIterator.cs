using Proiect__de_an.Models;

namespace Proiect__de_an.Core.Lab6.Iterator;

/// <summary>
/// Iterator: parcurge secvențial liniile din coș fără a expune structura internă (listă, session etc.).
/// </summary>
public interface ICartIterator
{
    bool HasNext();
    CartItem Next();
}
