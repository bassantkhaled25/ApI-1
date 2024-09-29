using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Data.Entities;
using Store.Repository.specification.productSpecs;
using Store.Service.services.product;
using Store.Service.services.product.Dtos;

namespace Store.Web.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class ProductsController : ControllerBase

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
        public async Task<ActionResult<IReadOnlyList<productDetailsDto>>> GetAllProducts([FromQuery]productspecification input)
          => Ok(await _productServices.GetAllProductsAsync(input));

        [HttpGet("Get product by Id")]
        public async Task<ActionResult<productDetailsDto>> GetProductbyId(int? id)
         => Ok(await _productServices.GetProductByIdAsync(id));


    }
}
