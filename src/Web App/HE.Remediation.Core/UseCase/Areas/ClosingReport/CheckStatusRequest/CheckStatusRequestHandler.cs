using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.CheckStatusRequest
{
    public class CheckStatusRequestHandler : IRequestHandler<CheckStatusRequest, CheckStatusResponse>
    {
        private readonly IClosingReportRepository _closingReportRepository;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public CheckStatusRequestHandler(IClosingReportRepository closingReportRepository, IApplicationDataProvider applicationDataProvider)
        {
            _closingReportRepository = closingReportRepository;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<CheckStatusResponse> Handle(CheckStatusRequest request, CancellationToken cancellationToken)
        {
            var isSubmitted = await _closingReportRepository.IsClosingReportSubmitted(_applicationDataProvider.GetApplicationId());

            return new CheckStatusResponse
            {
                IsSubmitted = isSubmitted
            };
        }
    }
}
