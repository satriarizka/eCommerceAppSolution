using AutoMapper;
using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Application.Services.Interfaces.Cart;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Domain.Interfaces.Cart;

namespace eCommerceApp.Application.Services.Implementations.Cart
{
    public class CartService(ICart cartInterface, IMapper mapper, IGeneric<Product> productInterface
        , IPaymentMethodService paymentMethodService, IPaymentService paymentService) : ICartService
    {
        public async Task<ServiceResponse> Checkout(Checkout checkout, string userId)
        {
            var (products, totalAmount) = await GetCartTotalAmount(checkout.Carts);
            var paymentMethods = await paymentMethodService.GetPaymentMethods();

            if (checkout.PaymentMethodId == paymentMethods.FirstOrDefault()!.Id)
                return await paymentService.Pay(totalAmount, products, checkout.Carts);
            else
                return new ServiceResponse(false, "Invalid payment method");
        }

        public async Task<ServiceResponse> SaveCheckoutHistory(IEnumerable<CreateAchieve> achieves)
        {
            var mappedData = mapper.Map<IEnumerable<Achieve>>(achieves);
            var result = await cartInterface.SaveCheckoutHistory(mappedData);

            return result > 0
                ? new ServiceResponse(true, "Checkout achieved")
                : new ServiceResponse(false, "Error occurred in saving");
        }

        private async Task<(IEnumerable<Product>, decimal)> GetCartTotalAmount(IEnumerable<ProcessCart> carts)
        {
            if (!carts.Any()) return (Enumerable.Empty<Product>(), 0);

            var products = await productInterface.GetAllAsync();
            if (!products.Any()) return (Enumerable.Empty<Product>(), 0);

            var cartProducts = carts
                .Select(cartItem => products.FirstOrDefault(p => p.Id == cartItem.ProductId))
                .Where(product => product != null)
                .Cast<Product>() // buang kemungkinan null
                .ToList();

            var totalAmount = carts
                .Where(cartItem => cartProducts.Any(p => p.Id == cartItem.ProductId))
                .Sum(cartItem =>
                {
                    var product = cartProducts.FirstOrDefault(p => p.Id == cartItem.ProductId);
                    return product != null ? cartItem.Quantity * product.Price : 0;
                });

            return (cartProducts, totalAmount);
        }

    }
}
