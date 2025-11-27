using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.ConstructionCompletionDate.Get
{
    public class GetConstructionCompletionDateRequest : IRequest<GetConstructionCompletionDateResponse>
    {
        private GetConstructionCompletionDateRequest()
        {
        }

        public static readonly GetConstructionCompletionDateRequest Request = new();
    }
}
