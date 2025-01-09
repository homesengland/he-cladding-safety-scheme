using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.InformedLeaseholder.SetInformedLeaseholder;

public class SetInformedLeaseholderRequest :  IRequest
{
    public bool? LeaseholdersInformed { get; set; }     
}
