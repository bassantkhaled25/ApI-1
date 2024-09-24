using AutoMapper;
using Store.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.services.product.Dtos
{
    public class ProductProfile : Profile                      //auto mapper 
    {
       public ProductProfile()                                      
                                                         //product => productDetailsDto
       {
                                                                                   //automapper=>  فلازم الاسماء تبقي واحده عشان يقدر يعمل مابينج => بعرفه ان دول مختلفين كاسماء وهعملهم مابينج
            CreateMap<Product, productDetailsDto>()                               //destination (to) .. from()
                .ForMember(dest => dest.TypeName, options => options.MapFrom(src => src.Type.Name))
                .ForMember(dest => dest.BrandName, options => options.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.PictureUrl, options => options.MapFrom<ProductPictureUrlResolver>());

            CreateMap<ProductBrand, BrandTybeDetailsDto>();
            CreateMap<ProductType, BrandTybeDetailsDto>();

       }




    }


}










