using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAssessment;

public class SetReportHandler : IRequestHandler<SetReportRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IFireRiskAssessmentRepository _fireRiskAssessmentRepository;

    public SetReportHandler(IApplicationDataProvider applicationDataProvider, IFireRiskAssessmentRepository fireRiskAssessmentRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _fireRiskAssessmentRepository = fireRiskAssessmentRepository;
    }

    public async Task<Unit> Handle(SetReportRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        await _fireRiskAssessmentRepository.SetAssessorAndFraDate(new SetAssessorAndFraDateParameters
        {
            ApplicationId = applicationId,
            AssessorId = request.AssessorId!.Value,
            FraDate = request.FraDate!.Value
        });

        return Unit.Value;
    }
}

public class SetReportRequest : IRequest
{
    public int? AssessorId { get; set; }
    public DateTime? FraDate { get; set; }
}