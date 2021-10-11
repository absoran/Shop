using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class ShoppingCart : BaseEntity
    {
        public ShoppingCart()
        {

        }
        public ShoppingCart(string id)
        {
            SId = id; 
        }
        public string SId { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
        public int? ShippingID { get; set; }

        [Column(TypeName = "decimal(18,5)")]
        public decimal ShippingPrice { get; set; }
        public string PaymentIntentId { get; set; }
        public string ClientSecret { get; set; }


        public void AddItem(int productid, int quantity = 1,decimal unitPrice = 0)
        {
            var existingItem = Items.FirstOrDefault(i => i.Product.Id == productid);

            if (existingItem != null)
            {
                existingItem.Quantity++;
                existingItem.TotalPrice = (existingItem.Quantity * existingItem.Product.Price);
            }
            else
            {
                Items.Add(new ShoppingCartItem()
                {
                    Productid = productid,
                    Quantity = quantity,
                    UnitPrice = unitPrice,
                    TotalPrice = quantity * unitPrice
                });
            }
        }
        public void RemoveItem(int cartitemid)
        {
            var itemtoremove = Items.FirstOrDefault(x => x.Id == cartitemid);
            if(itemtoremove != null)
            {
                Items.Remove(itemtoremove);
            }
            else
            {
               //toDO
            }
        }
        public void ClearCartItems()
        {
            Items.Clear();
        }
    }
}
