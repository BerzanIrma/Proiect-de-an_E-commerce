using System;
using Proiect__de_an.Core.Lab2.AbstractFactory;
using Proiect__de_an.Core.Lab3.Builder;
using Proiect__de_an.Core.Lab3.Singleton;
using Proiect__de_an.Core.Lab4.Adapter;
using Proiect__de_an.Core.Lab4.Composite;
using Proiect__de_an.Core.Lab5.Decorator;
namespace Proiect__de_an.Core.Lab4.Facade
{
    public class ECommerceFacade
    {
        // Subsystem: Abstract Factory 
        private readonly IECommerceFactory standardFactory = new StandardECommerceFactory();
        private readonly IECommerceFactory expressFactory = new ExpressECommerceFactory();

    
        /// <summary>
        /// Adapter converteste Order la IOrder.
        /// </summary>
        public IOrder CreateSampleOrderWithBuilder()
        {
            // Subsystem: Builder (Director + ConcreteBuilder → Order)
            var builder = new DefaultOrderBuilder();
            var director = new OrderDirector();
            director.CreateOrder(builder);
            var order = builder.Build();
            // Adapter: Order (Builder) → IOrder 
            return new OrderToIOrderAdapter(order);
        }

        /// <summary>
        /// Creează o comandă standard folosind Abstract Factory
        /// și costul de livrare din Singleton (StoreSettings).
        /// </summary>
        public IOrder CreateStandardOrder(decimal productsTotal)
        {
            // Subsystems: Singleton (StoreSettings), Abstract Factory (Standard)
            var settings = StoreSettings.Instance;
            var total = productsTotal + settings.DefaultShippingCost;

            var orderId = Guid.NewGuid().ToString("N")[..8];
            return standardFactory.CreateOrder(orderId, total);
        }

        /// <summary>
        /// Creează o comanda express folosind Abstract Factory
        /// costul de livrare express din Singleton.
        /// </summary>
        public IOrder CreateExpressOrder(decimal productsTotal)
        {
            // Subsystem: Abstract Factory (Express); StoreSettings pentru cost livrare
            var settings = StoreSettings.Instance;
            var total = productsTotal + settings.ExpressShippingCost;

            var orderId = Guid.NewGuid().ToString("N")[..8];
            return expressFactory.CreateOrder(orderId, total);
        }

        /// <summary>
        /// Creează o comandă standard și aplică o reducere (Decorator).
        /// </summary>
        public IOrder CreateStandardOrderWithDiscount(decimal productsTotal, decimal discountPercent)
        {
            var order = CreateStandardOrder(productsTotal);
            return new DiscountOrderDecorator(order, discountPercent);
        }

        /// <summary>
        /// Creează o comandă express și aplică o reducere (Decorator).
        /// </summary>
        public IOrder CreateExpressOrderWithDiscount(decimal productsTotal, decimal discountPercent)
        {
            var order = CreateExpressOrder(productsTotal);
            return new DiscountOrderDecorator(order, discountPercent);
        }

        /// <summary>
        /// Adaugă ambalaj cadou la orice comandă (Decorator). Funcționează cu Standard, Express sau Builder.
        /// </summary>
        public IOrder CreateOrderWithGiftWrap(IOrder order, decimal giftWrapFee)
        {
            return new GiftWrapOrderDecorator(order, giftWrapFee);
        }

        /// <summary>
        /// Returneaza arborele de categorii (Composite). Clientul tratează uniform leaf și composite.
        /// </summary>
        public ICategoryComponent GetCategoryTree()
        {
            var haine = new CategoryComposite("Haine", "Haine");
            haine.Add(new CategoryLeaf("Femei", "Haine > Femei"));
            haine.Add(new CategoryLeaf("Bărbați", "Haine > Bărbați"));

            var accesorii = new CategoryComposite("Accesorii", "Accesorii");
            accesorii.Add(new CategoryLeaf("Genti", "Accesorii > Genti"));
            accesorii.Add(new CategoryLeaf("Eșarfe", "Accesorii > Eșarfe"));

            var root = new CategoryComposite("Magazin", "Magazin");
            root.Add(haine);
            root.Add(accesorii);
            return root;
        }
    }
}

