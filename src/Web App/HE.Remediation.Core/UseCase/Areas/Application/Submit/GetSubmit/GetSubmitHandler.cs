using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;

using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Application.Submit.GetSubmit
{
    public class GetSubmitHandler : IRequestHandler<GetSubmitRequest, GetSubmitResponse>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetSubmitHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async ValueTask<GetSubmitResponse> Handle(GetSubmitRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var applicationScheme = _applicationDataProvider.GetApplicationScheme();

            var result = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetSubmitResponse>("GetApplicationReferenceNumber",
                    new { applicationId });
            if (result == null)
                result = new GetSubmitResponse() { ApplicationScheme = applicationScheme };
            else
                result.ApplicationScheme = applicationScheme;

            return result;
        }
    }
}
