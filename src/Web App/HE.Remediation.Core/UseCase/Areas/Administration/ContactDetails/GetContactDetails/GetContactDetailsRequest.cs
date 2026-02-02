using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Administration.ContactDetails.GetContactDetails
{
    public class GetContactDetailsRequest : IRequest<GetContactDetailsResponse>
    {
        public string FirstName { get;set; }

        public string LastName { get;set; }

        public string ContactNumber { get;set; }

        private GetContactDetailsRequest()
        {

        }

        public static readonly GetContactDetailsRequest Request = new();
    }
}
