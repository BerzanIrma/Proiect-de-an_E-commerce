using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Proiect__de_an.Services;

namespace Proiect__de_an.Core.Lab6.Command;

/// <summary>Invoker: execută comenzi și păstrează stivă Undo în sesiune (cross-request).</summary>
public sealed class CartCommandInvoker
{
    private const string SessionKey = "Lab6_CartUndoStack";
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };
    private readonly ICartService _cart;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartCommandInvoker(ICartService cart, IHttpContextAccessor httpContextAccessor)
    {
        _cart = cart;
        _httpContextAccessor = httpContextAccessor;
    }

    private ISession Session => _httpContextAccessor.HttpContext?.Session
        ?? throw new InvalidOperationException("Session not available");

    public void Run(ICartCommand command)
    {
        command.Execute(_cart);
        var payload = command.CreateUndoPayload();
        if (payload == null) return;
        var stack = LoadStack();
        stack.Add(payload);
        SaveStack(stack);
    }

    /// <summary>Anulează ultima operație înregistrată în stivă.</summary>
    public bool TryUndoLast()
    {
        var stack = LoadStack();
        if (stack.Count == 0) return false;
        var last = stack[^1];
        stack.RemoveAt(stack.Count - 1);
        SaveStack(stack);
        last.ApplyUndo(_cart);
        return true;
    }

    public bool CanUndo => LoadStack().Count > 0;

    public void ClearUndoHistory() => Session.Remove(SessionKey);

    private List<CartUndoPayload> LoadStack()
    {
        var json = Session.GetString(SessionKey);
        if (string.IsNullOrEmpty(json)) return new List<CartUndoPayload>();
        try
        {
            return JsonSerializer.Deserialize<List<CartUndoPayload>>(json, JsonOptions) ?? new List<CartUndoPayload>();
        }
        catch
        {
            return new List<CartUndoPayload>();
        }
    }

    private void SaveStack(List<CartUndoPayload> stack)
    {
        Session.SetString(SessionKey, JsonSerializer.Serialize(stack, JsonOptions));
    }
}
