using AutoMapper;
using Domain.Contracts;
using Domain.Entites.Orders;
using Domain.Exceptions.NotFound;
using Microsoft.Extensions.Configuration;
using Services.Abstractions.Payments;
using Shared.Dtos.Baskets;
using Stripe;
using Product = Domain.Entites.Products.Product;

namespace Services.Payments
{
    public class PaymentService(IBasketRepository _basketRepository, IUnitOfWork _unitOfWork, IConfiguration _configuration, IMapper _mapper) : IPaymentService
    {
        public async Task<BasketDto> CreatePaymentIntent(string BasketId)
        {
            // Calculate Amount = SubTotal + Delivery Method Cost

            // Get the basket from the repository by the provided BasketId
            var basket = await _basketRepository.GetBasketAsync(BasketId);

            if (basket is null) throw new BasketNotFoundExeption(BasketId);

            // Loop through the items in the basket and get the latest price for each item from the database
            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.GetRepository<int, Product>().GetAsync(item.Id);

                if (product is null) throw new ProductNotFountException(item.Id);

                item.Price = product.Price;
            }

            // Calculate the sub total for the basket
            var subTotal = basket.Items.Sum(I => I.Price * I.Quantity);

            // Get the delivery method cost if the delivery method is selected

            if (!basket.DeliveryMethodId.HasValue) throw new DeliveryMethodNotFoundExeption(-1);

            var deliveryMethod = await _unitOfWork.GetRepository<int, DeliveryMethod>().GetAsync(basket.DeliveryMethodId.Value);
            if (deliveryMethod is null) throw new DeliveryMethodNotFoundExeption(basket.DeliveryMethodId.Value);

            // Set the shipping cost for the basket
            basket.ShippingCost = deliveryMethod.Price;
            // Calculate the total amount for the payment intent
            var amount = subTotal + deliveryMethod.Price;

            //Send the amount to Stripe and create a payment intent

            StripeConfiguration.ApiKey = _configuration["Stripe:Secret"];

            PaymentIntentService paymentIntentService = new PaymentIntentService();

            PaymentIntent paymentIntent;

            if (basket.PaymentIntentId is null)
            {
                //create a new payment intent
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)amount * 100, // Stripe expects the amount in cents
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "card" }
                };
                paymentIntent = await paymentIntentService.CreateAsync(options);
            }
            else
            {
                //update the existing payment intent
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)amount * 100, // Stripe expects the amount in cents
                };
                paymentIntent = await paymentIntentService.UpdateAsync(basket.PaymentIntentId, options);
            }

            basket.PaymentIntentId = paymentIntent.Id;
            basket.ClientSecret = paymentIntent.ClientSecret;

            basket = await _basketRepository.CreateBasketAsync(basket, TimeSpan.FromDays(1));

            return _mapper.Map<BasketDto>(basket);
        }
    }
}
