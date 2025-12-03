using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.KeyDates.WorksPlanning;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.WorksPlanning;
public class SetContractorTenderHandler : IRequestHandler<SetContractorTenderRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingKeyDatesRepository _keyDatesRepository;

    public SetContractorTenderHandler(IApplicationDataProvider applicationDataProvider, IProgressReportingKeyDatesRepository keyDatesRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _keyDatesRepository = keyDatesRepository;
    }

    public async Task<Unit> Handle(SetContractorTenderRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();
        await _keyDatesRepository.SetContractorTenderType(
            new SetContractorTenderTypeParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId,
                ContractorTenderType = request.ContractorTenderType
            });
        return Unit.Value;
    }
}

public class SetContractorTenderRequest : IRequest<Unit>
{
    public int ContractorTenderType { get; set; }
}

