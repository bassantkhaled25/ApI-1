using Store.Data.Entities;

namespace store.Data.OrderEntities
{
    public class OrderItem : BaseEntity<Guid>

    {
    
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Guid orderId { get; set; }
        public ProductItemOrdered ProductItem { get; set; }
    }
}
