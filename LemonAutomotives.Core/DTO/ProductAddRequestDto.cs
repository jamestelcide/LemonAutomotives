using LemonAutomotives.Core.Domain.Entities;
using LemonAutomotives.Core.Exceptions;

namespace LemonAutomotives.Core.DTO
{
    /// <summary>
    /// DTO class for adding a new Prouduct
    /// </summary>
    public class ProductAddRequestDto
    {
        public string ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? ProductManufacturer { get; set; }
        public string? ProductModel { get; set; }
        public string? ProductYear { get; set; }
        public double? ProductPurchasePrice { get; set; }
        public int ProductQty { get; set; }
        public double ProductCommission { get; set; }

        public Products ToProducts() 
        {
            return new Products()
            {
                ProductName = ProductName,
                ProductManufacturer = ProductManufacturer,
                ProductModel = ProductModel,
                ProductYear = ProductYear,
                ProductPurchasePrice = ProductPurchasePrice,
                ProductQty = ProductQty,
                ProductCommission = ProductCommission
            };
        }
    }
}
