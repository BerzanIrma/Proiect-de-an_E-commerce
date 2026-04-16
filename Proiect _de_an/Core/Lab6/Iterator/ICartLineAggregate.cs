namespace Proiect__de_an.Core.Lab6.Iterator;

/// <summary>
/// Aggregate (Iterator pattern): oferă un iterator peste liniile coșului.
/// </summary>
public interface ICartLineAggregate
{
    ICartIterator CreateIterator();
}
