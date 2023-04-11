using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Administration.SecondaryContactDetails.GetSecondaryContactDetails
{
    public class GetSecondaryContactDetailsRequest : IRequest<GetSecondaryContactDetailsResponse>
    {
        public string FirstName { get;set; }

        public string LastName { get;set; }

        public string ContactNumber { get;set; }

        private GetSecondaryContactDetailsRequest()
        {
        }

        public static readonly GetSecondaryContactDetailsRequest Request = new();
    }
}
