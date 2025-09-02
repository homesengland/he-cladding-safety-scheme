using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageFireRiskAssessment;

public class SetReportHandler : IRequestHandler<SetReportRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IWorkPackageFireRiskAssessmentRepository _fireRiskAssessmentRepository;

    public SetReportHandler(IApplicationDataProvider applicationDataProvider, IWorkPackageFireRiskAssessmentRepository fireRiskAssessmentRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _fireRiskAssessmentRepository = fireRiskAssessmentRepository;
    }

    public async Task<Unit> Handle(SetReportRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        await _fireRiskAssessmentRepository.SetWorkPackageFraAssessorAndDate(
            new SetWorkPackageFraAssessorAndDateParameters
            {
                ApplicationId = applicationId,
                FraDate = request.FraDate!.Value,
                AssessorId = request.AssessorId!.Value
            });

        return Unit.Value;
    }
}

public class SetReportRequest : IRequest
{
    public int? AssessorId { get; set; }
    public DateTime? FraDate { get; set; }
}