using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.CompanyDetails.GetCompanyDetails
{
    public class GetCompanyDetailsRequest : IRequest<GetCompanyDetailsResponse>
    {
        private GetCompanyDetailsRequest() { }

        public static readonly GetCompanyDetailsRequest Request = new();
    }
}
