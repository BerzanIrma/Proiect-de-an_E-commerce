namespace Proiect__de_an.Core.Lab7.TemplateMethod;

public interface ICheckoutTemplate
{
    CheckoutExecutionResult RunCheckout(CheckoutOptions options);
}
