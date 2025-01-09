﻿
using HE.Remediation.Core.Data.Repositories;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.AppointedOtherMembers.SetAppointedOtherMembers;

public class SetAppointedOtherMembersHandler : IRequestHandler<SetAppointedOtherMembersRequest>
{
    private readonly IProgressReportingRepository _progressReportingRepository;

    public SetAppointedOtherMembersHandler(IProgressReportingRepository progressReportingRepository)
    {
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<Unit> Handle(SetAppointedOtherMembersRequest request, CancellationToken cancellationToken)
    {
        await UpdateOtherMembersAppointed(request);
        return Unit.Value;
    }

    private async Task UpdateOtherMembersAppointed(SetAppointedOtherMembersRequest request)
    {
        await _progressReportingRepository.UpdateOtherMembersAppointed(request.OtherMembersAppointed);
    }
}
