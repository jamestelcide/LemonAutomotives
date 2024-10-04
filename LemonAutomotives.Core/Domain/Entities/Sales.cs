using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public double PriceSold { get; set; }
        public double Commission { get; set; }
        public double CommissionEarnings { get; set; }
        
        //Foreign Key pointing to Product
        public string ProductID { get; set; }
        //Foreign Key pointing to Salesperson
        public string SalespersonID { get; set; }
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
