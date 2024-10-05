using System.ComponentModel.DataAnnotations;

namespace Store.Service.BasketService.Dtos
{
    public class CustomerBasketDto
    {
      
        public string? Id { get; set; }
        public List<BasketItemDto> BasketItems { get; set; } = new List<BasketItemDto>();
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }

        //public string? PaymentIntentId { get; set; }
        //public string? ClientSecret { get; set; }
    }
}