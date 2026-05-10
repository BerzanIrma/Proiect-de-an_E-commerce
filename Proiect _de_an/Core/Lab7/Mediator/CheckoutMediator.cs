using Proiect__de_an.Core.Lab5.Decorator;
using Proiect__de_an.Core.Lab7.ChainOfResponsibility;
using Proiect__de_an.Core.Lab7.State;
using Proiect__de_an.Core.Lab7.TemplateMethod;

namespace Proiect__de_an.Core.Lab7.Mediator;

/// <summary>
/// Mediator: coordonează colaborarea dintre Chain, State și Template Method pentru checkout.
/// </summary>
public class CheckoutMediator : ICheckoutMediator
{
    private readonly CheckoutPaymentValidationChainFactory _validationChainFactory;
    private readonly CheckoutTemplateFactory _checkoutTemplateFactory;

    public CheckoutMediator(CheckoutPaymentValidationChainFactory validationChainFactory, CheckoutTemplateFactory checkoutTemplateFactory)
    {
        _validationChainFactory = validationChainFactory;
        _checkoutTemplateFactory = checkoutTemplateFactory;
    }

    public CheckoutMediatorResult Handle(CheckoutMediatorRequest request)
    {
        var orderState = new OrderStateContext(request.CurrentOrderStateName);
        if (string.Equals(orderState.CurrentStateName, "CartOpen", StringComparison.Ordinal))
            orderState.BeginCheckout();

        if (string.Equals(request.ActionType, "cancel", StringComparison.OrdinalIgnoreCase))
        {
            try
            {
                orderState.Cancel();
                return CreateResult(orderState, CalculatePreviewTotal(request), request.GiftWrapFee, paymentMessage: "Comanda a fost anulată.");
            }
            catch (InvalidOperationException)
            {
                return CreateResult(orderState, CalculatePreviewTotal(request), request.GiftWrapFee, paymentError: "Comanda nu mai poate fi anulată din starea curentă.");
            }
        }

        if (!string.Equals(request.ActionType, "pay", StringComparison.OrdinalIgnoreCase))
        {
            return CreateResult(
                orderState,
                CalculatePreviewTotal(request),
                request.GiftWrapFee,
                paymentMessage: "Comanda este pregătită pentru plată. Completează datele și apasă «Achită comanda».");
        }

        var validationChain = _validationChainFactory.Create();
        var validationResult = validationChain.Handle(new CheckoutValidationContext
        {
            OrderStateName = orderState.CurrentStateName,
            PaymentMethod = request.PaymentMethod,
            CardHolder = request.CardHolder,
            CardNumber = request.CardNumber,
            CardExpiry = request.CardExpiry,
            CardCvv = request.CardCvv
        });

        if (!validationResult.IsValid)
        {
            if (string.Equals(validationResult.ErrorMessage, "Comanda este deja achitată.", StringComparison.Ordinal))
                return CreateResult(orderState, CalculatePreviewTotal(request), request.GiftWrapFee, paymentMessage: validationResult.ErrorMessage);

            return CreateResult(orderState, CalculatePreviewTotal(request), request.GiftWrapFee, paymentError: validationResult.ErrorMessage);
        }

        var options = new CheckoutOptions
        {
            Subtotal = request.Subtotal,
            GiftWrap = request.GiftWrapRequested,
            GiftWrapFee = request.GiftWrapRequested ? request.GiftWrapFee : 0m,
            ApplyDiscount = request.DiscountApplied,
            PaymentMethod = request.PaymentMethod
        };

        var checkoutTemplate = _checkoutTemplateFactory.GetTemplate(request.DeliveryType);
        var execution = checkoutTemplate.RunCheckout(options);

        if (string.Equals(orderState.CurrentStateName, "CheckoutStarted", StringComparison.Ordinal))
            orderState.StartPayment();
        if (string.Equals(orderState.CurrentStateName, "PaymentPending", StringComparison.Ordinal))
            orderState.ConfirmPayment();

        return CreateResult(
            orderState,
            execution.FinalTotal,
            execution.GiftWrapFeeApplied,
            paymentMessage: execution.PaymentMessage,
            paymentProcessorName: execution.PaymentProcessorName,
            checkoutTemplateName: execution.TemplateName);
    }

    private static CheckoutMediatorResult CreateResult(
        OrderStateContext orderState,
        decimal finalTotal,
        decimal giftWrapFeeApplied,
        string? paymentMessage = null,
        string? paymentError = null,
        string? paymentProcessorName = null,
        string? checkoutTemplateName = null)
    {
        return new CheckoutMediatorResult
        {
            NextOrderStateName = orderState.CurrentStateName,
            FinalTotal = finalTotal,
            GiftWrapFeeApplied = giftWrapFeeApplied,
            PaymentMessage = paymentMessage,
            PaymentError = paymentError,
            PaymentProcessorName = paymentProcessorName,
            CheckoutTemplateName = checkoutTemplateName
        };
    }

    private static decimal CalculatePreviewTotal(CheckoutMediatorRequest request)
    {
        var total = request.Subtotal + request.DeliveryCost;
        if (request.DiscountApplied)
            total *= (1 - DiscountOrderDecorator.SiteDiscountPercent / 100m);
        if (request.GiftWrapRequested)
            total += request.GiftWrapFee;
        return total;
    }
}
