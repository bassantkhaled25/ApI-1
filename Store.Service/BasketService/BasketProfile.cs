using AutoMapper;
using Store.Repository.Basket.Models;
using Store.Service.BasketService.Dtos;

namespace Services.BasketServices
{
    public class BasketProfile : Profile

    {
        public BasketProfile()

        {
            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItems, BasketItemDto>().ReverseMap();
        }
    }
}
