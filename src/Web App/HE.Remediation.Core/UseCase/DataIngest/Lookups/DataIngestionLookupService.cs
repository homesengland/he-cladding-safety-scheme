using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.Repositories.FireRiskAppraisal;
using HE.Remediation.Core.Services.UserService;

namespace HE.Remediation.Core.UseCase.DataIngest.Lookups
{
    public interface IDataIngestionLookupService
    {
        Task<DataIngestionLookupService.LookupData> GetLookups(ImportedDataRow importedDataRow);
    }

    public class DataIngestionLookupService : IDataIngestionLookupService
    {
        private readonly IUserService _userService;
        private readonly IAddressResolver _addressResolver;
        private readonly IFireRiskAppraisalRepository _fireRiskAppraisalRepository;
        private readonly ILocalAuthorityCostCentreRepository _localAuthorityCostCentreRepository;
        private readonly IApplicationRepository _applicationRepository;

        public DataIngestionLookupService(
            IUserService userService,
            IAddressResolver addressResolver,
            IFireRiskAppraisalRepository fireRiskAppraisalRepository,
            ILocalAuthorityCostCentreRepository localAuthorityCostCentreRepository,
            IApplicationRepository applicationRepository)
        {
            _userService = userService;
            _addressResolver = addressResolver;
            _fireRiskAppraisalRepository = fireRiskAppraisalRepository;
            _localAuthorityCostCentreRepository = localAuthorityCostCentreRepository;
            _applicationRepository = applicationRepository;
        }

        public async Task<LookupData> GetLookups(ImportedDataRow importedDataRow)
        {
            if (await IsExistingApplication(importedDataRow))
            {
                throw new Exception("Duplicate Application");
            }

            var lookups = new LookupData
            {
                ApplicantUserId = await GetApplicantUserId(importedDataRow),
                BuildingAddress = await GetBuildingAddress(importedDataRow),
                LocalAuthorityCostCentreId = await GetLocalAuthorityId(importedDataRow),
                FireRiskAssessorId = await GetFireRiskAssessmentCompany(importedDataRow)
            };

            return lookups;
        }

        private async Task<bool> IsExistingApplication(ImportedDataRow importedDataRow)
        {
            return await _applicationRepository.IsExistingApplication(importedDataRow.BuildingName, importedDataRow.PostCode);
        }

        private async Task<Guid> GetApplicantUserId(ImportedDataRow importedDataRow)
        {
            var user = await _userService.GetUserDetailsByCompanyRegistrationNumber(importedDataRow.RegistrationNumber);
            if (user == null)
            {
                throw new Exception("Provider_number - no applicant match");
            }
            return user.UserId;
        }

        private async Task<string> GetLocalAuthorityId(ImportedDataRow importedDataRow)
        {
            var costCentres = await _localAuthorityCostCentreRepository.GetCostCentres();
            var importedLocalAuthority = costCentres.FirstOrDefault(x => x.Name.Equals(importedDataRow.LocalAuthority, StringComparison.OrdinalIgnoreCase));
            if (importedLocalAuthority == null)
            {
                throw new Exception("Local Authority not found");
            }
            return importedLocalAuthority.Id;
        }

        private async Task<AddressResolver.AddressLookup> GetBuildingAddress(ImportedDataRow importedDataRow)
        {
            var details = await _addressResolver.GetAddress(importedDataRow);

            return new AddressResolver.AddressLookup()
            {
                AddressId = Guid.NewGuid(),
                NameNumber = details.NameNumber,
                AddressLine1 = details.AddressLine1,
                AddressLine2 = details.AddressLine2,
                City = details.City,
                County = details.County,
                Postcode = details.Postcode,
                LocalAuthority = details.LocalAuthority,
                SubBuildingName = details.SubBuildingName,
                BuildingName = details.BuildingName,
                BuildingNumber = details.BuildingNumber,
                Street = details.Street,
                Town = details.Town,
                AdminArea = details.AdminArea,
                UPRN = details.UPRN,
                AddressLines = details.AddressLines,
                XCoordinate = details.XCoordinate,
                YCoordinate = details.YCoordinate,
                Toid = details.Toid,
                BuildingType = details.BuildingType
            };
        }

        private async Task<int?> GetFireRiskAssessmentCompany(ImportedDataRow importedDataRow)
        {
            if (string.IsNullOrEmpty(importedDataRow.CompanyWhoDidTheSurvey))
            {
                return null;
            }
            var fireAssessmentCompanies = await _fireRiskAppraisalRepository.GetFireAssessorList();
            var matchedCompany = fireAssessmentCompanies.FirstOrDefault(f => f.CompanyName.Equals(importedDataRow.CompanyWhoDidTheSurvey, StringComparison.OrdinalIgnoreCase));
            return matchedCompany?.Id;
        }

        public class LookupData
        {
            public Guid ApplicantUserId { get; set; }
            public AddressResolver.AddressLookup BuildingAddress { get; set; }
            public string LocalAuthorityCostCentreId { get; set; }
            public int? FireRiskAssessorId { get; set; }
        }
    }
}