using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AssessorDetails.SetAssessorDetails
{
    public class SetAssessorDetailsRequest : IRequest<Unit>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string CompanyNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Telephone { get; set; }
    }
}
