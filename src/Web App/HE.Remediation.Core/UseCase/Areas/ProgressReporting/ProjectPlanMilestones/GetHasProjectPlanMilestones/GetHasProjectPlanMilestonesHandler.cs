using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ProjectPlanMilestones;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ProjectPlanMilestones.GetHasProjectPlanMilestones
{
    public class GetHasProjectPlanMilestonesHandler : IRequestHandler<GetHasProjectPlanMilestonesRequest, GetHasProjectPlanMilestonesResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IBuildingDetailsRepository _buildingDetailsRepository;
        private readonly IProgressReportingRepository _progressReportingRepository;

        public GetHasProjectPlanMilestonesHandler(IApplicationDataProvider applicationDataProvider,
                                    IBuildingDetailsRepository buildingDetailsRepository,
                                    IApplicationRepository applicationRepository,
                                    IProgressReportingRepository progressReportingRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _buildingDetailsRepository = buildingDetailsRepository;
            _applicationRepository = applicationRepository;
            _progressReportingRepository = progressReportingRepository;
        }

        public async Task<GetHasProjectPlanMilestonesResponse> Handle(GetHasProjectPlanMilestonesRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
            var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

            var hasProjectPlanMilestones = await _progressReportingRepository.GetProgressReportHasProjectPlanMilestones();

            var version = await _progressReportingRepository.GetProgressReportVersion();

            return new GetHasProjectPlanMilestonesResponse
            {
                BuildingName = buildingName,
                ApplicationReferenceNumber = applicationReferenceNumber,
                HasProjectPlanMilestones = hasProjectPlanMilestones,
                Version = version
            };
        }
    }
}
