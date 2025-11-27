using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.RefurbishmentCompletionDate.Get;

public class GetRefurbishmentCompletionDateRequest : IRequest<GetRefurbishmentCompletionDateResponse>
{
	private GetRefurbishmentCompletionDateRequest()
	{
	}

	public static readonly GetRefurbishmentCompletionDateRequest Request = new();
}
