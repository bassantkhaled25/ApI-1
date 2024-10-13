using Services.OrderServices.Dto;
using Store.Data.Entities;
using Store.Data.Entities.OrderEntities;

namespace store.Services
{
    public class OrderDetailsDto
    {
        public Guid Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } 
        public ShippingAddressDto ShippingAddress { get; set; }
        public string DeliveryMethodName { get; set; }                      //الاسم متغير في ال order
        public OrderPaymentStatus PaymentStatus { get; set; }
        public OrderStatus OrderStatus { get; set; } 
        public IReadOnlyList<OrderItemDto> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ShippingPrice { get; set; }                             //معنديش shippingprice in (order) entity => price at Deliverymethod
        public decimal Total { get; set; }
        public string? basketId { get; set; }
        public string? PaymentIntentId { get; set; }
    }
}
