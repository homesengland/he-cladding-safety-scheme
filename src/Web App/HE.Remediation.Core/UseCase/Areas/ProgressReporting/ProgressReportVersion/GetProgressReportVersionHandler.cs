﻿using HE.Remediation.Core.Data.Repositories;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ProgressReportVersion;

public class GetProgressReportVersionHandler : IRequestHandler<GetProgressReportVersionRequest, int>
{
    private readonly IProgressReportingRepository _progressReportingRepository;

    public GetProgressReportVersionHandler(IProgressReportingRepository progressReportingRepository)
    {
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<int> Handle(GetProgressReportVersionRequest request, CancellationToken cancellationToken)
    {
        var versionNumber = await _progressReportingRepository.GetProgressReportVersion();
        return versionNumber;
    }
}