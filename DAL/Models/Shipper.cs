using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Shipper
    {
        public Shipper()
        {
            Order = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public byte[] RowTimeStamp { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
