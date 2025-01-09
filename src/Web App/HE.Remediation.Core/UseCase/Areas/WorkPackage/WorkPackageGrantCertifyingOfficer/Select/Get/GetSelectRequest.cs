using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.Select.Get;

public class GetSelectRequest : IRequest<GetSelectResponse>
{
	private GetSelectRequest()
	{
	}

	public static readonly GetSelectRequest Request = new();
}
