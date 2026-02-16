using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.WorksRequirePermission.GetWorksRequirePermission;

public class GetWorksRequirePermissionRequest : IRequest<GetWorksRequirePermissionResponse>
{
	private GetWorksRequirePermissionRequest()
	{
	}

	public static readonly GetWorksRequirePermissionRequest Request = new();
}
