using LemonAutomotives.Core.Domain.Entities;

namespace LemonAutomotives.Core.DTO
{
    /// <summary>
    /// DTO class for adding a new Sale
    /// </summary>
    public class SalesAddRequestDto
    {
        public Guid SaleID { get; set; }
        public DateTime SalesDate { get; set; }
        public Guid ProductID { get; set; }
        public Guid SalespersonID { get; set; }
        public Guid CustomerID { get; set; }

        public Sales ToSales()
        {
            return new Sales()
            {
                SalesDate = SalesDate,
                ProductID = ProductID,
                SalespersonID = SalespersonID,
                CustomerID = CustomerID
            };
        }
    }
}
