using HE.Remediation.Core.Data.StoredProcedureResults.Costs;
using Mediator;

namespace HE.Remediation.Core.UseCase.Shared.Costs.Set;

public class SetCostsRequest : IRequest
{
    public IList<MonthlyCostResult> MonthlyCosts { get; set; }
}
