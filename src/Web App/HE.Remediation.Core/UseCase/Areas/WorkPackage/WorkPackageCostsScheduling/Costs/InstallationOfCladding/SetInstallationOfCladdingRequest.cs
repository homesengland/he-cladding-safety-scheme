using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.Costs.InstallationOfCladding;

public class SetInstallationOfCladdingRequest : IRequest
{
    public decimal? NewCladdingAmount { get; set; }
    public string NewCladdingDescription { get; set; }
    public decimal? ExternalWorksAmount { get; set; }
    public string ExternalWorksDescription { get; set; }
    public decimal? InternalWorksAmount { get; set; }
    public string InternalWorksDescription { get; set; }
}