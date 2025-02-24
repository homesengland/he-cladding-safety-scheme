﻿using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.StartedOnSiteMilestone;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PracticalCompletionMilestone;

public class SubmitCheckYourAnswersHandler : IRequestHandler<SubmitCheckYourAnswersRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IMilestoneRepository _milestoneRepository;

    public SubmitCheckYourAnswersHandler(IApplicationDataProvider applicationDataProvider, IMilestoneRepository milestoneRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _milestoneRepository = milestoneRepository;
    }

    public async Task<Unit> Handle(SubmitCheckYourAnswersRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        await _milestoneRepository.SubmitPracticalCompletion(applicationId);

        return Unit.Value;
    }
}

public class SubmitCheckYourAnswersRequest : IRequest
{
    private SubmitCheckYourAnswersRequest()
    {
    }

    public static readonly SubmitCheckYourAnswersRequest Request = new();
}