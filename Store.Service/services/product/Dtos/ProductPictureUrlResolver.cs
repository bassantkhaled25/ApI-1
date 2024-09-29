using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.services.product.Dtos
{
    public class ProductPictureUrlResolver : IValueResolver<Product, productDetailsDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, productDetailsDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))             
                return $"{ _configuration["BaseUrl"]}{source.PictureUrl}";         //concat (baseurl => in appsettings) + sourse))
           else
            return null;
        }
    }
}
