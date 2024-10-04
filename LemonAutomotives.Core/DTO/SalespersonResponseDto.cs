using LemonAutomotives.Core.Domain.Entities;

namespace LemonAutomotives.Core.DTO
{
    /// <summary>
    /// DTO class that is used as return type for most SalespersonService methods
    /// </summary>
    public class SalespersonResponseDto
    {
        public string SalespersonID { get; set; } = string.Empty;
        public string? SalespersonFirstName { get; set; }
        public string? SalespersonLastName { get; set; }
        public string? SalespersonAddress { get; set; }
        public string? SalespersonPhone { get; set; }
        public DateTime SalespersonStartDate { get; set; }
        public DateTime? SalespersonTerminationDate { get; set; }

        //Compares current object to another object of SalespersonResponse type and returns true, if both values are the same; otherwise it will return false
        public override bool Equals(object? obj)
        {
            if (obj == null) {return false;}

            if (obj.GetType() != typeof(SalespersonResponseDto)) {return false;}

            SalespersonResponseDto salespersonToCompare = (SalespersonResponseDto)obj;

            return SalespersonID == salespersonToCompare.SalespersonID;
        }

        //Returns a unique key for the current object
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public SalespersonUpdateRequestDto ToSalespersonUpdateRequest()
        {
            if (SalespersonFirstName == null)
            {
                throw new InvalidOperationException("First Name cannot be null");
            }
            if (SalespersonLastName == null)
            {
                throw new InvalidOperationException("Last Name cannot be null");
            }
            if (SalespersonAddress == null)
            {
                throw new InvalidOperationException("Address cannot be null");
            }
            if (SalespersonPhone == null)
            {
                throw new InvalidOperationException("Phone Number cannot be null");
            }

            return new SalespersonUpdateRequestDto()
            {
                SalespersonID = SalespersonID,
                SalespersonFirstName = SalespersonFirstName,
                SalespersonLastName = SalespersonLastName,
                SalespersonAddress = SalespersonAddress,
                SalespersonPhone = SalespersonPhone,
                SalespersonStartDate = SalespersonStartDate,
                SalespersonTerminationDate = SalespersonTerminationDate
            };
        }
    }
    public static class SalespersonExtension
    {
        //Converts from Salesperson object to SalespersonResponse object
        public static SalespersonResponseDto ToSalespersonResponse(this Salesperson salesperson)
        {
            return new SalespersonResponseDto()
            {
                SalespersonID = salesperson.SalespersonID,
                SalespersonFirstName = salesperson.SalespersonFirstName,
                SalespersonLastName = salesperson.SalespersonLastName,
                SalespersonAddress = salesperson.SalespersonAddress,
                SalespersonPhone = salesperson.SalespersonPhone,
                SalespersonStartDate = salesperson.SalespersonStartDate,
                SalespersonTerminationDate = salesperson.SalespersonTerminationDate
            };
        }
    }
}
