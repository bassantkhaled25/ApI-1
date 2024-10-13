using AutoMapper;
using Services.OrderServices.Dto;
using store.Data.OrderEntities;
using Store.Data.Entities.IdentityEntities;
using Store.Data.Entities.OrderEntities;

namespace store.Services
{
    public class OrderProfile : Profile                  // + register (extension)
    {
        public OrderProfile()
        {
            CreateMap <ShippingAddressDto, Address>().ReverseMap();

            CreateMap <ShippingAddressDto, ShippingAddressDto>();

            CreateMap <Order, OrderDetailsDto>()
                    .ForMember(dest => dest.DeliveryMethodName, option => option.MapFrom(src => src.DeliveryMethod.ShortName))
                    .ForMember(dest => dest.ShippingPrice, option => option.MapFrom(src => src.DeliveryMethod.Price));

            CreateMap<OrderItem, OrderItemDto>()
                    .ForMember(dest => dest.ProductItemId, option => option.MapFrom(src => src.ProductItem.ProductItemId))
                    .ForMember(dest => dest.ProductName, option => option.MapFrom(src => src.ProductItem.ProductName))
                    .ForMember(dest => dest.PictureUrl, option => option.MapFrom(src => src.ProductItem.PictureUrl))
                    .ForMember(dest => dest.PictureUrl, option => option.MapFrom<OrderUrlResolver>());
        }
    }
}
