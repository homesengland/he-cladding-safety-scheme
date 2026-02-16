using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.CompanyDetails.SetCompanyDetails
{
    public class SetCompanyDetailsRequest : IRequest
    {
        public string CompanyName { get; set; }
        public string CompanyRegistrationNumber { get; set; }
    }
}