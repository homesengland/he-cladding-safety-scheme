﻿using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageFireRiskAssessment;

public class SetFireRiskHandler : IRequestHandler<SetFireRiskRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IWorkPackageFireRiskAssessmentRepository _fireRiskAssessmentRepository;

    public SetFireRiskHandler(
        IApplicationDataProvider applicationDataProvider, 
        IWorkPackageFireRiskAssessmentRepository fireRiskAssessmentRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _fireRiskAssessmentRepository = fireRiskAssessmentRepository;
    }

    public async Task<Unit> Handle(SetFireRiskRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        await _fireRiskAssessmentRepository.SetWorkPackageFraFireRiskRating(
            new SetWorkPackageFraFireRiskRatingParameters
            {
                ApplicationId = applicationId,
                FireRiskRatingId = (int)request.FireRiskRating!.Value,
                HasInteralFireSafetyRisks = request.HasInternalFireSafetyRisks!.Value
            });

        return Unit.Value;
    }
}

public class SetFireRiskRequest : IRequest
{
    public EFraRiskRating? FireRiskRating { get; set; }
    public bool? HasInternalFireSafetyRisks { get; set; }
}