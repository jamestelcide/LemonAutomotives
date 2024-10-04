using LemonAutomotives.Core.Domain.Entities;
using LemonAutomotives.Core.Domain.RepositoryContracts;
using LemonAutomotives.Core.DTO;
using LemonAutomotives.Core.Helpers;
using LemonAutomotives.Core.ServiceContracts;

namespace LemonAutomotives.Core.Services
{
    public class SalespersonService : ISalespersonService
    {
        private readonly ISalespersonRepository _salespersonRepository;

        public SalespersonService(ISalespersonRepository salespersonRepository)
        {
            _salespersonRepository = salespersonRepository;
        }

        public async Task<SalespersonResponseDto> AddSalespersonAsync(SalespersonAddRequestDto? salespersonAddRequest)
        {
            //Validation: salespersonAddRequest parameter can't be null
            if (salespersonAddRequest == null)
            {
                throw new ArgumentNullException(nameof(salespersonAddRequest));
            }

            if(salespersonAddRequest.SalespersonFirstName == null)
            {
                throw new ArgumentNullException(nameof(salespersonAddRequest));
            }

            //Converts object from salespersonAddRequest to salesperson type
            Salesperson salesperson = salespersonAddRequest.ToSalesperson();

            //Generates a new salespersonID
            salesperson.SalespersonID = GenerateSalespersonID(salesperson.SalespersonFirstName, salesperson.SalespersonLastName);

            await _salespersonRepository.AddSalespersonAsync(salesperson);

            return salesperson.ToSalespersonResponse();
        }

        public async Task<List<SalespersonResponseDto>> GetAllSalespersonsAsync()
        {
            List<Salesperson> salespersons = await _salespersonRepository.GetAllSalespersonsAsync();
            return salespersons.Select(salesperson => salesperson.ToSalespersonResponse()).ToList();
        }

        public async Task<SalespersonResponseDto?> GetSalespersonByIDAsync(Guid? salespersonID)
        {
            if (salespersonID == null) { return null; }
            Salesperson? salespersonResponseFromList = await _salespersonRepository.GetSalespersonByIDAsync(salespersonID.Value);

            if (salespersonResponseFromList == null) { return null; }
            return salespersonResponseFromList.ToSalespersonResponse();
        }

        public async Task<List<SalespersonResponseDto>> GetFilteredSalespersonsAsync(string searchBy, string? searchString)
        {
            List<Salesperson> salespersons;

            salespersons = searchBy switch
            {
                nameof(SalespersonResponseDto.SalespersonFirstName) =>
                await _salespersonRepository.GetFilteredSalespersons(s =>
                s.SalespersonFirstName != null &&
                s.SalespersonFirstName.Contains(searchString ?? string.Empty)),

                nameof(SalespersonResponseDto.SalespersonLastName) =>
                await _salespersonRepository.GetFilteredSalespersons(s =>
                s.SalespersonLastName != null &&
                s.SalespersonLastName.Contains(searchString ?? string.Empty)),

                nameof(SalespersonResponseDto.SalespersonAddress) =>
                await _salespersonRepository.GetFilteredSalespersons(s =>
                s.SalespersonAddress != null &&
                s.SalespersonAddress.Contains(searchString ?? string.Empty)),

                nameof(SalespersonResponseDto.SalespersonPhone) =>
                await _salespersonRepository.GetFilteredSalespersons(s =>
                s.SalespersonPhone != null &&
                s.SalespersonPhone.Contains(searchString ?? string.Empty)),

                nameof(SalespersonResponseDto.SalespersonStartDate) =>
                await _salespersonRepository.GetFilteredSalespersons(s =>
                s.SalespersonStartDate.ToString("dd MM yyyy").Contains(searchString ?? string.Empty)),

                nameof(SalespersonResponseDto.SalespersonTerminationDate) =>
                await _salespersonRepository.GetFilteredSalespersons(s =>
                s.SalespersonTerminationDate.HasValue &&
                s.SalespersonTerminationDate.Value.ToString("dd MM yyyy").Contains(searchString ?? string.Empty)),

                _ => await _salespersonRepository.GetAllSalespersonsAsync()
            };

            return salespersons.Select(s => s.ToSalespersonResponse()).ToList();
        }

        public async Task<SalespersonResponseDto> UpdateSalespersonAsync(SalespersonUpdateRequestDto? salespersonUpdateRequest)
        {
            if (salespersonUpdateRequest == null)
            {
                throw new ArgumentNullException(nameof(salespersonUpdateRequest));
            }

            Salesperson? matchingSalesperson = await _salespersonRepository.GetSalespersonByIDAsync(salespersonUpdateRequest.SalespersonID);
            if (matchingSalesperson == null)
            {
                throw new ArgumentNullException(nameof(salespersonUpdateRequest.SalespersonID), "SalespersonID not found.");
            }

            matchingSalesperson.SalespersonFirstName = salespersonUpdateRequest.SalespersonFirstName;
            matchingSalesperson.SalespersonLastName = salespersonUpdateRequest.SalespersonLastName;
            matchingSalesperson.SalespersonAddress = salespersonUpdateRequest.SalespersonAddress;
            matchingSalesperson.SalespersonPhone = salespersonUpdateRequest.SalespersonPhone;
            matchingSalesperson.SalespersonStartDate = salespersonUpdateRequest.SalespersonStartDate;
            matchingSalesperson.SalespersonTerminationDate = salespersonUpdateRequest.SalespersonTerminationDate;

            await _salespersonRepository.UpdateSalespersonAsync(matchingSalesperson);

            return matchingSalesperson.ToSalespersonResponse();
        }

        public async Task<bool> DeleteSalespersonAsync(Guid? salespersonID)
        {
            if (salespersonID == null)
            {
                throw new ArgumentNullException(nameof(salespersonID));
            }

            Salesperson? salesperson = await _salespersonRepository.GetSalespersonByIDAsync(salespersonID.Value);

            if (salesperson == null) { return false; }

            await _salespersonRepository.DeleteSalespersonByIDAsync(salespersonID.Value);

            return true;
        }
    }
}
