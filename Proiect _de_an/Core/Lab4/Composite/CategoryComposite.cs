using System.Collections.Generic;
using System.Linq;

namespace Proiect__de_an.Core.Lab4.Composite
{
    /// <summary>
    /// Composite: categorie care contine subcategorii (alte ICategoryComponent).
    /// Acelasi tip de operatii ca leaf-ul, dar le deleaga si la copii.
    /// </summary>
    public class CategoryComposite : ICategoryComponent
    {
        private readonly string _name;
        private readonly string _path;
        private readonly List<ICategoryComponent> _children = new();

        public CategoryComposite(string name, string? path = null)
        {
            _name = name;
            _path = path ?? name;
        }

        public void Add(ICategoryComponent child) => _children.Add(child);

        public string GetName() => _name;
        public string GetPath() => _path;
        public IEnumerable<ICategoryComponent> GetChildren() => _children;
    }
}
