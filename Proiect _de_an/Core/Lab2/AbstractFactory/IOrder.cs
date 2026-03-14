namespace Proiect__de_an.Core.Lab2.AbstractFactory
{
   
    public interface IOrder
    {
        string OrderId { get; }
        decimal Total { get; }
        string GetSummary();
    }
}
