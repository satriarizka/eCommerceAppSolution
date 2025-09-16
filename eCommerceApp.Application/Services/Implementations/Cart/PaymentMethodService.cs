﻿using AutoMapper;
using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Application.Services.Interfaces.Cart;
using eCommerceApp.Domain.Interfaces.Cart;

namespace eCommerceApp.Application.Services.Implementations.Cart
{
    internal class PaymentMethodService(IPaymentMethod paymentMethod, IMapper mapper) : IPaymentMethodService
    {
        public async Task<IEnumerable<GetPaymentMethod>> GetPaymentMethods()
        {
            var methods = await paymentMethod.GetPaymentMethods();
            if (!methods.Any()) return [];

            return mapper.Map<IEnumerable<GetPaymentMethod>>(methods);
        }
    }
}
