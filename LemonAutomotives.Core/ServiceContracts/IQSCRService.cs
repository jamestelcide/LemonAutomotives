using LemonAutomotives.Core.DTO;

namespace LemonAutomotives.Core.ServiceContracts
{
    public interface IQSCRService
    {
        Task<List<QSCRDto>> GetQuarterlyCommissionReportAsync(DateTime startDate, DateTime endDate, Guid? salespersonId);
    }
}
