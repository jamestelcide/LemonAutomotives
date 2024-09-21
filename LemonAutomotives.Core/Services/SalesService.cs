using LemonAutomotives.Core.Domain.Entities;
using LemonAutomotives.Core.Domain.RepositoryContracts;
using LemonAutomotives.Core.DTO;
using LemonAutomotives.Core.ServiceContracts;


namespace LemonAutomotives.Core.Services
{
    public class SalesService : ISalesService
    {
        private readonly ISalesRepository _salesRepository;

        public SalesService(ISalesRepository salesRepository)
        {
            _salesRepository = salesRepository;
        }

        public async Task<SalesResponseDto> CreateSaleAsync(SalesAddRequestDto? salesAddRequest)
        {
            if (salesAddRequest == null)
            {
                throw new ArgumentNullException(nameof(salesAddRequest));
            }

            if (salesAddRequest.ProductID == null)  {
                throw new ArgumentNullException(nameof(salesAddRequest));
            }

            Sales sale = salesAddRequest.ToSales();

            sale.SaleID = Guid.NewGuid();

            await _salesRepository.CreateSale(sale);

            return sale.ToSalesResponse();
        }

        public async Task<List<SalesResponseDto>> GetAllSalesAsync()
        {
            List<Sales> sales = await _salesRepository.GetAllSalesAsync();
            return sales.Select(sales => sales.ToSalesResponse()).ToList();
        }

        public async Task<List<SalesResponseDto>> GetFilteredSalesAsync(string searchBy, string? searchString)
        {
            List<Sales> sale;

            sale = searchBy switch
            {
                nameof(Sales.ProductID) =>
                await _salesRepository.GetFilteredSalesAsync(s =>
                s.ProductID != null &&
                s.ProductID.ToString().Contains(searchString ?? string.Empty)),

                nameof(Sales.SalespersonID) =>
                await _salesRepository.GetFilteredSalesAsync(s =>
                s.SalespersonID != null &&
                s.SalespersonID.ToString().Contains(searchString ?? string.Empty)),

                nameof(Sales.CustomerID) =>
                await _salesRepository.GetFilteredSalesAsync(s =>
                s.CustomerID != null &&
                s.CustomerID.ToString().Contains(searchString ?? string.Empty)),

                _ => await _salesRepository.GetAllSalesAsync()
            };
            return sale.Select(s => s.ToSalesResponse()).ToList();
        }

        public async Task<SalesResponseDto?> GetSaleByIDAsync(Guid? saleID)
        {
            if (saleID == null) { return null; }
            Sales? saleResponseFromList = await _salesRepository.GetSalesByIDAsync(saleID.Value);

            if (saleResponseFromList == null) { return null; }
            return saleResponseFromList.ToSalesResponse();
        }
    }
}
