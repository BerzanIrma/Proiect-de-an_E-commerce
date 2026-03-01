using System;
using Proiect__de_an.Core.Lab2.AbstractFactory;
using Proiect__de_an.Core.Lab2.FactoryMethod;

namespace Proiect__de_an.Core.Lab3.Builder
{
    /// <summary>
    /// Directorul care știe pașii de construire a unei comenzi.
    /// CreateOrder(builder): void — clientul obține Order apelând builder.Build() după.
    /// </summary>
    public class OrderDirector
    {
        public void CreateOrder(IOrderBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.Reset();

            var haineFactory = new HaineProductFactory();
            var accesoriiFactory = new AccesoriiProductFactory();

            builder.AddProduct(haineFactory.CreateProduct("B1", "Bluză basic", 89m));
            builder.AddProduct(accesoriiFactory.CreateProduct("A1", "Curea clasică", 59m));

            builder.SetShipping(new StandardShippingService());
            builder.SetPayment(new CardPaymentProcessor());
        }
    }
}

