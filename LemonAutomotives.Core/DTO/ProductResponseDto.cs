﻿using LemonAutomotives.Core.Domain.Entities;

namespace LemonAutomotives.Core.DTO
{
    /// <summary>
    /// Dto class that is used as return type for most ProductService methods
    /// </summary>
    public class ProductResponseDto
    {
        public string ProductID { get; set; } = string.Empty;
        public string? ProductName { get; set; }
        public string? ProductManufacturer { get; set; }
        public string? ProductModel { get; set; }
        public string? ProductYear { get; set; }
        public double? ProductPurchasePrice { get; set; }
        public double? ProductSalePrice { get; set; }
        public int ProductQty { get; set; }
        public double ProductCommission { get; set; }

        public override bool Equals(object? obj)
        {
            if(obj == null) { return false; }
            if(obj.GetType() != typeof(ProductResponseDto)) { return false; }

            ProductResponseDto productToCompare = (ProductResponseDto)obj;

            return ProductID == productToCompare.ProductID;
        }

        //Returns a unique key for the current object
        public override int GetHashCode()
        {
            return HashCode.Combine(ProductID, ProductName);
        }

        public ProductUpdateRequestDto ToProductUpdateRequest()
        {
            if (ProductName == null)
            {
                throw new InvalidOperationException("Product name cannot be null");
            }
            if (ProductManufacturer == null)
            {
                throw new InvalidOperationException("Product Manufacturer cannot be null");
            }
            if (ProductModel == null)
            {
                throw new InvalidOperationException("Product Model cannot be null");
            }
            if (ProductPurchasePrice == null)
            {
                throw new InvalidOperationException("Product PurchasePrice cannot be null");
            }
            if (ProductQty <= 0)
            {
                throw new InvalidOperationException("Product Quantity cannot be less than or equal to zero");
            }
            if (ProductCommission == 0 || ProductCommission >= 1)
            {
                throw new InvalidOperationException("Product Commission Must be less than 1 and not be 0");
            }

            return new ProductUpdateRequestDto()
            {
                ProductID = ProductID,
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

    public static class ProductExtension
    {
        public static ProductResponseDto ToProductResponse(this Products product)
        {
            return new ProductResponseDto()
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                ProductManufacturer = product.ProductManufacturer,
                ProductModel = product.ProductModel,
                ProductYear = product.ProductYear,
                ProductPurchasePrice = product.ProductPurchasePrice,
                ProductQty = product.ProductQty,
                ProductCommission = product.ProductCommission
            };
        }
    }
}
