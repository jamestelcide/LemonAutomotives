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
        [Required]
        public string SalespersonFirstName { get; set; } = string.Empty;
        [StringLength(20)]
        [Required]
        public string SalespersonLastName { get; set; } = string.Empty;
        [StringLength(200)]
        [Required]
        public string SalespersonAddress { get; set; } = string.Empty;
        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        [Required]
        public string SalespersonPhone { get; set; } = string.Empty;
        [Required]
        public DateTime SalespersonStartDate { get; set; }
        public DateTime? SalespersonTerminationDate { get; set; }

        //Navigation property to Sales
        public virtual ICollection<Sales>? Sales { get; set; }
    }
}
