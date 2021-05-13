using System;
using System.Threading.Tasks;
using Kata.Domain.Checkout;
using Kata.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace KataApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CheckoutController
    {
        private CheckoutApplicationService _appService;

        public CheckoutController(CheckoutApplicationService appService)
        {
            _appService = appService;
        }

        /// <summary>
        /// Creates a new basket
        /// </summary>
        ///
        /// <remarks>
        /// Sample request:
        /// POST /api/checkout
        /// </remarks>
        ///
        /// <returns>A basket id</returns>
        /// <response code="200">The id of the newly created basket</response>
        [HttpPost]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Guid>> CreateBasket()
        {
            var id = await _appService.CreateBasket();
            return new OkObjectResult(id);
        }

        /// <summary>
        /// Gets a basket from the specified basket id
        /// </summary>
        ///
        /// <remarks>
        /// Sample request:
        /// POST /api/checkout/{basketId}
        /// </remarks>
        /// 
        /// <param name="basketId">The id of the basket</param>
        /// <returns>The complete basket</returns>
        /// <response code="200">The basket</response>
        /// <response code="404">The basket could not be found</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Basket>> GetBasket(Guid basketId)
        {
            var basket = await _appService.GetBasket(new BasketId(basketId));
            if (basket == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(basket);
        }
    }
}
