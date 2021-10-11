using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities.Order
{
    public class ItemOrdered
    {
        public ItemOrdered()
        {

        }
        public ItemOrdered(int Itemid,string productname,string imgurl)
        {
            ProductItemId = Itemid;
            ProductName = productname;
            ImgUrl = imgurl;
        }
        //[ForeignKey("ProductItemID")]
        [Key]
        public int ProductItemId { get; set; }
        public string ProductName { get; set; }
        public string ImgUrl { get; set; }
    }
}
