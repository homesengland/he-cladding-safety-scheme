using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageProgrammePlan;

public class SetProjectPlanCheckYourAnswersHandler : IRequestHandler<SetProjectPlanCheckYourAnswersRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IWorkPackageRepository _workPackageRepository;

    public SetProjectPlanCheckYourAnswersHandler(
        IApplicationDataProvider applicationDataProvider, 
        IWorkPackageRepository workPackageRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _workPackageRepository = workPackageRepository;
    }

    public async Task<Unit> Handle(SetProjectPlanCheckYourAnswersRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        await _workPackageRepository.SetWorkPackageProgrammePlanTaskStatus(new SetWorkPackageProgrammePlanTaskStatusParameters
        {
            ApplicationId = applicationId,
            TaskStatusId = (int)ETaskStatus.Completed
        });

        return Unit.Value;
    }
}

public class SetProjectPlanCheckYourAnswersRequest : IRequest
{
    private SetProjectPlanCheckYourAnswersRequest()
    {
    }

    public static readonly SetProjectPlanCheckYourAnswersRequest Request = new();
}