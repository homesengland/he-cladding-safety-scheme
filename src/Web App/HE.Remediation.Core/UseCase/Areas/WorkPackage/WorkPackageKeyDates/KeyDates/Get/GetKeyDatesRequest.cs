using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageKeyDates.KeyDates.Get;

public class GetKeyDatesRequest : IRequest<GetKeyDatesResponse>
{
	private GetKeyDatesRequest()
	{
	}

	public static readonly GetKeyDatesRequest Request = new();
}
