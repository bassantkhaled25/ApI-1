
using Google.Apis.Calendar.v3.Data;
using Microsoft.AspNetCore.Mvc;
using store.Services;
using Store.Service.BasketService.Dtos;
using Store.Web.Controllers;
using Stripe;

namespace store.Web.Controllers
{

    public class PaymentController : BaseController

    {
        private readonly IPaymentServices _paymentServices;
        private readonly ILogger _logger;
        const string endpointSecret = "whsec_6aa7bf5578f08c6613fdc619ddc933393212fa7db44b753ef02df0e142ac849c";

        public PaymentController(IPaymentServices paymentServices, ILogger logger)

        {
            _paymentServices = paymentServices;
            _logger = logger;
        }


        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(CustomerBasketDto basketDto)
     
        => Ok(await _paymentServices.CreateOrUpdatePaymentIntent(basketDto));
            
        
        [HttpPost]
        public async Task<ActionResult> WebHook()

        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try

            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"],endpointSecret);

                PaymentIntent paymentIntent;
                OrderDetailsDto order; 

                                                                                  //handle events
                if (stripeEvent.Type == EventTypes.PaymentIntentPaymentFailed)

                {
                    paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment Failed ", paymentIntent.Id);
                    order = await _paymentServices.UpdateOrderPaymentFailed(paymentIntent.Id);
                    _logger.LogInformation("Order Updated to Payment Failed ", order.Id);
                }

                else if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)

                {
                    paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment Succeeded ", paymentIntent.Id);
                    order = await _paymentServices.UpdateOrderPaymentSucceeded(paymentIntent.Id);
                    _logger.LogInformation("Order Updated to Payment Succeeded ", order.Id);
                }
              
                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }

                return Ok();
            }

            catch (StripeException e)

            {
                return BadRequest();
            }
        }

    }
}
