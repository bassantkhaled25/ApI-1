using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using store.Data.OrderEntities;

namespace store.Data
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)

        {
            builder.OwnsOne(orderItem => orderItem.ProductItem , x =>
            {
                x.WithOwner();
            });
        }
    }
}
