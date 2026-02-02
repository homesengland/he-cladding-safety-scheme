using HE.Remediation.Core.Data.Repositories;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Application.ManageProgramme
{
    public class GetApplicationHeadlinesHandler : IRequestHandler<GetApplicationHeadlinesRequest, GetApplicationHeadlinesResponse>
    {
        private readonly IManageProgrammeRepository _manageProgrammeRepository;

        public GetApplicationHeadlinesHandler(IManageProgrammeRepository manageProgrammeRepository)
        {
            _manageProgrammeRepository = manageProgrammeRepository;
        }

        public async ValueTask<GetApplicationHeadlinesResponse> Handle(GetApplicationHeadlinesRequest request, CancellationToken cancellationToken)
        {
            var items = new List<string>();
            var applicationHeadlines = await _manageProgrammeRepository.GetApplicationHeadlines(request.ApplicationIds);
            foreach (var a in applicationHeadlines)
            {
                items.Add($"{a.BuildingName} ({a.ReferenceNumber})");
            }
            var result = new GetApplicationHeadlinesResponse()
            {
                Items = items.ToArray()
            };
            return await Task.FromResult(result);
        }
    }

    public class GetApplicationHeadlinesRequest : IRequest<GetApplicationHeadlinesResponse>
    {
        public string[] ApplicationIds { get; set; }
    }

    public class GetApplicationHeadlinesResponse
    {
        public string[] Items { get; set; }
    }
}
