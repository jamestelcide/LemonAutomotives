using LemonAutomotives.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace LemonAutomotives.Core.DTO
{
    /// <summary>
    /// DTO class for adding a new Salesperson
    /// </summary>
    public class SalespersonAddRequestDto
    {
        public string SalespersonFirstName { get; set; } = string.Empty;
        public string SalespersonLastName { get; set; } = string.Empty;
        public string SalespersonAddress { get; set; } = string.Empty;
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Phone Number.")]
        public string SalespersonPhone { get; set; } = string.Empty;
        public DateTime SalespersonStartDate { get; set; }

        public Salesperson ToSalesperson()
        {
            return new Salesperson()
            {
                SalespersonFirstName = SalespersonFirstName,
                SalespersonLastName = SalespersonLastName,
                SalespersonAddress = SalespersonAddress,
                SalespersonPhone = SalespersonPhone,
                SalespersonStartDate = SalespersonStartDate
            };
        }
    }
}
