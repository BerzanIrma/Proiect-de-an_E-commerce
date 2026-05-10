using Proiect__de_an.Core.Lab4.Composite;

namespace Proiect__de_an.Core.Lab7.Visitor;

public class CategoryPathListVisitor : CategoryVisitorBase
{
    private readonly List<string> _paths = new();
    public IReadOnlyList<string> Paths => _paths;

    public override void VisitLeaf(CategoryLeaf leaf)
    {
        _paths.Add(leaf.GetPath());
    }

    public override void VisitComposite(CategoryComposite composite)
    {
        _paths.Add(composite.GetPath());
        base.VisitComposite(composite);
    }
}
