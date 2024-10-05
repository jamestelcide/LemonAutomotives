using System.ComponentModel.DataAnnotations;

namespace LemonAutomotives.Core.Domain.Entities
{
    /// <summary>
    /// Domain model for Customers
    /// </summary>
    public class Customer
    {
        [Key]
        public string CustomerID { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Please enter a valid First Name")]
        public string CustomerFirstName { get; set; } = string.Empty;

        [StringLength(20)]
        [Required(ErrorMessage = "Please enter a valid Last Name")]
        public string CustomerLastName { get; set; } = string.Empty;

        [StringLength(200)]
        [Required(ErrorMessage = "Please enter a valid Address")]
        public string CustomerAddress {  get; set; } = string.Empty;

        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        [Required(ErrorMessage = "Please enter a valid Phone Number")]
        public string CustomerPhone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a valid Start Date")]
        public DateTime? CustomerStartDate { get; set; }

        //Navigation property to Sales
        public virtual ICollection<Sales>? Sales { get; set; }
    }
}
