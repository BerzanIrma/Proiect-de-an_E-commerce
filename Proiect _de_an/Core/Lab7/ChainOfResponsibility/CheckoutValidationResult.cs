namespace Proiect__de_an.Core.Lab7.ChainOfResponsibility;

public class CheckoutValidationResult
{
    public bool IsValid { get; }
    public string? ErrorMessage { get; }

    private CheckoutValidationResult(bool isValid, string? errorMessage)
    {
        IsValid = isValid;
        ErrorMessage = errorMessage;
    }

    public static CheckoutValidationResult Success() => new(true, null);
    public static CheckoutValidationResult Failure(string message) => new(false, message);
}
