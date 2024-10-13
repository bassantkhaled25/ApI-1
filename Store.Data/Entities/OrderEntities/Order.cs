using store.Data.OrderEntities;

namespace Store.Data.Entities.OrderEntities
{

    public class Order : BaseEntity<Guid>
    {
     
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public ShippingAddress ShippingAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public int? DeliveryMethodId {  get; set; } 
        public OrderStatus OrderStatus { get; set; } = OrderStatus.placed;          //default
        public OrderPaymentStatus PaymentStatus { get; set; } 
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public string? basketId {  get; set; }
        public decimal SubTotal { get; set; }
        public string? PaymentIntentId { get; set; }                      //وسيط بيني وبين البنك 
        public decimal GetTotal()
            => SubTotal + DeliveryMethod.Price;
    }
}
