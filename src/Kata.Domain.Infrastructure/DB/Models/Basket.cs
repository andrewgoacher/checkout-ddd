using System;
using System.Collections.Generic;

namespace Kata.Domain.Infrastructure.DB.Models
{
    public class Basket
    {
        public List<Item> Items { get; set; }
        public List<Discount> Discounts { get; set; }
        public Guid Id { get; set; }
    }
}
