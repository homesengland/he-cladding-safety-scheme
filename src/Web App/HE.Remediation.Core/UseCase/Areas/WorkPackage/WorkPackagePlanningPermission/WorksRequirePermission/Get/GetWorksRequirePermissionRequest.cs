using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackagePlanningPermission.WorksRequirePermission.Get;

public class GetWorksRequirePermissionRequest : IRequest<GetWorksRequirePermissionResponse>
{
	private GetWorksRequirePermissionRequest()
	{
	}

	public static readonly GetWorksRequirePermissionRequest Request = new();
}
