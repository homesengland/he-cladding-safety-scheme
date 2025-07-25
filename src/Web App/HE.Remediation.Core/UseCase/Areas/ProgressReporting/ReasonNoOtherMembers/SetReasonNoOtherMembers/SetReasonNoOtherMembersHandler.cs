﻿using HE.Remediation.Core.Data.Repositories;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonNoOtherMembers.SetReasonNoOtherMembers;

public class SetReasonNoOtherMembersHandler : IRequestHandler<SetReasonNoOtherMembersRequest>
{
    private readonly IProgressReportingRepository _progressReportingRepository;

    public SetReasonNoOtherMembersHandler(IProgressReportingRepository progressReportingRepository)
    {
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<Unit> Handle(SetReasonNoOtherMembersRequest request, CancellationToken cancellationToken)
    {
        await _progressReportingRepository.UpdateOtherMembersNotAppointedReason(request.OtherMembersNotAppointedReason, request.OtherMembersNeedsSupport);
        return Unit.Value;
    }
}
