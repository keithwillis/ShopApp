using System;
using System.ComponentModel.DataAnnotations;

namespace SampleShop.Model
{
    public class Item
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Brand { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
