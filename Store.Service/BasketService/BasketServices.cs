using AutoMapper;
using Store.Repository.Basket;
using Store.Repository.Basket.Models;
using Store.Service.BasketService;
using Store.Service.BasketService.Dtos;

namespace Services.BasketServices
{
    public class BasketServices : IBasketServices

    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketServices(IBasketRepository basketRepository, IMapper mapper)

        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        public async Task<bool> DeleteBasketAsync(string basketId)

           => await _basketRepository.DeleteBasketAsync(basketId);

        public async Task<CustomerBasketDto> GetBasketAsync(string basketId)

        {
            var basketdata = await _basketRepository.GetBasketAsync(basketId);
            if (basketdata == null)
                return new CustomerBasketDto();                                                   //empty basket
            else

            {
                var mappedBasketData = _mapper.Map<CustomerBasketDto>(basketdata);                //Customerbasket => CustomerbasketDto
                return mappedBasketData;
            }
        }

        public async Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto basket)                //عاوز يرجعلي customerBasket

        {
            if (basket.Id == null)
                basket.Id = GenerateRandomBasketId();               

                                                                            //if id not null (already فيه باسكت وعاوز اعملها ابديت)

            var customerData = _mapper.Map<CustomerBasket>(basket);                                 //before update => هعمل مابينج ل => CustomerBasket  لان الابديت بياخد ده => customerBasket => not (Dto)

            var updatedCustomerData = await _basketRepository.UpdateBasketAsync(customerData);

            var mappedupdatedCustomerData = _mapper.Map<CustomerBasketDto>(updatedCustomerData);       //هرجع تاني اعمل مابينج ل => customerBasketDto لان ده اللي عاوزه يرجعلي ف الاخر

            return mappedupdatedCustomerData;

        }

       private string GenerateRandomBasketId()                //method to generate random basket id (if not have a basket)

       {  
          Random random = new Random();

          int randomDigits = random.Next(1000, 10000);

          return $"BS-{randomDigits}";
           
       }







    }
}
