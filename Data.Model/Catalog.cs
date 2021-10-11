using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Model
{
    public class Catalog : BaseModel
    {
        public Catalog()
        {
            Products = new List<Product>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
