
namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.AppointedOtherMembers.GetAppointedOtherMembers;

public class GetAppointedOtherMembersResponse
{
    public string ApplicationReferenceNumber { get; set; }

    public string BuildingName { get; set; }

    public bool? LeaseholdersInformed { get; set; }

    public bool? OtherMembersAppointed { get; set; }
}
