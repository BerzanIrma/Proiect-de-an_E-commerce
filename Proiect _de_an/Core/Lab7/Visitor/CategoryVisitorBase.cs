using Proiect__de_an.Core.Lab4.Composite;

namespace Proiect__de_an.Core.Lab7.Visitor;

/// <summary>
/// Bază Visitor: definește traversarea implicită a arborelui Composite.
/// </summary>
public abstract class CategoryVisitorBase : ICategoryVisitor
{
    public virtual void VisitLeaf(CategoryLeaf leaf)
    {
    }

    public virtual void VisitComposite(CategoryComposite composite)
    {
        foreach (var child in composite.GetChildren())
            child.Accept(this);
    }
}
