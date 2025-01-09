namespace HE.Remediation.Core.Helpers;

public class CostsCalculationHelper
{
    public static MonthlyCostsCalculationResult CalculateMonthlyCosts(MonthlyCostsCalculationRequest request)
    {
        var totalMonthlyCosts = request.MonthlyCosts is not null 
            ? request.MonthlyCosts.Sum(x => x) 
            : 0;

        return new MonthlyCostsCalculationResult
        {
            TotalMonthlyCosts = totalMonthlyCosts,
            UnprofiledAmount = (request.ApprovedGrantFunding ?? 0) - (request.GrantPaidToDate ?? 0) - totalMonthlyCosts - request.CurrentCost - request.FinalCost - request.AdditionalCost,
            TotalCurrentCost = request.CurrentCost + request.AdditionalCost,
            FinalCost = request.FinalCost
        };
    }
}