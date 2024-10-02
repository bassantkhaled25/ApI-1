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
    public class ProductPictureUrlResolver : IValueResolver<Product, productDetailsDto, string >    //source => dest
    {
        private readonly IConfiguration _configuration;                //محتاج اعمل config معينه => baseURl عشان اوصل لل

        public ProductPictureUrlResolver(IConfiguration configuration)  // منه object باخد 
        {
            _configuration = configuration;                                   
        }

        //ctrl+. to intrface(ivalueresolver)
        public string Resolve(Product source, productDetailsDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))             
                return $"{ _configuration["BaseUrl"]}{source.PictureUrl}";         //concat (baseurl => in appsettings) + source))
           else
            return null;
        }
    }
}
