using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Application.StageDiagram.GetStageDiagram
{
    public class GetStageDiagramHandler : IRequestHandler<GetStageDiagramRequest, GetStageDiagramResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IDbConnectionWrapper _db;

        public GetStageDiagramHandler(IApplicationDataProvider applicationDataProvider, IDbConnectionWrapper db)
        {
            _applicationDataProvider = applicationDataProvider;
            _db = db;
        }

        public async Task<GetStageDiagramResponse> Handle(GetStageDiagramRequest request, CancellationToken cancellationToken)
        {
            var stageDiagramResponse = await _db.QuerySingleOrDefaultAsync<GetStageDiagramResponse>("GetStageDiagram", new
            {
                ApplicationId = _applicationDataProvider.GetApplicationId()
            });

            return stageDiagramResponse ?? new GetStageDiagramResponse();
        }
    }
}
