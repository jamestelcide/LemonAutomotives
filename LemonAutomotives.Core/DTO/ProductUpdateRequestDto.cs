using LemonAutomotives.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace LemonAutomotives.Core.DTO
{
    /// <summary>
    /// Represents the DTO class that contains the Product details to update
    /// </summary>
    public class ProductUpdateRequestDto
    {
        public Guid ProductID { get; set; }
        [Required(ErrorMessage = "Product Name can not be blank")]
        public string? ProductName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Product Manufacturer can not be blank")]
        public string? ProductManufacturer { get; set; } = string.Empty;
        [Required(ErrorMessage = "Product Model can not be blank")]
        public string? ProductModel { get; set; } = string.Empty;
        [Required(ErrorMessage = "Product Year can not be blank")]
        public string? ProductYear { get; set; }
        [Required(ErrorMessage = "Product Purchase Price can not be blank")]
        public double? ProductPurchasePrice { get; set; }
        [Required(ErrorMessage = "Product Quantity can not be blank")]
        public int ProductQty { get; set; }
        [Required(ErrorMessage = "Product Commission can not be blank")]
        public double ProductCommission { get; set; }

        /// <summary>
        /// Converts the current object of ProductAddRequest into a new object of the Product type
        /// </summary>
        /// <returns>Returns Product object</returns>
        public Products ToProduct()
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