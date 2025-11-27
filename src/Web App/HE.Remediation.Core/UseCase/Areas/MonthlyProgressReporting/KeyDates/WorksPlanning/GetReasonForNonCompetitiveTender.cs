using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.KeyDates.WorksPlanning;
public class GetReasonForNonCompetitiveTenderHandler : IRequestHandler<GetReasonForNonCompetitiveTenderRequest, GetReasonForNonCompetitiveTenderResponse>
{
    private readonly IApplicationDataProvider _dataProvider;
    private readonly IApplicationDetailsProvider _detailsProvider;
    private readonly IProgressReportingKeyDatesRepository _keyDatesRepository;

    public GetReasonForNonCompetitiveTenderHandler(
        IApplicationDataProvider dataProvider,
        IApplicationDetailsProvider detailsProvider,
        IProgressReportingKeyDatesRepository keyDatesRepository)
    {
        _dataProvider = dataProvider;
        _detailsProvider = detailsProvider;
        _keyDatesRepository = keyDatesRepository;
    }

    public async Task<GetReasonForNonCompetitiveTenderResponse> Handle(GetReasonForNonCompetitiveTenderRequest request, CancellationToken cancellationToken)
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

        return new GetReasonForNonCompetitiveTenderResponse
        {
            ApplicationReferenceNumber = details?.ApplicationReferenceNumber,
            BuildingName = details?.BuildingName,
            ReasonForNonCompetitiveTender = contractorTenderDetails?.ReasonForNonCompetitiveTender
        };
    }
}

public class GetReasonForNonCompetitiveTenderRequest : IRequest<GetReasonForNonCompetitiveTenderResponse>
{
    private GetReasonForNonCompetitiveTenderRequest()
    {
    }
    public static readonly GetReasonForNonCompetitiveTenderRequest Request = new();
}

public class GetReasonForNonCompetitiveTenderResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public string ReasonForNonCompetitiveTender { get; set; }
}
