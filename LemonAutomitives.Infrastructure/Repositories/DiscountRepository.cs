using LemonAutomotives.Core.Domain.Entities;
using LemonAutomotives.Core.Domain.RepositoryContracts;
using LemonAutomotives.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LemonAutomotives.Infrastructure.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly ApplicationDbContext _db;
        public DiscountRepository(ApplicationDbContext db) { _db = db; }

        public async Task<Discount> AddDiscountAsync(Discount discount)
        {
            _db.Discounts.Add(discount);
            await _db.SaveChangesAsync();

            return discount;
        }

        public async Task<bool> DeleteDiscountByIDAsync(Guid discountID)
        {
            var discount = await _db.Discounts.FindAsync(discountID);
            if (discount == null) return false;

            _db.Discounts.Remove(discount);
            int rowsDeleted = await _db.SaveChangesAsync();

            return rowsDeleted > 0;
        }

        public async Task<List<Discount>> GetAllDiscountsAsync()
        {
            return await _db.Discounts.ToListAsync();
        }

        public async Task<Discount?> GetDiscountByIDAsync(Guid discountID)
        {
            return await _db.Discounts.FirstOrDefaultAsync(discount => discount.DiscountID == discountID);
        }

        public async Task<List<Discount>> GetFilteredDiscounts(Expression<Func<Discount, bool>> predicate)
        {
            return await _db.Discounts.Where(predicate).ToListAsync();
        }

        public async Task<Discount> UpdateDiscountAsync(Discount discount)
        {
            var matchingDiscount = await _db.Discounts.FirstOrDefaultAsync(d => d.DiscountID == discount.DiscountID);

            if (matchingDiscount == null) return discount;

            matchingDiscount.BeginDate = discount.BeginDate;
            matchingDiscount.EndDate = discount.EndDate;
            matchingDiscount.DiscountPercentage = discount.DiscountPercentage;
            matchingDiscount.ProductID = discount.ProductID;

            await _db.SaveChangesAsync();
            return matchingDiscount;
        }
    }

}
