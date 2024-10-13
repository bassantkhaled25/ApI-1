using StackExchange.Redis;
using Store.Data.Entities.OrderEntities;
using Store.Repository.Specification;
using Order = Store.Data.Entities.OrderEntities.Order;

namespace Store.Repository.specification.OrderSpecs
{
    public class OrderWithPaymentspecification:BaseSpecification<Order>

    {

        public OrderWithPaymentspecification(string? paymentIntentId)
           : base(order => order.PaymentIntentId== paymentIntentId)
        {
        }


    }
}
