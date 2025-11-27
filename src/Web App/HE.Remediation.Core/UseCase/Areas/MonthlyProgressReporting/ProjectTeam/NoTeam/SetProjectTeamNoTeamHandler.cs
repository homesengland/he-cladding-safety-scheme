using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectTeam;
using HE.Remediation.Core.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.NoTeam;
public class SetProjectTeamNoTeamHandler : IRequestHandler<SetProjectTeamNoTeamRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingProjectTeamRepository _progressReportingProjectTeamRepository;

    public SetProjectTeamNoTeamHandler(IApplicationDataProvider applicationDataProvider, 
        IProgressReportingProjectTeamRepository progressReportingProjectTeamRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _progressReportingProjectTeamRepository = progressReportingProjectTeamRepository;
    }

    public async Task<Unit> Handle(SetProjectTeamNoTeamRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var progressReportId = _applicationDataProvider.GetProgressReportId();
        var parameters = new SetProjectTeamNoTeamParameters
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            ReasonNoTeam = request.ReasonNoTeam,
            SubmitAction = (int)request.SubmitAction
        };
        await _progressReportingProjectTeamRepository.SetProjectTeamNoTeam(parameters);
        return Unit.Value;
    }
}
