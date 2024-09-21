using LemonAutomotives.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LemonAutomotives.Core.Domain.RepositoryContracts
{
    public interface IDiscountRepository
    {
        Task<List<Discount>> GetAllDiscountsAsync();

        Task<Discount> AddDiscountAsync(Discount discount);

        Task<Discount?> GetDiscountByIDAsync(Guid discountID);

        Task<List<Discount>> GetFilteredDiscounts(Expression<Func<Discount, bool>> predicate);
        Task<bool> DeleteDiscountByIDAsync(Guid discountID);

        Task<Discount> UpdateDiscountAsync(Discount Discount);
    }
}
