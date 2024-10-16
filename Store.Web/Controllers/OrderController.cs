using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using store.Services;
using Store.Data.Entities;
using Store.Service.HandleResponses;
using Store.Web.Controllers;
using System.Security.Claims;

namespace store.Web.Controllers
{
   
    public class OrderController : BaseController

    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)

        {
            _orderService = orderService;
        }

        [HttpPost]                                    //end points
        public async Task<ActionResult<OrderDetailsDto>> CreateOrderAsync(OrderDto orderDto)

        {
            var order = await _orderService.CreateOrderAsync(orderDto);


            if (order == null)

                return BadRequest(new Response (400, "Error While Creating the order !!"));

            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderDetailsDto>>> GetAllOrderForUserAsync(string buyerEmail)

        {
            var email = User?.FindFirstValue(ClaimTypes.Email);

            var order = await _orderService.GetAllOrderForUserAsync(email);

            return Ok(order);
        }


        [HttpGet]
        public async Task<ActionResult<OrderDetailsDto>> GetOrderByIdAsync(Guid id)

        {
            var email = User?.FindFirstValue(ClaimTypes.Email);

            var order = await _orderService.GetOrderByIdAsync(id,email);

            return Ok(order);
        }


        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetAllDeliveryMethodAsync()

            => Ok(await _orderService.GetAllDeliveryMethodAsync());
    }
}
