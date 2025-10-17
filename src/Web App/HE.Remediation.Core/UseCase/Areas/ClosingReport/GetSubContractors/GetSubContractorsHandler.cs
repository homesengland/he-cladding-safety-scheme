using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.GetSubContractors;

public class GetSubContractorsHandler : IRequestHandler<GetSubContractorsRequest, GetSubContractorsResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IClosingReportRepository _closingReportRepository;
    private readonly ISubContractorSurveyRepository _subContractorSurveyRepository;

    public GetSubContractorsHandler(IApplicationDataProvider applicationDataProvider,
        IApplicationRepository applicationRepository,
        IBuildingDetailsRepository buildingDetailsRepository,
        IClosingReportRepository closingReportRepository, 
        ISubContractorSurveyRepository subContractorSurveyRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _closingReportRepository = closingReportRepository;
        _subContractorSurveyRepository = subContractorSurveyRepository;
    }

    public async Task<GetSubContractorsResponse> Handle(GetSubContractorsRequest request,
        CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var ratingsId = await _closingReportRepository.GetSubcontractorSurveyId(applicationId);
        if(!ratingsId.HasValue)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            ratingsId = await _subContractorSurveyRepository.CreateSurvey(applicationId);
            await _subContractorSurveyRepository.AddSubcontractorSurveyLeadContractor(applicationId, ratingsId.Value);
            await _closingReportRepository.UpdateSubcontractorSurveyId(applicationId, ratingsId.Value);
            scope.Complete();
        }

        var subContractors = await _subContractorSurveyRepository.GetSurveyOverview(ratingsId.Value);
        var isSubmitted = await _closingReportRepository.IsClosingReportSubmitted(applicationId);

        return new GetSubContractorsResponse
        {
            SubContractors = subContractors,
            IsSubmitted = isSubmitted,
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber
        };
    }
}