using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

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

    public async ValueTask<Unit> Handle(SetFireRiskRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        await _fireRiskAssessmentRepository.SetWorkPackageFraFireRiskRating(
            new SetWorkPackageFraFireRiskRatingParameters
            {
                ApplicationId = applicationId,
                FireRiskRatingId = (int)request.FireRiskRating!.Value,
                HasInternalFireSafetyRisks = request.HasInternalFireSafetyRisks!.Value
            });

        return Unit.Value;
    }
}

public class SetFireRiskRequest : IRequest
{
    public EFraRiskRating? FireRiskRating { get; set; }
    public bool? HasInternalFireSafetyRisks { get; set; }
}