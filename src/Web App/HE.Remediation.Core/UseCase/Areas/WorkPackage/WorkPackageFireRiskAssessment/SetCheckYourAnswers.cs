using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageFireRiskAssessment;

public class SetCheckYourAnswersHandler : IRequestHandler<SetCheckYourAnswersRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IWorkPackageFireRiskAssessmentRepository _fireRiskAssessmentRepository;

    public SetCheckYourAnswersHandler(IApplicationDataProvider applicationDataProvider, IWorkPackageFireRiskAssessmentRepository fireRiskAssessmentRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _fireRiskAssessmentRepository = fireRiskAssessmentRepository;
    }

    public async Task<Unit> Handle(SetCheckYourAnswersRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _fireRiskAssessmentRepository.SubmitWorkPackageFra(applicationId);
        await _fireRiskAssessmentRepository.SetWorkPacakgeFraTaskStatus(new SetWorkPacakgeFraTaskStatusParameters
        {
            ApplicationId = applicationId,
            TaskStatusId = (int)ETaskStatus.Completed
        });

        scope.Complete();

        return Unit.Value;
    }
}

public class SetCheckYourAnswersRequest : IRequest
{
    private SetCheckYourAnswersRequest()
    {
    }

    public static readonly SetCheckYourAnswersRequest Request = new();
}