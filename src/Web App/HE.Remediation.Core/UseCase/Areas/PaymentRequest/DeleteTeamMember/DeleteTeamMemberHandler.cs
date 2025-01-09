using HE.Remediation.Core.Data.Repositories;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.DeleteTeamMember;

public class DeleteTeamMemberHandler : IRequestHandler<DeleteTeamMemberRequest>
{    
    private readonly IPaymentRequestRepository _paymentRequestRepository;

    public DeleteTeamMemberHandler(IPaymentRequestRepository paymentRequestRepository)
    {
        _paymentRequestRepository = paymentRequestRepository;   
    }

    public async Task<Unit> Handle(DeleteTeamMemberRequest request, CancellationToken cancellationToken)
    {
        await DeleteTeamMember(request);
        return Unit.Value;
    }

    private async Task DeleteTeamMember(DeleteTeamMemberRequest request)
    {
        await _paymentRequestRepository.UpdatePaymentRequestTeamMemberActiveState(request.TeamMemberId, false);
    }
}
