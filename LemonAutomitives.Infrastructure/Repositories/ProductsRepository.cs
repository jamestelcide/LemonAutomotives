using LemonAutomotives.Core.Domain.Entities;
using LemonAutomotives.Core.Domain.RepositoryContracts;
using LemonAutomotives.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LemonAutomotives.Infrastructure.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Products> AddProductAsync(Products product)
        {
            
            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            return product;
        }

        public async Task<List<Products>> GetAllProductsAsync()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task<Products?> GetProductsByIDAsync(string? productID)
        {
            return await _db.Products.FirstOrDefaultAsync(product => product.ProductID == productID);
        }

        public async Task<Products?> GetProductByNameAsync(string productName)
        {
            return await _db.Products.FirstOrDefaultAsync(p => p.ProductName == productName);
        }

        public async Task<List<Products>> GetFilteredProducts(Expression<Func<Products, bool>> predicate)
        {
            return await _db.Products
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<Products> UpdateProductsAsync(Products product)
        {
            Products? matchingProduct = await _db.Products.FirstOrDefaultAsync(p => p.ProductID == product.ProductID);

            if (matchingProduct == null) { return product; }

            matchingProduct.ProductName = product.ProductName;
            matchingProduct.ProductManufacturer = product.ProductManufacturer;
            matchingProduct.ProductModel = product.ProductModel;
            matchingProduct.ProductPurchasePrice = product.ProductPurchasePrice;
            matchingProduct.ProductQty = product.ProductQty;
            matchingProduct.ProductCommission = product.ProductCommission;

            await _db.SaveChangesAsync();
            return matchingProduct;
        }

        public async Task<bool> DeleteProductByIDAsync(string productID)
        {
            _db.Products.RemoveRange(_db.Products.Where(p => p.ProductID == productID));
            int rowsDeleted = await _db.SaveChangesAsync();

            return rowsDeleted > 0;
        }
    }
}
