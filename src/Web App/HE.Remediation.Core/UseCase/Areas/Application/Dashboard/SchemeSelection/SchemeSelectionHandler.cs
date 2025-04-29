using HE.Remediation.Core.Data.Repositories;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Application.Dashboard.SchemeSelection
{
    public class SchemeSelectionHandler : IRequestHandler<SchemeSelectionRequest, SchemeSelectionResponse>
    {
        private readonly IApplicationSchemeRepository _applicationSchemeRepositroy;

        public SchemeSelectionHandler(IApplicationSchemeRepository applicationSchemeRepository)
        {
            _applicationSchemeRepositroy = applicationSchemeRepository;
        }

        public async Task<SchemeSelectionResponse> Handle(SchemeSelectionRequest request, CancellationToken cancellationToken)
        {
            var response = new SchemeSelectionResponse();
            var schemes = await _applicationSchemeRepositroy.GetApplicationSchemes();
            response.Schemes = schemes;

            return response;
        }
    }
}
