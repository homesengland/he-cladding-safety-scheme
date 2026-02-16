using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.VariationRequest;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.VariationReason.Set;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.VariationRequest.Timescale.Set
{
    public class SetAdjustEndDateHandler : IRequestHandler<SetAdjustEndDateRequest>
    {
        private readonly IVariationRequestRepository _variationRequestRepository;

        public SetAdjustEndDateHandler(IVariationRequestRepository variationRequestRepository)
        {
            _variationRequestRepository = variationRequestRepository;
        }

        public async ValueTask<Unit> Handle(SetAdjustEndDateRequest request, CancellationToken cancellationToken)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            await _variationRequestRepository.UpdateVariationAdjustEndDate(new UpdateVariationAdjustEndDateParameters
            {
                NewEndMonth = request.NewEndMonth,
                NewEndYear = request.NewEndYear
            });

            scope.Complete();

            return Unit.Value;
        }
    }
}