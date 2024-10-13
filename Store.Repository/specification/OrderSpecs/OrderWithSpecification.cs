using Store.Data.Entities.OrderEntities;
using Store.Repository.Specification;

namespace store.Repository
{
    public class OrderWithSpecification : BaseSpecification<Order>
    {
        public OrderWithSpecification(string buyerEmail) 
                : base(order => order.BuyerEmail == buyerEmail)
        {
            AddInClude(order => order.DeliveryMethod);              //incase return list
            AddInClude(order => order.OrderItems);
            AddOrderByDescending(order => order.OrderDate);
        }

        public OrderWithSpecification(Guid id)                        //incase return one
                : base(order => order.Id == id)
        {
            AddInClude(order => order.DeliveryMethod);
            AddInClude(order => order.OrderItems);
            
        }
    }
}
