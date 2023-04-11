using HE.Remediation.Core.Data.StoredProcedureResults.FireRiskAppraisal;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.Data.Repositories.FireRiskAppraisal;

public class FireRiskAppraisalRepository : IFireRiskAppraisalRepository
{
    private readonly IDbConnectionWrapper _db;

    public FireRiskAppraisalRepository(IDbConnectionWrapper db)
    {
        _db = db;
    }


    public async Task<List<GetFireRiskAssessorListResult>> GetFireAssessorList()
    {
        var assessors = await _db.QueryAsync<GetFireRiskAssessorListResult>("GetFireRiskAssessorList");
        return assessors.ToList();
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

}
