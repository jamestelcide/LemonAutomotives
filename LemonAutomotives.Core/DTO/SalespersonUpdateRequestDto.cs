using LemonAutomotives.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace LemonAutomotives.Core.DTO
{
    /// <summary>
    /// Represents the DTO class that contains the Salesperson details to update
    /// </summary>
    public class SalespersonUpdateRequestDto
    {
        public Guid SalespersonID { get; set; }
        
        [Required(ErrorMessage = "Salesperson first name can not be blank")]
        public string SalespersonFirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Salesperson last name can not be blank")]
        public string SalespersonLastName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Salesperson address name can not be blank")]
        public string SalespersonAddress { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Salesperson phone number can not be blank")]
        public string SalespersonPhone { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Salesperson start date can not be blank")]
        public DateTime SalespersonStartDate { get; set; }
        
        public DateTime? SalespersonTerminationDate { get; set; }

        /// <summary>
        /// Converts the current object of SalesPersonAddRequest into a new object of the Salesperson type
        /// </summary>
        /// <returns>Returns Salesperson object</returns>
        public Salesperson ToSalesperson()
        {
            return new Salesperson()
            {
                SalespersonFirstName = SalespersonFirstName,
                SalespersonLastName = SalespersonLastName,
                SalespersonAddress = SalespersonAddress,
                SalespersonPhone = SalespersonPhone,
                SalespersonStartDate = SalespersonStartDate,
                SalespersonTerminationDate = SalespersonTerminationDate
            };
        }
    }
}
