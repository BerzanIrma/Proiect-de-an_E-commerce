using System;

namespace Proiect__de_an.Core.Lab3.Singleton
{
    /// <summary>
    /// Singleton: o singură instanță de setări globale pentru magazin (Lab3).
    /// </summary>
    public sealed class StoreSettings
    {
        private static volatile StoreSettings instance;
        private static readonly object lockObj = new object();

        public string StoreName { get; set; } = "Stylish Store";
        public string DefaultCurrency { get; set; } = "RON";
        public decimal DefaultShippingCost { get; set; } = 5m;
        public decimal ExpressShippingCost { get; set; } = 15m;

        private StoreSettings() { }

        public static StoreSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObj)
                    {
                        if (instance == null)
                            instance = new StoreSettings();
                    }
                }
                return instance;
            }
        }
    }
}
