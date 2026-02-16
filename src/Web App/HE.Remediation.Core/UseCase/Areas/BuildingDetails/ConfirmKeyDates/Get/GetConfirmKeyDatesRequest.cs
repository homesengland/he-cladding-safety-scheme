using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.ConfirmKeyDates.Get;

public class GetConfirmKeyDatesRequest : IRequest<GetConfirmKeyDatesResponse>
{
	private GetConfirmKeyDatesRequest()
	{
	}

	public static readonly GetConfirmKeyDatesRequest Request = new();
}
