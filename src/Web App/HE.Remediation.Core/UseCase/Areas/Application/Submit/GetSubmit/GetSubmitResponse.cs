using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.Application.Submit.GetSubmit
{
    public class GetSubmitResponse
    {
        public string ReferenceNumber { get; set; }
        public EApplicationScheme ApplicationScheme { get; set; }
    }
}
