using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.UseCase.Areas.VariationRequest.Costs.Set;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.ConfirmToProceed.SetConfirmToProceed
{
    public class SetWorkPackageConfirmToProceedHandler : IRequestHandler<SetWorkPackageConfirmToProceedRequest, Unit>
    {
        private readonly IWorkPackageRepository _workPackageRepository;

        public SetWorkPackageConfirmToProceedHandler(IWorkPackageRepository workPackageRepository)
        {
            _workPackageRepository = workPackageRepository;
        }

        public async Task<Unit> Handle(SetWorkPackageConfirmToProceedRequest request, CancellationToken cancellationToken)
        {
            bool? isConfirmedToProceed = request.IsConfirmedToProceed switch
            {
                ENoYes.Yes => true,
                ENoYes.No => false,
                _ => null
            };

            await _workPackageRepository.UpdateWorkPackageConfirmToProceed(isConfirmedToProceed);

            return Unit.Value;
        }
    }
}