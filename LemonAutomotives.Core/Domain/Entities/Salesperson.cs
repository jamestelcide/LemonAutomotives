using System.ComponentModel.DataAnnotations;

namespace LemonAutomotives.Core.Domain.Entities
{
    /// <summary>
    /// Domain model for Salesperson
    /// </summary>
    public class Salesperson
    {
        [Key]
        public Guid SalespersonID { get; set; }
        [StringLength(20)]
        public string? SalespersonFirstName { get; set; }
        [StringLength(20)]
        public string? SalespersonLastName { get; set; }
        [StringLength(200)]
        public string? SalespersonAddress { get; set; }
        [RegularExpression(@"^\+?\d{0,15}$", ErrorMessage = "Please enter a valid phone number.")]
        public string? SalespersonPhone { get; set; }
        public DateTime SalespersonStartDate { get; set; }
        public DateTime? SalespersonTerminationDate { get; set; }

        //Navigation property to Sales
        public virtual ICollection<Sales>? Sales { get; set; }
    }
}
