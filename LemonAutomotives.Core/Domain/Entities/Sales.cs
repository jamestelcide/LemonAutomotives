using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonAutomotives.Core.Domain.Entities
{
    /// <summary>
    /// Domain model for Sales
    /// </summary>
    public class Sales
    {
        [Key]
        public Guid SaleID { get; set; }
        public DateTime SalesDate { get; set; }

        
        //Foreign Key pointing to Product
        public Guid ProductID { get; set; }
        //Foreign Key pointing to Salesperson
        public Guid SalespersonID { get; set; }
        //Foreign Key pointing to Customer
        public Guid CustomerID { get; set; }
        

        [ForeignKey("ProductID")]
        public virtual Products? Products { get; set; }

        [ForeignKey("SalespersonID")]
        public virtual Salesperson? Salesperson { get; set; }

        [ForeignKey("CustomerID")]
        public virtual Customer? Customer { get; set; }
    }
}
