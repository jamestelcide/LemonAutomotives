using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonAutomotives.Core.Domain.Entities
{
    /// <summary>
    /// Domain model for Products
    /// </summary>
    public class Products
    {
        [Key]
        public Guid ProductID { get; set; }
        [StringLength(40)]
        public string? ProductName { get; set; }
        [StringLength(40)]
        public string? ProductManufacturer { get; set; }
        [StringLength(40)]
        public string? ProductModel {  get; set; }
        public double? ProductPurchasePrice { get; set; }
        public double? ProductSalePrice { get; set; }
        public int ProductQty { get; set; }
        public double ProductCommission { get; set; }

        //Navigation property to Discount
        public virtual Discount? Discount { get; set; }

        //Navigation property to Sales
        public virtual ICollection<Sales>? Sales { get; set; }
    }
}
