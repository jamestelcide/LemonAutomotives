using LemonAutomotives.Core.Domain.Entities;
using LemonAutomotives.Core.Domain.RepositoryContracts;
using LemonAutomotives.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LemonAutomotives.Infrastructure.Repositories
{
    public class SalespersonRepository : ISalespersonRepository
    {
        private readonly ApplicationDbContext _db;

        public SalespersonRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Salesperson> AddSalespersonAsync(Salesperson salesperson)
        {
            _db.Salespersons.Add(salesperson);
            await _db.SaveChangesAsync();

            return salesperson;
        }

        public async Task<List<Salesperson>> GetAllSalespersonsAsync()
        {
            return await _db.Salespersons.ToListAsync();
        }

        public async Task<Salesperson?> GetSalespersonByIDAsync(Guid salespersonID)
        {
            return await _db.Salespersons.FirstOrDefaultAsync(salesperson => salesperson.SalespersonID == salespersonID);
        }

        public async Task<List<Salesperson>> GetFilteredSalespersons(Expression<Func<Salesperson, bool>> predicate)
        {
            return await _db.Salespersons
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<Salesperson> UpdateSalespersonAsync(Salesperson salesperson)
        {
            Salesperson? matchingSalesperson = await _db.Salespersons
                .FirstOrDefaultAsync(s => s.SalespersonID == salesperson.SalespersonID);

            if (matchingSalesperson == null) { return salesperson; }

            matchingSalesperson.SalespersonFirstName = salesperson.SalespersonFirstName;
            matchingSalesperson.SalespersonLastName = salesperson.SalespersonLastName;
            matchingSalesperson.SalespersonAddress = salesperson.SalespersonAddress;
            matchingSalesperson.SalespersonPhone = salesperson.SalespersonPhone;
            matchingSalesperson.SalespersonStartDate = salesperson.SalespersonStartDate;
            matchingSalesperson.SalespersonTerminationDate = salesperson.SalespersonTerminationDate;

            await _db.SaveChangesAsync();

            return matchingSalesperson;
        }

        public async Task<bool> DeleteSalespersonByIDAsync(Guid salespersonID)
        {
            _db.Salespersons.RemoveRange(_db.Salespersons.Where(s => s.SalespersonID == salespersonID));
            int rowsDeleted = await _db.SaveChangesAsync();

            return rowsDeleted > 0;
        }
    }
}
