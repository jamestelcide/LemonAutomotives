using LemonAutomotives.Core.Domain.RepositoryContracts;
using LemonAutomotives.Core.DTO;
using LemonAutomotives.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonAutomotives.Core.Services
{
    public class QSCRService : IQSCRService
    {
        private readonly ISalespersonRepository _salespersonRepository;
        private readonly ISalesRepository _salesRepository;
        private readonly IProductsRepository _productsRepository;

        public QSCRService(ISalespersonRepository salespersonRepository, 
            ISalesRepository salesRepository, IProductsRepository productsRepository)
        {
            _salespersonRepository = salespersonRepository;
            _salesRepository = salesRepository;
            _productsRepository = productsRepository;
        }

        public async Task<List<QSCRDto>> GetQuarterlyCommissionReportAsync(DateTime startDate, DateTime endDate, Guid? salespersonId)
        {
            var sales = await _salesRepository.GetAllSalesAsync();

            var filteredSales = sales
                .Where(s => s.SalesDate >= startDate && s.SalesDate <= endDate);

            if (salespersonId.HasValue)
            {
                filteredSales = filteredSales.Where(s => s.SalespersonID == salespersonId.Value);
            }

            var commissions = new List<QSCRDto>();

            foreach (var group in filteredSales.GroupBy(s => s.SalespersonID))
            {
                var salesperson = await _salespersonRepository.GetSalespersonByIDAsync(group.Key);

                // Ensure salesperson is found
                if (salesperson == null)
                {
                    continue; // Skip if not found
                }

                commissions.Add(new QSCRDto
                {
                    SalespersonID = group.Key,
                    SalespersonName = salesperson.SalespersonFirstName,
                    TotalCommission = group.Sum(s => s.Products?.ProductCommission ?? 0), // Handle null Products
                    SalesCount = group.Count()
                });
            }
            return commissions;
        }
    }
}
