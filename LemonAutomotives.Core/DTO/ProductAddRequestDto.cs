using LemonAutomotives.Core.Domain.Entities;

namespace LemonAutomotives.Core.DTO
{
    /// <summary>
    /// DTO class for adding a new Prouduct
    /// </summary>
    public class ProductAddRequestDto
    {
        public Guid ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? ProductManufacturer { get; set; }
        public string? ProductModel { get; set; }
        public double? ProductPurchasePrice { get; set; }
        public double? ProductSalePrice { get; set; }
        public int ProductQty { get; set; }
        public double ProductCommission { get; set; }

        public Products ToProducts() 
        {
            return new Products()
            {
                ProductName = ProductName,
                ProductManufacturer = ProductManufacturer,
                ProductModel = ProductModel,
                ProductPurchasePrice = ProductPurchasePrice,
                ProductSalePrice = ProductSalePrice,
                ProductQty = ProductQty,
                ProductCommission = ProductCommission
            };
        }
    }
}
