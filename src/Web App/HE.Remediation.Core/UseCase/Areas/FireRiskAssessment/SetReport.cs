using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

using System.Transactions;

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

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _fireRiskAssessmentRepository.SetAssessorAndFraDate(new SetAssessorAndFraDateParameters
        {
            ApplicationId = applicationId,
            AssessorId = request.AssessorId!.Value,
            FraDate = request.FraDate!.Value
        });

        if (request.ApplicationScheme == EApplicationScheme.ResponsibleActorsScheme)
        {
            await _fireRiskAssessmentRepository.UpsertFraCommissionerType(new UpsertFraCommissionerTypeParameters
            {
                ApplicationId = applicationId,
                FraCommissionerTypeId = (int)request.FraCommissionerType!.Value,
            });
        }

        scope.Complete();

        return Unit.Value;
    }
}

public class SetReportRequest : IRequest
{
    public int? AssessorId { get; set; }
    public DateTime? FraDate { get; set; }
    public EFraCommissionerType? FraCommissionerType { get; set; }
    public EApplicationScheme ApplicationScheme { get; set; }
}