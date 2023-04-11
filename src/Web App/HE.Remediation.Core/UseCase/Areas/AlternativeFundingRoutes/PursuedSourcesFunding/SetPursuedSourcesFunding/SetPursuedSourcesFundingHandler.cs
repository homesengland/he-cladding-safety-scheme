using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.PursuedSourcesFunding.SetPursuedSourcesFunding
{
    public class SetPursuedSourcesFundingHandler : IRequestHandler<SetPursuedSourcesFundingRequest, Unit>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IDbConnectionWrapper _db;

        public SetPursuedSourcesFundingHandler(IApplicationDataProvider applicationDataProvider, IDbConnectionWrapper db)
        {
            _applicationDataProvider = applicationDataProvider;
            _db = db;
        }

        public async Task<Unit> Handle(SetPursuedSourcesFundingRequest request, CancellationToken cancellationToken)
        {
            await SetPursuedSourcesFunding(request);
            return Unit.Value;
        }

        private async Task SetPursuedSourcesFunding(SetPursuedSourcesFundingRequest request)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            await _db.ExecuteAsync("UpsertPursuedSourcesFunding", new { applicationId, request.PursuedSourcesFunding });
        }
    }
}
