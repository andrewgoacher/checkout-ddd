using Kata.Domain.Checkout;
using Kata.Domain.Shared;

namespace Kata.Domain.Services.Models
{
    public class Discount
    {
        public Discount()
        {
           
        }

        public DiscountId Id { get; set; }
        public Money Amount { get; set; }
        public Description Description { get; set; }
    }
}
