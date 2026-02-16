using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.GrantFundingSignatoryDetails.SetGrantFundingSignatoryDetails
{
    public class SetGrantFundingSignatoryDetailsRequest : IRequest
    {
        public Guid? Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string Role { get; set; }
    }
}
