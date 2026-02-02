using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAssessment;

public class SetIdentifiedDefectsHandler : IRequestHandler<SetIdentifiedDefectsRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IFireRiskAssessmentRepository _fireRiskAssessmentRepository;

    public SetIdentifiedDefectsHandler(IApplicationDataProvider applicationDataProvider, IFireRiskAssessmentRepository fireRiskAssessmentRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _fireRiskAssessmentRepository = fireRiskAssessmentRepository;
    }

    public async ValueTask<Unit> Handle(SetIdentifiedDefectsRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        if (!request.InternalFireSafetyDefects.Contains(EInternalFireSafetyDefect.Other))
        {
            request.OtherInternalDefect = null;
        }

        await _fireRiskAssessmentRepository.SetInternalFireSafetyDefects(new SetInternalFireSafetyDefectsParameters
        {
            ApplicationId = applicationId,
            InternalFireSafetyDefectIds = request.InternalFireSafetyDefects,
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