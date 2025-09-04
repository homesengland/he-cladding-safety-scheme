using HE.Remediation.Core.Data.Repositories;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Application.ManageProgramme
{
    public class SetApplicationUpdatesHandler : IRequestHandler<SetApplicationUpdatesRequest>
    {
        private readonly IManageProgrammeRepository _manageProgrammeRepository;

        public SetApplicationUpdatesHandler(IManageProgrammeRepository manageProgrammeRepository)
        {
            _manageProgrammeRepository = manageProgrammeRepository;
        }

        public async Task<Unit> Handle(SetApplicationUpdatesRequest request, CancellationToken cancellationToken)
        {
            await _manageProgrammeRepository.SaveManageProgrammeUpdates(request);
            return Unit.Value;
        }
    }

    public class SetApplicationUpdatesRequest : IRequest
    {
        public DateTime? EstimatedInvestigationCompletionDate { get; set; }
        public DateTime? EstimatedStartOnSiteDate { get; set; }
        public DateTime? EstimatedPracticalCompletionDate { get; set; }

        public string[] ApplicationIds { get; set; }
    }

}
