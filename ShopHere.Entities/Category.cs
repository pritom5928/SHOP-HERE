using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopHere.Entities
{
    public class Category : BaseEntity
    {
        public string ImageUrl { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        public bool IsFeatured { get; set; }
    }
}
