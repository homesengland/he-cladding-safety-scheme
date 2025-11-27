using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.Repositories.FireRiskAppraisal;
using HE.Remediation.Core.Services.UserService;

namespace HE.Remediation.Core.UseCase.DataIngest.RAS.Lookups;

public interface IDataIngestionLookupService
{
    Task<DataIngestionLookupService.LookupData> GetLookups(ImportedDataRow importedDataRow);
}

public class DataIngestionLookupService : IDataIngestionLookupService
{
    private readonly IUserService _userService;
    private readonly IFireRiskAppraisalRepository _fireRiskAppraisalRepository;
    private readonly ILocalAuthorityCostCentreRepository _localAuthorityCostCentreRepository;
    private readonly IApplicationRepository _applicationRepository;

    public DataIngestionLookupService(
        IUserService userService,
        IFireRiskAppraisalRepository fireRiskAppraisalRepository,
        ILocalAuthorityCostCentreRepository localAuthorityCostCentreRepository,
        IApplicationRepository applicationRepository)
    {
        _userService = userService;
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

        var userDetail = await GetApplicantUserDetail(importedDataRow);
        var lookups = new LookupData
        {
            ApplicantUserId = userDetail.Item1,
            ApplicantCompanyRegistration = userDetail.Item2,
            LocalAuthorityCostCentreId = await GetLocalAuthorityId(importedDataRow),
            FraFireRiskAssessorId = await GetFireRiskAssessmentCompany(importedDataRow.FRACompanyName),
            FraewFireRiskAssessorId = await GetFireRiskAssessmentCompany(importedDataRow.FRAEWCompanyName)
        };

        return lookups;
    }

    private async Task<bool> IsExistingApplication(ImportedDataRow importedDataRow)
    {
        return await _applicationRepository.IsExistingApplication(importedDataRow.BuildingName, importedDataRow.AddressLine1, importedDataRow.PostCode);
    }

    private async Task<(Guid, string)> GetApplicantUserDetail(ImportedDataRow importedDataRow)
    {
        var user = await _userService.GetUserDetailsByCompanyName(importedDataRow.Developer);
        if (user == null)
        {
            throw new Exception("Developer - no applicant with matching company name");
        }
        if (string.IsNullOrEmpty(user.CompanyRegistrationNumber))
        {
            throw new Exception("Developer - matching applicant does not have a company registration number.");
        }
        return (user.UserId, user.CompanyRegistrationNumber);
    }

    private async Task<string> GetLocalAuthorityId(ImportedDataRow importedDataRow)
    {
        var costCentres = await _localAuthorityCostCentreRepository.GetCostCentres();
        var importedLocalAuthority = costCentres.FirstOrDefault(x => x.Name.Equals(importedDataRow.LocalAuthority, StringComparison.OrdinalIgnoreCase));
        if (importedLocalAuthority == null)
        {
            return null;
        }
        return importedLocalAuthority.Id;
    }

    private async Task<int?> GetFireRiskAssessmentCompany(string companyName)
    {
        if (string.IsNullOrEmpty(companyName))
        {
            return null;
        }
        var fireAssessmentCompanies = await _fireRiskAppraisalRepository.GetFireAssessorList();
        var matchedCompany = fireAssessmentCompanies.FirstOrDefault(f => f.CompanyName.Equals(companyName, StringComparison.OrdinalIgnoreCase));
        return matchedCompany?.Id;
    }

    public class LookupData
    {
        public Guid ApplicantUserId { get; set; }
        public string ApplicantCompanyRegistration { get; set; }
        public string LocalAuthorityCostCentreId { get; set; }
        public int? FraFireRiskAssessorId { get; set; }
        public int? FraewFireRiskAssessorId { get; set; }
    }
}