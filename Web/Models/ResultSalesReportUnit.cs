using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class ResultSalesReportUnit
    {
        public List<string> Error;
        public List<SalesReportUnit> Report;
        public List<string> PositiveResult;

        public ResultSalesReportUnit()
        {
            Error = new List<string>();
            Report = new List<SalesReportUnit>();
            PositiveResult = new List<string>();
        }
    }
}
