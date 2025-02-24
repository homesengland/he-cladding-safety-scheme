namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.GrantFundingSignatories.GetGrantFundingSignatories
{
    public class GetGrantFundingSignatoryResponse
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string EmailAddress { get; set; }

        public string Role { get; set; }
    }
}
