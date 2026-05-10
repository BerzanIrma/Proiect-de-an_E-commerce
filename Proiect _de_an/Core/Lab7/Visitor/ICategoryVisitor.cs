using Proiect__de_an.Core.Lab4.Composite;

namespace Proiect__de_an.Core.Lab7.Visitor;

public interface ICategoryVisitor
{
    void VisitLeaf(CategoryLeaf leaf);
    void VisitComposite(CategoryComposite composite);
}
