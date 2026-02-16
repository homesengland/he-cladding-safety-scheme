
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.WhenSubmit.SetWhenSubmit;

public class SetWhenSubmitRequest : IRequest
{
    public int? SubmissionMonth { get; set; }

    public int? SubmissionYear { get; set; }
}
