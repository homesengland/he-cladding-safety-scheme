
using HE.Remediation.Core.Data.Repositories;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.AddRole.SetAddRole;

public class SetAddRoleHandler : IRequestHandler<SetAddRoleRequest>
{
    private readonly IProgressReportingRepository _progressReportingRepository;

    public SetAddRoleHandler(IProgressReportingRepository progressReportingRepository)
    {
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<Unit> Handle(SetAddRoleRequest request, CancellationToken cancellationToken)
    {
        return Unit.Value;
    }    
}
