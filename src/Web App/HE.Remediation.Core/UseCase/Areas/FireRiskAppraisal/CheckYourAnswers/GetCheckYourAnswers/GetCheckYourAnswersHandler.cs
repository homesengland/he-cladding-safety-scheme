using HE.Remediation.Core.Data;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults.FireRiskAppraisal;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.WorksToCladdingSystems.GetWorksToCladdingSystems;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.CheckYourAnswers.GetCheckYourAnswers
{
    public class GetCheckYourAnswersHandler : IRequestHandler<GetCheckYourAnswersRequest, GetCheckYourAnswersResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IDbConnectionWrapper _db;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IFireRiskWorksRepository _fireRiskWorksRepository;        

        public GetCheckYourAnswersHandler(IApplicationDataProvider applicationDataProvider, 
                                          IDbConnectionWrapper db, 
                                          IApplicationRepository applicationRepository,
                                          IFireRiskWorksRepository fireRiskWorksRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _db = db;
            _applicationRepository = applicationRepository;
            _fireRiskWorksRepository = fireRiskWorksRepository;
        }

        public async Task<GetCheckYourAnswersResponse> Handle(GetCheckYourAnswersRequest request, CancellationToken cancellationToken)
        {
            return await GetCheckYourAnswers();
        }

        private async Task<GetCheckYourAnswersResponse> GetCheckYourAnswers()
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var applicationStatus = await _applicationRepository.GetApplicationStatus(applicationId);

            var answers = await _db.QuerySingleOrDefaultAsync<GetCheckYourAnswersResponse>("GetFireRiskAppraisalCheckYourAnswers", new
            {
                ApplicationId = _applicationDataProvider.GetApplicationId()
            });

            var internalWorks = await _fireRiskWorksRepository.GetFireRiskWallWorks(applicationId,
                                                                                  EWorkType.Internal);

            var externalWorks = await _fireRiskWorksRepository.GetFireRiskWallWorks(applicationId,
                                                                                  EWorkType.External);

            var claddingSystems = await _fireRiskWorksRepository.GetFireRiskCladdingWorks(applicationId);
            
            answers.CladdingSystems = claddingSystems;
            answers.InternalWorks = internalWorks;
            answers.ExternalWorks = externalWorks;

            answers.ReadOnly = applicationStatus.Submitted;

            return answers ?? new GetCheckYourAnswersResponse();
        }
    }
}
