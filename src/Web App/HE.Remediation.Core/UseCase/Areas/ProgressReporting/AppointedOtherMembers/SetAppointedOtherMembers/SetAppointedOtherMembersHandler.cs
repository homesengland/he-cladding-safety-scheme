using HE.Remediation.Core.Data.Repositories;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.AppointedOtherMembers.SetAppointedOtherMembers;

public class SetAppointedOtherMembersHandler : IRequestHandler<SetAppointedOtherMembersRequest>
{
    private readonly IProgressReportingRepository _progressReportingRepository;

    public SetAppointedOtherMembersHandler(IProgressReportingRepository progressReportingRepository)
    {
        _progressReportingRepository = progressReportingRepository;
    }

    public async ValueTask<Unit> Handle(SetAppointedOtherMembersRequest request, CancellationToken cancellationToken)
    {
        await _progressReportingRepository.UpdateOtherMembersAppointed(request.OtherMembersAppointed);
        return Unit.Value;
    }
}
