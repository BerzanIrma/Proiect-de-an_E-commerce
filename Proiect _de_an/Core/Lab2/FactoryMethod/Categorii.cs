namespace Proiect__de_an.Core.Lab2.FactoryMethod
{
    // ========== Factory Method: doar produse FIZICE pe categorii ==========
    // Fiecare fabrică creează produse fizice într-o categorie: Haine sau Accesorii.
    // Folosire: var p = new HaineProductFactory().CreateProduct("1", "Bluză", 89m);

    public static class Categorii
    {
        public const string Haine = "Haine";
        public const string Accesorii = "Accesorii";
    }
}
