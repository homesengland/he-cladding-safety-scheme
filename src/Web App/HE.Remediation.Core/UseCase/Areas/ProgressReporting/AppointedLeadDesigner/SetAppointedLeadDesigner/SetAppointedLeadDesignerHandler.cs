
using HE.Remediation.Core.Data.Repositories;
using MediatR;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.AppointedLeadDesigner.SetAppointedLeadDesigner;

public class SetAppointedLeadDesignerHandler : IRequestHandler<SetAppointedLeadDesignerRequest>
{
    private readonly IProgressReportingRepository _progressReportingRepository;

    public SetAppointedLeadDesignerHandler(IProgressReportingRepository progressReportingRepository)
    {
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<Unit> Handle(SetAppointedLeadDesignerRequest request, CancellationToken cancellationToken)
    {
        await UpdateLeadDesignerAppointed(request);
        return Unit.Value;
    }

    private async Task UpdateLeadDesignerAppointed(SetAppointedLeadDesignerRequest request)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        if (request.LeadDesignerAppointed == false) 
        {            
            await _progressReportingRepository.DeleteLeadDesignerForCurrentProgressReport();
        }
        else
        {
            await _progressReportingRepository.DeleteLeadDesignerNotAppointedReason();
        }

        await _progressReportingRepository.UpdateLeadDesignerAppointed(request.LeadDesignerAppointed);

        scope.Complete();
    }
}
