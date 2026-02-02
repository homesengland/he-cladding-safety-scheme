using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageFireRiskAssessment;

public class SetFundingHandler : IRequestHandler<SetFundingRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IWorkPackageFireRiskAssessmentRepository _fireRiskAssessmentRepository;

    public SetFundingHandler(
        IApplicationDataProvider applicationDataProvider, 
        IWorkPackageFireRiskAssessmentRepository fireRiskAssessmentRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _fireRiskAssessmentRepository = fireRiskAssessmentRepository;
    }

    public async ValueTask<Unit> Handle(SetFundingRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var applicationId = _applicationDataProvider.GetApplicationId();

        await _fireRiskAssessmentRepository.SetWorkPackageFraFunding(new SetWorkPackageFraFundingParameters
        {
            ApplicationId = applicationId,
            HasFunding = request.HasFunding!.Value,
            FundingTypeId = (int)(request.HasFunding!.Value ? request.HasFundingType!.Value : request.HasNoFundingType!.Value)
        });

        return Unit.Value;
    }
}

public class SetFundingRequest : IRequest
{
    public bool? HasFunding { get; set; }
    public EFraFundingType? HasFundingType { get; set; }
    public EFraFundingType? HasNoFundingType { get; set; }
}