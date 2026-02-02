using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.NoTeam;
public class SetProjectTeamNoTeamRequest : IRequest
{
    public string ReasonNoTeam { get; set; }
    public ESubmitAction SubmitAction { get; set; }
}
