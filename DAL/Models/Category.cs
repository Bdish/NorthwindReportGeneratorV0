using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Category
    {
        public Category()
        {
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
        public byte[] RowTimeStamp { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
