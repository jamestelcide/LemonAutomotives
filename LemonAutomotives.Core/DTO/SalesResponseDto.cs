using LemonAutomotives.Core.Domain.Entities;

namespace LemonAutomotives.Core.DTO
{
    /// <summary>
    /// DTO class that is used as return type for most SalesService methods
    /// </summary>
    public class SalesResponseDto
    {
        public Guid SaleID { get; set; }
        public string SalespersonID { get; set; }
        public string ProductID { get; set; }
        public Guid CustomerID { get; set; }
        public DateTime SalesDate { get; set; }
        public double PriceSold { get; set; }
        public double Commission { get; set; }
        public double CommissionEarnings { get; set; }

        //Compares current object to another object of SalesResponse type and returns true, if both values are the same; otherwise it will return false
        public override bool Equals(object? obj)
        {
            if (obj == null) { return false; }

            if (obj.GetType() != typeof(SalesResponseDto)) { return false; }

            SalesResponseDto saleToCompare = (SalesResponseDto)obj;

            return SaleID == saleToCompare.SaleID;
        }

        //Returns a unique key for the current object
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public SalesUpdateRequestDto ToSalesUpdateRequest()
        {
            if (SalesDate == null)
            {
                throw new InvalidOperationException("Sales date cannot be null");
            }
            if (ProductID == null)
            {
                throw new InvalidOperationException("Product ID cannot be null");
            }
            if (SalespersonID == null)
            {
                throw new InvalidOperationException("Salesperson ID cannot be null");
            }
            if (CustomerID == null)
            {
                throw new InvalidOperationException("Customer ID cannot be null");
            }

            return new SalesUpdateRequestDto()
            {
                SaleID = SaleID,
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

    public static class SalesExtension
    {
        //Converts from Sales object to SalesResponse object
        public static SalesResponseDto ToSalesResponse(this Sales sale)
        {
            return new SalesResponseDto()
            {
                SaleID = sale.SaleID,
                SalespersonID = sale.SalespersonID,
                ProductID = sale.ProductID,
                CustomerID = sale.CustomerID,
                SalesDate = sale.SalesDate,
                PriceSold = sale.PriceSold,
                Commission = sale.Commission,
                CommissionEarnings = sale.CommissionEarnings
            };
        }
    }
}
