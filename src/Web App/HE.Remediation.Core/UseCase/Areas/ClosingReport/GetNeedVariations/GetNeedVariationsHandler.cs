using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.GetNeedVariations
{
    public class GetNeedVariationsHandler : IRequestHandler<GetNeedVariationsRequest, GetNeedVariationsReponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IBuildingDetailsRepository _buildingDetailsRepository;
        private readonly IClosingReportRepository _closingReportRepository;

        public GetNeedVariationsHandler(IApplicationDataProvider applicationDataProvider,
            IApplicationRepository applicationRepository,
            IBuildingDetailsRepository buildingDetailsRepository,
            IClosingReportRepository closingReportRepository)
        {
            _applicationDataProvider = applicationDataProvider;
            _applicationRepository = applicationRepository;
            _buildingDetailsRepository = buildingDetailsRepository;
            _closingReportRepository = closingReportRepository;
        }

        public async ValueTask<GetNeedVariationsReponse> Handle(GetNeedVariationsRequest request,
            CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
            var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

            var needVariations = await _closingReportRepository.GetClosingReportNeedVariations(applicationId);
            var isSubmitted = await _closingReportRepository.IsClosingReportSubmitted(applicationId);

            return new GetNeedVariationsReponse
            {
                IsSubmitted = isSubmitted,
                NeedVariations = needVariations,
                BuildingName = buildingName,
                ApplicationReferenceNumber = applicationReferenceNumber
            };
        }
    }
}