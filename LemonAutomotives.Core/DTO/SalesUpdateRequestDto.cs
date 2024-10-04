using LemonAutomotives.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace LemonAutomotives.Core.DTO
{
    /// <summary>
    /// Represents the DTO class that contains the Sales details to update
    /// </summary>
    public class SalesUpdateRequestDto
    {
        public Guid SaleID { get; set; }

        [Required(ErrorMessage = "SalespersonID can not be blank")]
        public string SalespersonID { get; set; }

        [Required(ErrorMessage = "Product Name can not be blank")]
        public string ProductID { get; set; }

        [Required(ErrorMessage = "CustomerID can not be blank")]
        public Guid CustomerID { get; set; }
        [Required(ErrorMessage = "SalesDate can not be blank")]
        public DateTime SalesDate { get; set; }
        [Required(ErrorMessage = "PriceSold can not be blank")]
        public double PriceSold { get; set; }
        [Required(ErrorMessage = "Commission can not be blank")]
        public double Commission { get; set; }
        [Required(ErrorMessage = "CommissionEarnings can not be blank")]
        public double CommissionEarnings { get; set; }

        /// <summary>
        /// Converts the current object of SalesAddRequest into a new object of the Sales type
        /// </summary>
        /// <returns>Returns Sales object</returns>
        public Sales ToSales()
        {
            return new Sales()
            {
                SalespersonID = SalespersonID,
                ProductID = ProductID,
                CustomerID = CustomerID,
                SalesDate = SalesDate,
                PriceSold = PriceSold,
                Commission = Commission,
                CommissionEarnings = CommissionEarnings
            };
        }
    }
}
