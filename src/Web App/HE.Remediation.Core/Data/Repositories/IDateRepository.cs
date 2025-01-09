using HE.Remediation.Core.Data.StoredProcedureParameters;

namespace HE.Remediation.Core.Data.Repositories;

public interface IDateRepository
{
    Task<DateTime> AddWorkingDays(AddWorkingDaysParameters parameters);
}