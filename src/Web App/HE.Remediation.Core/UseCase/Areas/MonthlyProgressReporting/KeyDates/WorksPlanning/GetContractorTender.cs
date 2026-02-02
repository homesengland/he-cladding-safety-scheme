using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.WorksPlanning;
public class GetContractorTenderHandler : IRequestHandler<GetContractorTenderRequest, GetContractorTenderResponse>
{
    private readonly IApplicationDataProvider _dataProvider;
    private readonly IApplicationDetailsProvider _detailsProvider;
    private readonly IProgressReportingKeyDatesRepository _keyDatesRepository;

    public GetContractorTenderHandler(
        IApplicationDataProvider dataProvider,
        IApplicationDetailsProvider detailsProvider,
        IProgressReportingKeyDatesRepository keyDatesRepository)
    {
        _dataProvider = dataProvider;
        _detailsProvider = detailsProvider;
        _keyDatesRepository = keyDatesRepository;
    }
    public async ValueTask<GetContractorTenderResponse> Handle(GetContractorTenderRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var details = await _detailsProvider.GetApplicationDetails();
        var reportId = _dataProvider.GetProgressReportId();
        var parameters = new GetMonthlyProgressReportParameters
        {
            ApplicationId = details.ApplicationId,
            ProgressReportId = reportId
        };
        var contractorTenderDetails = await _keyDatesRepository.GetContractorTenderDetails(parameters);

        return new GetContractorTenderResponse
        {
            ApplicationReferenceNumber = details?.ApplicationReferenceNumber,
            BuildingName = details?.BuildingName,
            ContractorTenderType = contractorTenderDetails?.ContractorTenderType
        };
    }
}

public class GetContractorTenderRequest : IRequest<GetContractorTenderResponse>
{
    private GetContractorTenderRequest()
    {
    }
    public static readonly GetContractorTenderRequest Request = new();
}

public class GetContractorTenderResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public int? ContractorTenderType { get; set; }
}