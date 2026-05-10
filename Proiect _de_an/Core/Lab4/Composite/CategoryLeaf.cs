using System.Collections.Generic;
using Proiect__de_an.Core.Lab7.Visitor;

namespace Proiect__de_an.Core.Lab4.Composite
{
    /// <summary>
    /// Leaf: categorie fara subcategorii.
    /// </summary>
    public class CategoryLeaf : ICategoryComponent
    {
        private readonly string _name;
        private readonly string _path;

        public CategoryLeaf(string name, string? path = null)
        {
            _name = name;
            _path = path ?? name;
        }

        public string GetName() => _name;
        public string GetPath() => _path;
        public IEnumerable<ICategoryComponent> GetChildren() => [];
        public void Accept(ICategoryVisitor visitor) => visitor.VisitLeaf(this);
    }
}
