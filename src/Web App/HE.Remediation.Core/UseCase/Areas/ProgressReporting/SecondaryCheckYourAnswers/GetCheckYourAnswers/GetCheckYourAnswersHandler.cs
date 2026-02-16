using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.SecondaryCheckYourAnswers.GetCheckYourAnswers
{
    public class GetCheckYourAnswersHandler : IRequestHandler<GetCheckYourAnswersRequest, GetCheckYourAnswersResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IBuildingDetailsRepository _buildingDetailsRepository;
        private readonly IProgressReportingRepository _progressReportingRepository;

        public GetCheckYourAnswersHandler(IApplicationDataProvider applicationDataProvider,
                                               IBuildingDetailsRepository buildingDetailsRepository,
                                               IApplicationRepository applicationRepository,
                                               IProgressReportingRepository progressReportingRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _buildingDetailsRepository = buildingDetailsRepository;
            _applicationRepository = applicationRepository;
            _progressReportingRepository = progressReportingRepository;
        }

        public async ValueTask<GetCheckYourAnswersResponse> Handle(GetCheckYourAnswersRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);

            var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

            var checkMyAnswersResult = await _progressReportingRepository.GetProgressReportSecondaryCheckMyAnswers();

            await _progressReportingRepository.SetHasVisitedCheckYourAnswers(new SetHasVisitedCheckYourAnswersParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = _applicationDataProvider.GetProgressReportId()
            });

            var response = new GetCheckYourAnswersResponse
            {
                ApplicationReferenceNumber = applicationReferenceNumber,
                BuildingName = buildingName,
                LastUpdate = checkMyAnswersResult.LastUpdate,
                ThisMonthProgressSummary = checkMyAnswersResult.ThisMonthProgressSummary,
                NextMonthProgressSummary = checkMyAnswersResult.NextMonthProgressSummary,
                HelpNeeded = checkMyAnswersResult.HelpNeeded,
                SupportNeededReason = checkMyAnswersResult.SupportNeededReason
            };

            if (checkMyAnswersResult?.LeadDesignerNeedsSupport == true)
            {
                response.SupportTypes.Add(EProgressReportSupportType.AppointingDesigner);
            }

            if (checkMyAnswersResult?.OtherMembersNeedsSupport == true)
            {
                response.SupportTypes.Add(EProgressReportSupportType.AppointingTeam);
            }

            if (checkMyAnswersResult?.QuotesNeedsSupport == true)
            {
                response.SupportTypes.Add(EProgressReportSupportType.SeekingQuotes);
            }

            if (checkMyAnswersResult?.PlanningPermissionNeedsSupport == true)
            {
                response.SupportTypes.Add(EProgressReportSupportType.PlanningPermission);
            }

            if (checkMyAnswersResult?.OtherNeedsSupport == true)
            {
                response.SupportTypes.Add(EProgressReportSupportType.Other);
            }

            return response;
        }
    }

    public class GetCheckYourAnswersRequest : IRequest<GetCheckYourAnswersResponse>
    {
        private GetCheckYourAnswersRequest() { }

        public static readonly GetCheckYourAnswersRequest Request = new();
    }

    public class GetCheckYourAnswersResponse
    {
        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string ThisMonthProgressSummary { get; set; }
        public string NextMonthProgressSummary { get; set; }
        public bool? HelpNeeded { get; set; }
        public string SupportNeededReason { get; set; }
        public List<EProgressReportSupportType> SupportTypes { get; set; } = new List<EProgressReportSupportType>();
    }
}