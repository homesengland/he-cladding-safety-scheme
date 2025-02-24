
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.WhenSubmit.SetWhenSubmit;

public class SetWhenSubmitRequest : IRequest<SetWhenSubmitResponse>
{
    public int? SubmissionMonth { get; set; }

    public int? SubmissionYear { get; set; }
}
