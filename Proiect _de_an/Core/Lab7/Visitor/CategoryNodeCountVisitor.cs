using Proiect__de_an.Core.Lab4.Composite;

namespace Proiect__de_an.Core.Lab7.Visitor;

public class CategoryNodeCountVisitor : CategoryVisitorBase
{
    public int TotalNodes { get; private set; }
    public int CompositeCount { get; private set; }
    public int LeafCount { get; private set; }

    public override void VisitLeaf(CategoryLeaf leaf)
    {
        LeafCount++;
        TotalNodes++;
    }

    public override void VisitComposite(CategoryComposite composite)
    {
        CompositeCount++;
        TotalNodes++;
        base.VisitComposite(composite);
    }
}
