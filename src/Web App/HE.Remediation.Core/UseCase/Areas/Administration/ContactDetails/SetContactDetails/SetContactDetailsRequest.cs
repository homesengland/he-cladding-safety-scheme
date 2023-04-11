using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Administration.ContactDetails.SetContactDetails
{
    public class SetContactDetailsRequest : IRequest<Unit>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ContactNumber { get; set; }
    }
}
