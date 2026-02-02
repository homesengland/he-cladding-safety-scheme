using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities
{
    public class SetResponsibleEntityCompleteHandler : IRequestHandler<SetResponsibleEntityCompleteRequest>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public SetResponsibleEntityCompleteHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async ValueTask<Unit> Handle(SetResponsibleEntityCompleteRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            await UpdateResponsibleEntitiesTaskStatus(applicationId, (int)ETaskStatus.Completed);

            return Unit.Value;
        }

        private async Task UpdateResponsibleEntitiesTaskStatus(Guid applicationId, int taskStatusId)
        {
            await _dbConnectionWrapper.ExecuteAsync("UpdateResponsibleEntityTaskStatus", new { applicationId, taskStatusId });
        }
    }

    public class SetResponsibleEntityCompleteRequest : IRequest
    {
        private SetResponsibleEntityCompleteRequest() { }

        public static readonly SetResponsibleEntityCompleteRequest Request = new();

    }
}
