using Proiect__de_an.Services;

namespace Proiect__de_an.Core.Lab6.Command;

/// <summary>Command: încapsulează o operație pe coș; după Execute se poate genera payload pentru Undo.</summary>
public interface ICartCommand
{
    void Execute(ICartService cart);
    CartUndoPayload? CreateUndoPayload();
}
