using Proiect__de_an.Core.Lab2.AbstractFactory;
using Proiect__de_an.Core.Lab4.Facade;
using Proiect__de_an.Core.Lab5.Bridge;
using Proiect__de_an.Core.Lab5.Decorator;
using BridgeCardPaymentProcessor = Proiect__de_an.Core.Lab5.Bridge.CardPaymentProcessor;
using BridgePayPalPaymentProcessor = Proiect__de_an.Core.Lab5.Bridge.PayPalPaymentProcessor;
using BridgePaymentProcessor = Proiect__de_an.Core.Lab5.Bridge.IPaymentProcessor;

namespace Proiect__de_an.Core.Lab7.TemplateMethod;

/// <summary>
/// Template Method: definește algoritmul fix de checkout și lasă pașii variați în clasele concrete.
/// </summary>
public abstract class CheckoutTemplate : ICheckoutTemplate
{
    private readonly ECommerceFacade _facade;

    protected CheckoutTemplate(ECommerceFacade facade)
    {
        _facade = facade;
    }

    public CheckoutExecutionResult RunCheckout(CheckoutOptions options)
    {
        Validate(options);

        var order = CreateBaseOrder(options.Subtotal);
        var giftWrapFeeApplied = ResolveGiftWrapFee(options);
        order = ApplyDecorators(order, options, giftWrapFeeApplied);

        var processor = BuildPaymentProcessor(options);
        var paymentMessage = ExecutePayment(order, processor);

        return new CheckoutExecutionResult
        {
            FinalTotal = order.Total,
            GiftWrapFeeApplied = giftWrapFeeApplied,
            PaymentMessage = paymentMessage,
            PaymentProcessorName = processor.Name,
            TemplateName = GetType().Name
        };
    }

    protected virtual void Validate(CheckoutOptions options)
    {
        if (options.Subtotal < 0)
            throw new InvalidOperationException("Subtotal invalid pentru checkout.");
    }

    protected abstract IOrder CreateBaseOrder(decimal subtotal);

    protected virtual decimal ResolveGiftWrapFee(CheckoutOptions options)
    {
        if (!options.GiftWrap)
            return 0m;

        return options.GiftWrapFee > 0 ? options.GiftWrapFee : 5m;
    }

    protected virtual IOrder ApplyDecorators(IOrder order, CheckoutOptions options, decimal giftWrapFeeApplied)
    {
        var result = order;

        if (options.ApplyDiscount)
            result = new DiscountOrderDecorator(result);

        if (options.GiftWrap)
            result = _facade.CreateOrderWithGiftWrap(result, giftWrapFeeApplied);

        return result;
    }

    protected virtual BridgePaymentProcessor BuildPaymentProcessor(CheckoutOptions options)
    {
        return string.Equals(options.PaymentMethod, "PayPal", StringComparison.OrdinalIgnoreCase)
            ? new BridgePayPalPaymentProcessor()
            : new BridgeCardPaymentProcessor();
                }

    protected virtual string ExecutePayment(IOrder order, BridgePaymentProcessor processor)
    {
        Payment payment = new OnlineOrderPayment(order, processor);
        return payment.Execute();
    }

    protected ECommerceFacade Facade => _facade;
}
