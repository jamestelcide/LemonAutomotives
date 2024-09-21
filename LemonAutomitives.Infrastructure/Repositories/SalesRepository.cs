using LemonAutomotives.Core.Domain.Entities;
using LemonAutomotives.Core.Domain.RepositoryContracts;
using LemonAutomotives.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace LemonAutomotives.Infrastructure.Repositories
{
    public class SalesRepository : ISalesRepository
    {
        private readonly ApplicationDbContext _db;
        public SalesRepository(ApplicationDbContext db) { _db = db; }

        public async Task<Sales> CreateSale(Sales sale)
        {
            _db.Sales.Add(sale);
            await _db.SaveChangesAsync();

            return sale;
        }

        public async Task<List<Sales>> GetAllSalesAsync()
        {
            return await _db.Sales.ToListAsync();
        }

        public async Task<List<Sales>> GetFilteredSalesAsync(Expression<Func<Sales, bool>> predicate)
        {
            return await _db.Sales
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<List<Sales>> GetSalesByDateRange(DateTime startDate, DateTime endDate)
        {
            return await _db.Sales
                .Where(s => s.SalesDate >= startDate && s.SalesDate <= endDate)
                .ToListAsync();
        }

        public async Task<Sales?> GetSalesByIDAsync(Guid saleID)
        {
            return await _db.Sales.FirstOrDefaultAsync(sales => sales.SaleID == saleID);
        }
    }
}
