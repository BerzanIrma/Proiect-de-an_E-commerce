using System.Collections.Generic;

namespace Proiect__de_an.Core.Lab4.Composite
{
    /// <summary>
    /// Component (Composite pattern): interfata comuna pentru noduri de categorie (leaf și composite).
    /// Clientul trateaza uniform o categorie simpla si o categorie cu subcategorii.
    /// </summary>
    public interface ICategoryComponent
    {
        string GetName();
        string GetPath();
        IEnumerable<ICategoryComponent> GetChildren();
    }
}
