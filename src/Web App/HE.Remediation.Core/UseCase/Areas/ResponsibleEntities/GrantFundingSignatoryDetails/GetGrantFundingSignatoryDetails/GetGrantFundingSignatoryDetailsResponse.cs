namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.GrantFundingSignatoryDetails.GetGrantFundingSignatoryDetails
{
    public class GetGrantFundingSignatoryDetailsResponse
    {
        public Guid? Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string Role { get; set; }
    }
}
