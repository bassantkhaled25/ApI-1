﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using store.Web;
using Store.Repository.specification.productSpecs;
using Store.Service.services.product;
using Store.Service.services.product.Dtos;

namespace Store.Web.Controllers
{
    [Authorize]
    public class ProductsController : BaseController

    {

        private readonly IProductService _productServices;

        public ProductsController(IProductService productServices)            //inject IproducrService (all methods are get)

        {
            _productServices = productServices;
        }

        [HttpGet("Get All Brands")]
        public async Task<ActionResult<IReadOnlyList<BrandTybeDetailsDto>>> GetAllBrands()

           => Ok(await _productServices.GetAllProductBrandAsync());


        [HttpGet("Get All Types")]
        public async Task<ActionResult<IReadOnlyList<BrandTybeDetailsDto>>> GetAlltypes()
           => Ok(await _productServices.GetAllProductBrandAsync());


        [HttpGet("Get All Products")]
        [Cache(10)]
        public async Task<ActionResult<IReadOnlyList<productDetailsDto>>> GetAllProducts([FromQuery]productspecification input)
          => Ok(await _productServices.GetAllProductsAsync(input));

        [HttpGet("Get product by Id")]
        public async Task<ActionResult<productDetailsDto>> GetProductbyId(int? id)
         => Ok(await _productServices.GetProductByIdAsync(id));


    }
}
