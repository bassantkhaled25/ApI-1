using AutoMapper;
using Services.OrderServices.Dto;
using StackExchange.Redis;
using store.Data.OrderEntities;
using store.Repository;
using store.Services;
using Store.Data.Entities;
using Store.Data.Entities.OrderEntities;
using Store.Service.BasketService;
using Order = Store.Data.Entities.OrderEntities.Order;

namespace Store.Service
{
    public class OrderService : IOrderService


    {
        private readonly IBasketServices _basketService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
     
     
        public OrderService(IBasketServices basketService, IUnitOfWork unitOfWork , IMapper mapper)

        {
            _basketService = basketService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;   
            
        }

        public async Task<OrderDetailsDto> CreateOrderAsync(OrderDto orderDto)

        {
            //Get Basket

            var basket = await _basketService.GetBasketAsync(orderDto.BasketId);
            if (basket == null)
                throw new Exception("Basket Not Exist");

            var OrderItems = new List<OrderItemDto>();

            //fill order item list with items in the Basket

            foreach (var item in basket.BasketItems)

            {
                var productItem = await _unitOfWork.Repository<Product, int>().GetByIdAsync(item.Id);

                if (productItem == null)

                    throw new Exception($"product with Id :{item.Id} Not Exist");

                var itemOrdered = new ProductItemOrdered

                {
                    PictureUrl = productItem.PictureUrl,
                    ProductName = productItem.Name,
                    ProductItemId = productItem.Id,

                };

                var orderitem = new OrderItem
                {
                    Price = productItem.Price,
                    Quantity = item.Quantity,
                    ProductItem = itemOrdered


                };

                var mappedOrderItem = _mapper.Map<OrderItemDto>(orderitem);
                OrderItems.Add(mappedOrderItem);
            }
                //get delivery method

                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId);
            
                if (deliveryMethod == null)
                    throw new Exception("Delivery Method Not Provided");

                //calculate subtotal

                var subTotal = OrderItems.Sum(item => item.Price * item.Quantity);

                //create order

                var mappedShippingaddress = _mapper.Map<ShippingAddress>(orderDto.ShippingAddress);
                var mappedorderitems = _mapper.Map<List<OrderItem>>(OrderItems);
                var Order = new Order
                {
                    DeliveryMethodId = deliveryMethod.Id,
                    ShippingAddress = mappedShippingaddress,
                    BuyerEmail = orderDto.BuyerEmail,
                    basketId = orderDto.BasketId,
                    OrderItems = mappedorderitems,
                    SubTotal = subTotal

                };


                await _unitOfWork.Repository<Order, Guid>().AddAsync(Order);
                await _unitOfWork.CompeleteAsync();
                var mappedorder = _mapper.Map<OrderDetailsDto>(Order);
                return mappedorder;
            

        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodAsync()

         => await _unitOfWork.Repository<DeliveryMethod, int>().GetAllAsync();

        public async Task<IReadOnlyList<OrderDetailsDto>> GetAllOrderForUserAsync(string buyerEmail)

        {
            var specs = new OrderWithSpecification(buyerEmail);

            var orders = await _unitOfWork.Repository<Order,Guid>().GetAllWithSpecificationAsync(specs);

            if (!orders.Any())

                throw new Exception("You Donot Have Any Orders Yet!!");

            var mappedOrders = _mapper.Map<List<OrderDetailsDto>>(orders);

            return mappedOrders;
        }


        public async Task<OrderDetailsDto> GetOrderByIdAsync(Guid id)

        {
            var specs = new OrderWithSpecification(id);

            var order = await _unitOfWork.Repository<Order,Guid>().GetWithSpecificationByIdAsync(specs);

            if (order==null)

              throw new Exception($"There is No Order with id {id}");

            var mappedOrder = _mapper.Map<OrderDetailsDto>(order);

            return mappedOrder;
        }
    }
}
