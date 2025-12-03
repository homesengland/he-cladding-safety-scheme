using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.NameOfDevelopment.GetNameOfDevelopment
{
    public class GetNameOfDevelopmentRequest : IRequest<GetNameOfDevelopmentResponse>
    {
        private GetNameOfDevelopmentRequest() { }

        public static readonly GetNameOfDevelopmentRequest Request = new();
    }
}
