using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Data.StoredProcedureResults.FireRiskAppraisal;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.Data.Repositories.FireRiskAppraisal;

public class FireRiskAppraisalRepository : IFireRiskAppraisalRepository
{
    private readonly IDbConnectionWrapper _db;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public FireRiskAppraisalRepository(IDbConnectionWrapper db, IApplicationDataProvider applicationDataProvider)
    {
        _db = db;
        _applicationDataProvider = applicationDataProvider;
    }


    public async Task<List<GetFireRiskAssessorListResult>> GetFireAssessorList()
    {
        var results = new Dictionary<int, GetFireRiskAssessorListResult>();

        var assessors = await _db.QueryAsync<GetFireRiskAssessorListResult, GetFireRiskAssessorListResult.RegionResult, GetFireRiskAssessorListResult>(
            "GetFireRiskAssessorList",
            (a, r) =>
            {
                if (!results.TryGetValue(a.Id, out var assessor))
                {
                    assessor = a;
                    results.Add(a.Id, a);
                }

                if (assessor.Regions.All(x => x.Id != r.Id))
                {
                    assessor.Regions.Add(r);
                }

                return assessor;
            });
        return results.Values.ToList();
    }

    public async Task<List<GetCladdingTypeResult>> GetCladdingSystemTypes()
    {
        var claddingTypes = await _db.QueryAsync<GetCladdingTypeResult>("GetCladdingSystemTypes");
        return claddingTypes.ToList();
    }

    public async Task<List<GetInsulationTypeResult>> GetInsulationTypes()
    {
        var insulationTypes = await _db.QueryAsync<GetInsulationTypeResult>("GetInsulationTypes");
        return insulationTypes.ToList();
    }

    public async Task<List<GetCladdingManufacturerResult>> GetActiveCladdingManufacturers()
    {
        var manufacturers = await _db.QueryAsync<GetCladdingManufacturerResult>("GetActiveCladdingManufacturers");
        return manufacturers.ToList();
    }

    public async Task UpdateStatusToInProgress()
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        await _db.ExecuteAsync("SetFraewSectionToInprogress", new { applicationId });
    }

    public async Task<IReadOnlyCollection<GetFireRiskAssessorPdfListResult>> GetFireRiskAssessorPdfList()
    {
        var resultDictionary = new Dictionary<int, GetFireRiskAssessorPdfListResult>();

        var assessors = await _db.QueryAsync<GetFireRiskAssessorPdfListResult, GetFireRiskAssessorPdfListResult.RegionResult, GetFireRiskAssessorPdfListResult>(
            nameof(GetFireRiskAssessorPdfList),
            (a, region) =>
            {
                if (!resultDictionary.TryGetValue(a.Id, out var assessor))
                {
                    assessor = a;
                    resultDictionary.Add(a.Id, a);
                }

                if (assessor.Regions.All(x => x.Id != region.Id))
                {
                    assessor.Regions.Add(region);
                }

                return assessor;
            });

        return resultDictionary.Values;
    }
}
