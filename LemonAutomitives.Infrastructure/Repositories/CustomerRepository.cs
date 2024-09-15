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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _db;

        public CustomerRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _db.Customers.ToListAsync();
        }

        public async Task<List<Customer>> GetFilteredCustomers(Expression<Func<Customer, bool>> predicate)
        {
            return await _db.Customers
                .Where(predicate)
                .ToListAsync();
        }
    }
}
