using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SampleShop.Model
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }

        [Required]
        public virtual IDictionary<int, int> OrderItems { get; set; }
    }
}
