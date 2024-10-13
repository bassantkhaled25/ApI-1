using AutoMapper;
using Microsoft.Extensions.Configuration;
using Services.OrderServices.Dto;
using store.Data.OrderEntities;

namespace store.Services
{
    public class OrderUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>        
    {
        private readonly IConfiguration _configuration;

        public OrderUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ProductItem.PictureUrl))
                return _configuration["BaseUrl"] + source.ProductItem.PictureUrl;

            return null;
        }
    }
}
