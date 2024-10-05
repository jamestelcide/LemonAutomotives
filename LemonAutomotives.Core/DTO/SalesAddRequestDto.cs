using LemonAutomotives.Core.Domain.Entities;

namespace LemonAutomotives.Core.DTO
{
    /// <summary>
    /// DTO class for adding a new Sale
    /// </summary>
    public class SalesAddRequestDto
    {
        public Guid SaleID { get; set; }
        public string SalespersonID { get; set; } = string.Empty;
        public string ProductID { get; set; } = string.Empty;
        public string CustomerID { get; set; } = string.Empty;
        public DateTime SalesDate { get; set; }
        public double PriceSold { get; set; }
        public double Commission { get; set; }
        public double CommissionEarnings { get; set; }

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
