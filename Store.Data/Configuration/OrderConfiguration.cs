using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Data.Entities.OrderEntities;

namespace store.Data
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>

    {
        public void Configure(EntityTypeBuilder<Order> builder)

        {
            builder.OwnsOne(order => order.ShippingAddress, x => 
            {
                x.WithOwner();
            });
                                                                                   //لو مسحت الاوردر فال => orderitems يتمسحوا
            builder.HasMany(x => x.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
