using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Proiect__de_an.Models;
using Proiect__de_an.Services;

namespace Proiect__de_an.Core.Lab5.Proxy
{
    /// <summary>
    /// Proxy: controlează accesul la coș (audit/logging) înainte de delegare la serviciul real.
    /// Clientul folosește ICartService; poate primi fie CartService, fie acest Proxy.
    /// </summary>
    public class CartServiceProxy : ICartService
    {
        private readonly ICartService _subject;
        private readonly ILogger<CartServiceProxy> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        // Restricții demo (Protection Proxy)
        private const int GuestMaxQuantityPerItem = 10;

        public CartServiceProxy(ICartService subject, ILogger<CartServiceProxy> logger, IHttpContextAccessor httpContextAccessor)
        {
            _subject = subject ?? throw new ArgumentNullException(nameof(subject));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public void AddItem(string id, string name, decimal price, int quantity = 1)
        {
            var q = quantity < 1 ? 1 : quantity;
            if (price < 0)
            {
                _logger.LogWarning("[CartProxy][Denied] AddItem blocked (negative price). id={Id} name={Name} price={Price} qty={Qty}",
                    id, name, price, q);
                return;
            }

            if (!IsAdmin() && q > GuestMaxQuantityPerItem)
            {
                _logger.LogWarning("[CartProxy][Restricted] Quantity capped for guest. id={Id} requestedQty={Requested} cappedTo={Capped}",
                    id, q, GuestMaxQuantityPerItem);
                q = GuestMaxQuantityPerItem;
            }

            Log($"AddItem(id: {id}, name: {name}, price: {price}, qty: {q})");
            _subject.AddItem(id, name, price, q);
        }

        public void RemoveAt(int index)
        {
            Log($"RemoveAt(index: {index})");
            _subject.RemoveAt(index);
        }

        public void RemoveProductQuantity(string productId, int quantity)
        {
            Log($"RemoveProductQuantity(productId: {productId}, quantity: {quantity})");
            _subject.RemoveProductQuantity(productId, quantity);
        }

        public void SetDeliveryType(string type)
        {
            // Demo restrictionare: Express doar pentru utilizatori autentificați
            if (string.Equals(type, "Express", StringComparison.OrdinalIgnoreCase) && !IsAuthenticated())
            {
                _logger.LogWarning("[CartProxy][Denied] SetDeliveryType blocked (Express requires authenticated user).");
                _subject.SetDeliveryType("Standard");
                return;
            }

            Log($"SetDeliveryType(type: {type})");
            _subject.SetDeliveryType(type);
        }

        public CartViewModel GetCartViewModel()
        {
            Log("GetCartViewModel()");
            return _subject.GetCartViewModel();
        }

        private void Log(string message)
        {
            _logger.LogInformation("[CartProxy] {Message}", message);
        }

        private bool IsAuthenticated()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.Identity?.IsAuthenticated == true;
        }

        private bool IsAdmin()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.IsInRole("Admin") == true;
        }
    }
}
