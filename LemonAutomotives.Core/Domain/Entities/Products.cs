using System.ComponentModel.DataAnnotations;

namespace LemonAutomotives.Core.Domain.Entities
{
    /// <summary>
    /// Domain model for Products
    /// </summary>
    public class Products
    {
        [Key]
        public string ProductID { get; set; }
        [StringLength(40)]
        public string? ProductName { get; set; }
        [StringLength(40)]
        public string? ProductManufacturer { get; set; }
        [StringLength(40)]
        public string? ProductModel {  get; set; }
        public string? ProductYear {  get; set; }
        public double? ProductPurchasePrice { get; set; }
        public int ProductQty { get; set; }
        public double ProductCommission { get; set; }

        //Navigation property to Sales
        public virtual ICollection<Sales>? Sales { get; set; }
    }
}
