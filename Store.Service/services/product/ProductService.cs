using AutoMapper;
using Infrastructure.Specification;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Store.Data.Entities;
using Store.Repository.specification.productSpecs;
using Store.Service.Helper;
using Store.Service.services.product.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.services.product
{
    public class ProductService : IProductService

    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IMapper _mapper;

     
        public ProductService(IUnitOfWork unitOfWork , IMapper mapper)                 //ingect IUnitOfWork (has instance from rep and function to save operations)

        {
            _UnitOfWork = unitOfWork;                                                   //inject Imapper
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<BrandTybeDetailsDto>> GetAllProductBrandAsync()
        {
            var brands = await _UnitOfWork.Repository<ProductBrand, int>().GetAllAsync();      //mapping => productbrand => BrandTybeDetailsDto

            var mappedBrands=_mapper.Map<IReadOnlyList<BrandTybeDetailsDto>>(brands);    //des - from

            //var mappedBrands = brands.Select(x => new BrandTybeDetailsDto

            //{
            //    id = x.Id,
            //    name = x.Name,
            //    CreatedAt = x.createdAt

            //}).ToList();

            return mappedBrands;

        }
        public async Task<Pagination<productDetailsDto>> GetAllProductsAsync(productspecification input)

        {
            var specs = new productWithBrandAndTypeSpecification(input);

            var products = await _UnitOfWork.Repository<Product, int>().GetAllWithSpecificationAsync(specs);      

            var countspecs = new ProductWithCountSpecification(input);

            var count = await _UnitOfWork.Repository<Product, int>().GetCountSpecificationAsync(countspecs);

            var mappedproducts = _mapper.Map<IReadOnlyList<productDetailsDto>>(products);                             //product => productDetailsDto

            //var mappedproducts = products.Select(x => new productDetailsDto

            //{
            //    Id = x.Id,
            //    Name = x.Name,
            //    PictureUrl = x.PictureUrl,
            //    Description = x.Description,
            //    CreatedAt = x.createdAt,
            //    Price = x.Price,
            //    BrandName = x.Brand.Name,
            //    TypeName = x.Type.Name


            //}).ToList();

            return new Pagination<productDetailsDto>(input.PageIndex,input.PageSize,count,mappedproducts);

        }

        public async Task<IReadOnlyList<BrandTybeDetailsDto>> GetAllProductTypeAsync()

        {
           

            var types = await _UnitOfWork.Repository<ProductType, int>().GetAllAsync();       //types => BrandTybeDetailsDto

            var mappedtypes = _mapper.Map<IReadOnlyList<BrandTybeDetailsDto>>(types);

            //var mappedtypes = types.Select(x => new BrandTybeDetailsDto

            //{

            //    id = x.Id,
            //    name = x.Name,
            //    CreatedAt = x.createdAt

            //}).ToList();

            return mappedtypes;
        }


        public async Task<productDetailsDto> GetProductByIdAsync(int? id)

        {
            if (id == null)
                throw new Exception("Id Is Null");

            var specs = new productWithBrandAndTypeSpecification(id);


            var product = await _UnitOfWork.Repository<Product, int>().GetWithSpecificationByIdAsync(specs);

            if (product == null)
                throw new Exception("Product Not Found");


            var mappedproduct = _mapper.Map<productDetailsDto>(product);

            //var mappedproduct = new productDetailsDto

            //{
            //    Id = product.Id,
            //    Name = product.Name,
            //    CreatedAt = product.createdAt,
            //    Price = product.Price,
            //    BrandName = product.Brand.Name,
            //    TypeName = product.Type.Name,
            //    PictureUrl = product.PictureUrl,


            //};

            return mappedproduct;

        }

      
    }
}
