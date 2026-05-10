namespace Proiect__de_an.Models;

public class ShopIndexViewModel
{
    public int TotalCategoryNodes { get; set; }
    public int CategoryGroupsCount { get; set; }
    public int SubcategoryCount { get; set; }
    public List<string> CategoryPaths { get; set; } = new();
}
