using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Proiect__de_an.Core.Lab6.Memento;

/// <summary>
/// Caretaker: păstrează memento-ul în session, fără a inspecta conținutul (doar persistă / încarcă).
/// </summary>
public class CartCaretaker
{
    private const string SessionKey = "Lab6_CartMemento";
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartCaretaker(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ISession Session => _httpContextAccessor.HttpContext?.Session
        ?? throw new InvalidOperationException("Session not available");

    public void SaveMemento(CartMemento memento)
    {
        Session.SetString(SessionKey, JsonSerializer.Serialize(memento, JsonOptions));
    }

    public CartMemento? LoadMemento()
    {
        var json = Session.GetString(SessionKey);
        if (string.IsNullOrEmpty(json)) return null;
        return JsonSerializer.Deserialize<CartMemento>(json, JsonOptions);
    }

    public void ClearMemento() => Session.Remove(SessionKey);

    public bool HasSavedSnapshot() => LoadMemento() != null;
}
