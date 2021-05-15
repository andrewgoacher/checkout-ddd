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
        /// <param name="basketId">The id of the basket</param>
        /// <returns>The complete basket</returns>
        /// <response code="200">The basket</response>
        /// <response code="400">There was a validation error</response>
        /// <response code="404">The basket could not be found</response>
        [HttpGet("{basketId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Basket>> GetBasket([FromRoute] Guid basketId)
        {
            return await _appService.GetBasket(new BasketId(basketId));
        }

        /// <summary>
        /// Adds an item with a specified quantity to the specified basket
        /// </summary>
        ///
        /// <remarks>
        /// 
        /// <param name="basketId">The basket to add the item to</param>
        /// <param name="addItem">
        /// Contains the item id of the item to add and a quantity of the item to add
        /// </param>
        ///
        /// <response code="200">Returns OK if the item was added</response>
        /// <response code="400">A validation error occured when trying to add the item to the basket</response>
        /// <response code="404">Returns not found if the basket could not be found</response>
        /// 
        /// <returns></returns>
        [HttpPost("{basketId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> AddItem([FromRoute] Guid basketId, [FromBody] AddItemCmd addItem)
        {
            await _appService.AddItemAsync(new (basketId), new (addItem.ItemId), new (addItem.Quantity));
            return new OkResult();
        }
    }

    public class AddItemCmd
    {
        public string ItemId { get; set; }
        public int Quantity { get; set; }
    }
}
