using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageFireRiskAssessment;

public class SetIdentifiedDefectsHandler : IRequestHandler<SetIdentifiedDefectsRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IWorkPackageFireRiskAssessmentRepository _fireRiskAssessmentRepository;

    public SetIdentifiedDefectsHandler(
        IApplicationDataProvider applicationDataProvider, 
        IWorkPackageFireRiskAssessmentRepository fireRiskAssessmentRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _fireRiskAssessmentRepository = fireRiskAssessmentRepository;
    }

    public async Task<Unit> Handle(SetIdentifiedDefectsRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        if (!request.InternalFireSafetyDefects.Contains(EInternalFireSafetyDefect.Other))
        {
            request.OtherInternalDefect = null;
        }

        await _fireRiskAssessmentRepository.SetWorkPackageFraInternalDefects(
            new SetWorkPackageFraInternalDefectsParameters
            {
                ApplicationId = applicationId,
                DefectIds = request.InternalFireSafetyDefects.Cast<int>(),
                OtherInternalFireSafetyRisk = request.OtherInternalDefect
            });

        return Unit.Value;
    }
}

public class SetIdentifiedDefectsRequest : IRequest
{
    public IList<EInternalFireSafetyDefect> InternalFireSafetyDefects { get; set; } = new List<EInternalFireSafetyDefect>();
    public string OtherInternalDefect { get; set; }
}