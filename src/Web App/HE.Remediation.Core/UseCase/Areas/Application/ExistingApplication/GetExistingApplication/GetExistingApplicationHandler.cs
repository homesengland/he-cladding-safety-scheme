using Dapper;
using HE.Remediation.Core.Extensions;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Application.ExistingApplication.GetExistingApplication
{
    public class GetExistingApplicationHandler : IRequestHandler<GetExistingApplicationRequest, IReadOnlyCollection<GetExistingApplicationResponse>>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IDbConnectionWrapper _db;

        public GetExistingApplicationHandler(IDbConnectionWrapper db, IApplicationDataProvider applicationDataProvider)
        {
            _db = db;
            _applicationDataProvider = applicationDataProvider; 
        }

        public async Task<IReadOnlyCollection<GetExistingApplicationResponse>> Handle(GetExistingApplicationRequest request, CancellationToken cancellationToken)
        {
            var stages = request.SelectedFilterStageOptions.Select(x => (int)x).ToArray();
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("UserId", _applicationDataProvider.GetUserId());
            dynamicParams.Add("Search", string.IsNullOrWhiteSpace(request.Search) ? null : request.Search);
            dynamicParams.Add("Stage", stages
            .ToDataTable()
            .AsTableValuedParameter("[dbo].[IntListType]"));

            var existingApplications = await _db.QueryAsync<GetExistingApplicationResponse>("GetExistingApplications", dynamicParams);
            
            return existingApplications;
        }
    }
}
