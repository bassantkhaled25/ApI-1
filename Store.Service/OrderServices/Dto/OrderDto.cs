using Services.OrderServices.Dto;

namespace store.Services
{
    public class OrderDto

    {
        public string BasketId { get; set; }
        public string BuyerEmail { get; set; }
        public int DeliveryMethodId { get; set; }
        public ShippingAddressDto ShippingAddress { get; set; }
    }
}
