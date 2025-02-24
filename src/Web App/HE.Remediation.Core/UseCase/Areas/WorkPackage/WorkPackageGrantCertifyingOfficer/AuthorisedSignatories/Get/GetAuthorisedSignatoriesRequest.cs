using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.AuthorisedSignatories.Get;

public class GetAuthorisedSignatoriesRequest : IRequest<GetAuthorisedSignatoriesResponse>
{
	private GetAuthorisedSignatoriesRequest()
	{
	}

	public static readonly GetAuthorisedSignatoriesRequest Request = new();
}
