using System.Collections.Generic;
using Kata.Domain.Services.Models;

namespace Kata.Domain.Services
{
    using CheckoutItem = Kata.Domain.Checkout.Item;

    public class DiscountRuleService
    {
        private readonly IEnumerable<DiscountRule> _rules;

        public DiscountRuleService()
        {
            _rules = new List<DiscountRule>
            {
                new ThreeAFor130DiscountRule(),
                new TwoBFor45DiscountRule()
            };
        }

        public IEnumerable<Discount> ApplyDiscounts(IEnumerable<CheckoutItem> items)
        {
            var discounts = new List<Discount>();
            foreach (var rule in _rules)
            {
                discounts.AddRange(rule.Apply(items));
            }

            return discounts;
        }
    }
}
