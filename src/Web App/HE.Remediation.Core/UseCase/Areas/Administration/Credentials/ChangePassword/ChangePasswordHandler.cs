using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Administration.Credentials.ChangePassword
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordRequest>
    {        
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IDbConnectionWrapper _db;

        public ChangePasswordHandler(IApplicationDataProvider applicationDataProvider, IDbConnectionWrapper db)
        {
            _applicationDataProvider = applicationDataProvider;
            _db = db;
        }

        public async Task<Unit> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
        {
            var auth0UserId = _applicationDataProvider.GetAuth0UserId();

            //var existingApplications = await _db.QueryAsync<GetExistingApplicationResponse>("GetExistingApplications");

            //if (existingApplications == null)
            //{
            //    return new List<ChangePasswordResponse>();
            //}

            return Unit.Value;
        }
    }
}
