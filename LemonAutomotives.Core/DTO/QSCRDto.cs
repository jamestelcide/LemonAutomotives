using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonAutomotives.Core.DTO
{
    public class QSCRDto
    {
        public Guid SalespersonID { get; set; }
        public string SalespersonName { get; set; }
        public double TotalCommission { get; set; }
        public int SalesCount { get; set; }
    }
}
