namespace Proiect__de_an.Core.Lab2.AbstractFactory
{
    // ========== FUNCȚIONALITATE: Abstract Factory ==========
    // Creezi o FAMILIE de obiecte care merg împreună: Comandă + Plată + Livrare.
    // Alegi O singură fabrică (Standard SAU Express) și primești toate cele 3 obiecte compatibile.
    // Folosire: var f = new StandardECommerceFactory();
    //           var order = f.CreateOrder("ORD-1", 100m); var pay = f.CreatePaymentProcessor(); var ship = f.CreateShippingService();

    public interface IOrder
    {
        string OrderId { get; }
        decimal Total { get; }
        string GetSummary();
    }
}
