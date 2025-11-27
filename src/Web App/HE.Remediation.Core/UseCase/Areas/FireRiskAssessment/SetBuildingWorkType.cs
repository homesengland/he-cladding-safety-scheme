using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAssessment;

public class SetBuildingWorkTypeHandler : IRequestHandler<SetBuildingWorkTypeRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IFireRiskAssessmentRepository _fireRiskAssessmentRepository;

    public SetBuildingWorkTypeHandler(IApplicationDataProvider applicationDataProvider, IFireRiskAssessmentRepository fireRiskAssessmentRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _fireRiskAssessmentRepository = fireRiskAssessmentRepository;
    }

    public async Task<Unit> Handle(SetBuildingWorkTypeRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        await _fireRiskAssessmentRepository.SetFraBuildingWorkType(new SetFraBuildingWorkTypeParameters
        {
            ApplicationId = applicationId,
            FraBuildingWorkTypeId = (int)(request.FraBuildingWorkTypeId.Value)
        });

        return Unit.Value;
    }
}

public class SetBuildingWorkTypeRequest : IRequest
{
    public EFraBuildingWorkType? FraBuildingWorkTypeId { get; set; }
}
