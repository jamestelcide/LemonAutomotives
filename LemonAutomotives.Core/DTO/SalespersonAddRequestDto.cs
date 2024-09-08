using LemonAutomotives.Core.Domain.Entities;

namespace LemonAutomotives.Core.DTO
{
    /// <summary>
    /// DTO class for adding a new Salesperson
    /// </summary>
    public class SalespersonAddRequestDto
    {
        public string? SalespersonFirstName { get; set; }
        public string? SalespersonLastName { get; set; }
        public string? SalespersonAddress { get; set; }
        public string? SalespersonPhone { get; set; }
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
