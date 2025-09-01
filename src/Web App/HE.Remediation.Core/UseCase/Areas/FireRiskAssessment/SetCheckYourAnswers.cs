using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAssessment;

public class SetCheckYourAnswersHandler : IRequestHandler<SetCheckYourAnswersRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IFireRiskAssessmentRepository _fireRiskAssessmentRepository;

    public SetCheckYourAnswersHandler(IApplicationDataProvider applicationDataProvider, IFireRiskAssessmentRepository fireRiskAssessmentRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _fireRiskAssessmentRepository = fireRiskAssessmentRepository;
    }

    public async Task<Unit> Handle(SetCheckYourAnswersRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _fireRiskAssessmentRepository.SubmitFra(applicationId);

        await _fireRiskAssessmentRepository.SetFraTaskStatus(new SetFraTaskStatusParameters
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
