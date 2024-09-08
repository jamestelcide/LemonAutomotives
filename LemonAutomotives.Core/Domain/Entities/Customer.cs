using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonAutomotives.Core.Domain.Entities
{
    /// <summary>
    /// Domain model for Customers
    /// </summary>
    public class Customer
    {
        [Key]
        public Guid CustomerID { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Please enter a valid First Name")]
        public string? CustomerFirstName { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Please enter a valid Last Name")]
        public string? CustomerLastName { get; set; }

        [StringLength(200)]
        [Required(ErrorMessage = "Please enter a valid Address")]
        public string? CustomerAddress {  get; set; }
        
        [RegularExpression(@"^\+?\d{0,15}$", ErrorMessage = "Please enter a valid phone number.")]
        [Required(ErrorMessage = "Please enter a valid Phone Number")]
        public string? CustomerPhone { get; set; }

        [Required(ErrorMessage = "Please enter a valid Start Date")]
        public DateTime CustomerStartDate { get; set; }

        //Navigation property to Sales
        public virtual ICollection<Sales>? Sales { get; set; }
    }
}
