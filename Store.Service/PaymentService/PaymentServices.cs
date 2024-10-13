using AutoMapper;
using Microsoft.Extensions.Configuration;
using store.Services;
using Store.Data.Entities;
using Store.Data.Entities.OrderEntities;
using Store.Repository.specification.OrderSpecs;
using Store.Service.BasketService;
using Store.Service.BasketService.Dtos;
using Stripe;


namespace Services.PaymentServices
{
    public class PaymentServices : IPaymentServices

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketServices _basketServices ;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public PaymentServices(IUnitOfWork unitOfWork, IBasketServices basketServices, IConfiguration config , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _basketServices = basketServices;
            _config = config;
            _mapper = mapper;
        }

        public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(CustomerBasketDto basket)
        {
            StripeConfiguration.ApiKey = _config["Stripe:SecretKey"];

            if (basket == null)
                throw new Exception("Basket is Empty");

            var deliveryMethod= await _unitOfWork.Repository<DeliveryMethod,int>().GetByIdAsync(basket.DeliveryMethodId);

            if (deliveryMethod == null)
                throw new Exception("Delivery method Not provided");

           decimal shippingPrice = deliveryMethod.Price;
                                                                               // for each => عشان اضمن ان السعر اللي ف الباسكت هو السعر بتاع البرودكت
         
            foreach (var item in basket.BasketItems)

            {
                var productItem = await _unitOfWork.Repository<Store.Data.Entities.Product,int>().GetByIdAsync(item.Id);
                if(item.Price != productItem.Price)
                    item.Price = productItem.Price;
            }

            var service = new PaymentIntentService();           //هاخد object منها
 
            PaymentIntent intent;

            if (string.IsNullOrEmpty(basket.PaymentIntentId))

            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)basket.BasketItems.Sum(item => item.Quantity * (item.Price * 100) * (long)(shippingPrice * 100)),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                intent = await service.CreateAsync(options);
                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;                          //update basket => at redis
            }

            else                                                                  //update لو مش هيدخل ع ال => if

            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)basket.BasketItems.Sum(item => item.Quantity * (item.Price * 100) * (long)(shippingPrice * 100))
                };
                await service.UpdateAsync(basket.PaymentIntentId ,options);
            }

            await _basketServices.UpdateBasketAsync(basket);

            return basket;
        }



        public async Task<OrderDetailsDto> UpdateOrderPaymentFailed(string paymentIntentId)

        {
            var specs = new OrderWithPaymentspecification(paymentIntentId);

            var order = await _unitOfWork.Repository<Order,Guid>().GetWithSpecificationByIdAsync(specs);

            if (order == null)
              throw new Exception("Order Does Not Exist");

            order.PaymentStatus = OrderPaymentStatus.failed;

            _unitOfWork.Repository<Order,Guid>().Update(order);

            await _unitOfWork.CompeleteAsync();

            var mappedOrder = _mapper.Map<OrderDetailsDto>(order);
            return mappedOrder;
        }

        public async Task<OrderDetailsDto> UpdateOrderPaymentSucceeded(string paymentIntentId)

        {
            var specs = new OrderWithPaymentspecification(paymentIntentId);

            var order = await _unitOfWork.Repository<Order, Guid>().GetWithSpecificationByIdAsync(specs);

            if (order == null)
                throw new Exception("Order Does Not Exist");

            order.PaymentStatus = OrderPaymentStatus.received;

            _unitOfWork.Repository<Order, Guid>().Update(order);

            await _unitOfWork.CompeleteAsync();

            await _basketServices.DeleteBasketAsync(order.basketId);

            var mappedOrder = _mapper.Map<OrderDetailsDto>(order);

            return mappedOrder;

        }
    }
}
