using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    //нет артикла
    public class SalesReportUnit
    {
        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string MarkingOfProduct { get; set; }
        public string NameProduct { get; set; }
        public short? UnitsOnOrder { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
