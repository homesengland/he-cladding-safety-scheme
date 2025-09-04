using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.CheckYourAnswers.SetCheckYourAnswers
{
    public class SetCheckYourAnswersHandler : IRequestHandler<SetCheckYourAnswersRequest, Unit>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IDbConnectionWrapper _db;

        public SetCheckYourAnswersHandler(IApplicationDataProvider applicationDataProvider, IDbConnectionWrapper db)
        {
            _applicationDataProvider = applicationDataProvider;
            _db = db;
        }

        public async Task<Unit> Handle(SetCheckYourAnswersRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var applicationId = _applicationDataProvider.GetApplicationId();

            await _db.ExecuteAsync("UpdateFundingRoutesCheckYourAnswers", new { applicationId });
            return Unit.Value;
        }
    }
}
