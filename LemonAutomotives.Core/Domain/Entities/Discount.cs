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
    /// Domain model for Discount
    /// </summary>
    public class Discount
    {
        [Key]
        public Guid DiscountID { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public double DiscountPercentage { get; set; }

        //Foreign Key pointing to Product
        public Guid ProductID { get; set; }

        [ForeignKey("ProductID")]
        public virtual Products? Products { get; set; }
    }
}
