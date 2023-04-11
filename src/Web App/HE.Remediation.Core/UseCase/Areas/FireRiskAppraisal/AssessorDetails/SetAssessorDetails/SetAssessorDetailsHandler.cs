using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.AssessorDetails.SetAssessorDetails
{
    public class SetAssessorDetailsHandler : IRequestHandler<SetAssessorDetailsRequest, Unit>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IDbConnectionWrapper _db;

        public SetAssessorDetailsHandler(IApplicationDataProvider applicationDataProvider, IDbConnectionWrapper db)
        {
            _applicationDataProvider = applicationDataProvider;
            _db = db;
        }

        public async Task<Unit> Handle(SetAssessorDetailsRequest request, CancellationToken cancellationToken)
        {
            await SetAccessorDetails(request);
            return Unit.Value;
        }

        private async Task SetAccessorDetails(SetAssessorDetailsRequest request)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();
            await _db.ExecuteAsync("InsertOrUpdateFireRiskAssessmentAssessorDetails", new 
            { 
                applicationId, 
                request.FirstName,
                request.LastName,
                request.CompanyName,
                request.CompanyNumber,
                request.EmailAddress,
                request.Telephone
            });
        }
    }
}
