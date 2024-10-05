using Microsoft.AspNetCore.Mvc;
using Store.Service.BasketService;
using Store.Service.BasketService.Dtos;
using Store.Web.Controllers;

namespace Demo.API.Controllers
{

    public class BasketController : BaseController
    {
        private readonly IBasketServices _basketServices;

        public BasketController(IBasketServices basketServices)
        {
            _basketServices = basketServices;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasketDto>> GetBasketById(string id)
            => Ok(await _basketServices.GetBasketAsync(id));

        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasketAsync(CustomerBasketDto basket)
            => Ok(await _basketServices.UpdateBasketAsync(basket));

        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomerBasketDto>> DeleteBasketAsync(string id)
            => Ok(await _basketServices.DeleteBasketAsync(id));
    }
}
