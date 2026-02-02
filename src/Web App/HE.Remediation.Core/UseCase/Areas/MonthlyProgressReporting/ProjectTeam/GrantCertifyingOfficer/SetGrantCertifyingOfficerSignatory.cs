using System.Transactions;
using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectTeam;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;

public class SetGrantCertifyingOfficerSignatoryHandler : IRequestHandler<SetGrantCertifyingOfficerSignatoryRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingProjectTeamRepository _progressReportingProjectTeamRepository;

    public SetGrantCertifyingOfficerSignatoryHandler(
        IApplicationDataProvider applicationDataProvider,
        IProgressReportingProjectTeamRepository progressReportingProjectTeamRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _progressReportingProjectTeamRepository = progressReportingProjectTeamRepository;
    }

    public async ValueTask<Unit> Handle(SetGrantCertifyingOfficerSignatoryRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var progressReportId = _applicationDataProvider.GetProgressReportId();
        
        await _progressReportingProjectTeamRepository.UpdateGrantCertifyingOfficerSignatory(
                    new UpdateGrantCertifyingOfficerSignatoryParameters
                    {
                        ProgressReportId = progressReportId,
                        Signatory = request.Signatory,
                        EmailAddress = request.EmailAddress,
                        DateAppointed = request.DateAppointed
                    });

        return Unit.Value;
    }
}

public class SetGrantCertifyingOfficerSignatoryRequest : IRequest
{
    public string Signatory { get; set; }
    public string EmailAddress { get; set; }
    public DateTime DateAppointed { get; set; }
}