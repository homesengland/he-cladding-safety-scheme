using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAssessment;

public class SetFraDateHandler : IRequestHandler<SetFraDateRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IFireRiskAssessmentRepository _fireRiskAssessmentRepository;

    public SetFraDateHandler(IApplicationDataProvider applicationDataProvider, IFireRiskAssessmentRepository fireRiskAssessmentRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _fireRiskAssessmentRepository = fireRiskAssessmentRepository;
    }

    public async Task<Unit> Handle(SetFraDateRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicatioId = _applicationDataProvider.GetApplicationId();

        await _fireRiskAssessmentRepository.SetFraDate(new SetFraDateParameters
        {
            ApplicationId = applicatioId,
            FraDate = request.FraDate!.Value
        });

        return Unit.Value;
    }
}

public class SetFraDateRequest : IRequest
{
    public DateTime? FraDate { get; set; }
}