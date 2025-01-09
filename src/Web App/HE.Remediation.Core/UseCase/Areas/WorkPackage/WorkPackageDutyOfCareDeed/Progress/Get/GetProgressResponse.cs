
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageDutyOfCareDeed.Progress.Get;

public class GetProgressResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public  DateTime? DateSentToGrantCertifyingOfficer { get; set; }

	public  DateTime? DateSignedByGrantCertifyingOfficer { get; set; }

    public ETaskStatus Status { get; set; }
}
