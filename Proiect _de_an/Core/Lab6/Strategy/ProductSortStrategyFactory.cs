namespace Proiect__de_an.Core.Lab6.Strategy;

/// <summary>
/// Context / factory: alege strategia după cheie (query string), fără switch mare în controller.
/// </summary>
public class ProductSortStrategyFactory
{
    private readonly DefaultOrderSortStrategy _default = new();
    private readonly PriceAscendingSortStrategy _priceAsc = new();
    private readonly PriceDescendingSortStrategy _priceDesc = new();
    private readonly NameAscendingSortStrategy _nameAsc = new();

    public IProductSortStrategy GetStrategy(string? sortKey) =>
        sortKey?.Trim().ToLowerInvariant() switch
        {
            "priceasc" => _priceAsc,
            "pricedesc" => _priceDesc,
            "nameasc" => _nameAsc,
            _ => _default
        };
}
