using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageKeyDates.IsCladdingBeingRemoved.Get;

public class GetIsCladdingBeingRemovedRequest : IRequest<bool>
{
	private GetIsCladdingBeingRemovedRequest()
	{
	}

	public static readonly GetIsCladdingBeingRemovedRequest Request = new();
}
