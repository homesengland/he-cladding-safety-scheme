using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectTeam;
using HE.Remediation.Core.Interface;
using Mediator;
namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;

public class SetGrantCertifyingOfficerAddressHandler : IRequestHandler<SetGrantCertifyingOfficerAddressRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingProjectTeamRepository _progressReportingProjectTeamRepository;

    public SetGrantCertifyingOfficerAddressHandler(
        IApplicationDataProvider applicationDataProvider,
        IProgressReportingProjectTeamRepository progressReportingProjectTeamRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _progressReportingProjectTeamRepository = progressReportingProjectTeamRepository;
    }

    public async ValueTask<Unit> Handle(SetGrantCertifyingOfficerAddressRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var progressReportId = _applicationDataProvider.GetProgressReportId();
        await _progressReportingProjectTeamRepository.UpdateGrantCertifyingOfficerAddress(
            new UpdateGrantCertifyingOfficerAddressParameters
            {
                ProgressReportId = progressReportId,
                NameNumber = request.NameNumber ?? "",
                AddressLine1 = request.AddressLine1 ?? "",
                AddressLine2 = request.AddressLine2 ?? "",
                City = request.City ?? "",
                County = request.County ?? "",
                Postcode = request.Postcode ?? ""
            });

        return Unit.Value;
    }
}

public class SetGrantCertifyingOfficerAddressRequest : IRequest
{
    public string NameNumber { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string County { get; set; }
    public string Postcode { get; set; }
}