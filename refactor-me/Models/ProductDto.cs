using System;
using Newtonsoft.Json;

namespace refactor_me.Models
{
    
    public class ProductDto : BaseDto
    {
        public decimal Price { get; set; }
        public decimal DeliveryPrice { get; set; }
    }
}