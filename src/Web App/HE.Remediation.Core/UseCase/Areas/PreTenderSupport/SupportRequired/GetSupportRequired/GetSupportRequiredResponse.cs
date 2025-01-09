namespace HE.Remediation.Core.UseCase.Areas.PreTenderSupport.SupportRequired.GetSupportRequired
{
    public class GetSupportRequiredResponse
    {
        public Guid ApplicationId { get; set; }
        public bool? SupportRequired { get; set; }
        public bool IsSubmitted { get; set; }
    }
}