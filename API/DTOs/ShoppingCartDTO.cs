using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace API.DTOs
{
    public class ShoppingCartDTO
    {
        [Required]
        public int Id { get; set; }
        public List<ShoppingCartItemDTO> Items { get; set; }
        public int? ShippingId { get; set; }
        public string ClientSecret { get; set; }
        public string PaymentIntentId { get; set; }
        public decimal ShippingPrice { get; set; }
    }
}
