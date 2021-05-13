using System;
using System.Threading.Tasks;
using Kata.Domain.Checkout;
using Kata.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace KataApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckoutController
    {
        private CheckoutApplicationService _appService;

        public CheckoutController(CheckoutApplicationService appService)
        {
            _appService = appService;
        }

        [HttpPost]
        public async Task<Guid> CreateBasket()
        {
            var id = await _appService.CreateBasket();
            return id;
        }

        [HttpGet]
        public async Task<Basket> GetBasket(Guid basketId)
        {
            return await _appService.GetBasket(new BasketId(basketId));
        }
    }
}
