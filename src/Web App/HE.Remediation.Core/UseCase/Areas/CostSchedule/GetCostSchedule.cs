using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.CostSchedule;

public class GetCostScheduleHandler : IRequestHandler<GetCostScheduleRequest, GetCostScheduleResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageRepository _workPackageRepository;


    public GetCostScheduleHandler(
        IApplicationDataProvider applicationDataProvider, 
        IApplicationRepository applicationRepository, 
        IBuildingDetailsRepository buildingDetailsRepository, 
        IWorkPackageRepository workPackageRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<GetCostScheduleResponse> Handle(GetCostScheduleRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var costs = await _workPackageRepository.GetLatestCostSchedule(applicationId);

        return new GetCostScheduleResponse
        {
            ApplicationReferenceNumber = reference,
            BuildingName = buildingName,

            UnsafeCladdingRemovalAmount = costs.RemovalOfCladdingAmount,
            UnsafeCladdingRemovalDescription = costs.RemovalOfCladdingDescription,

            NewCladdingAmount = costs.NewCladdingAmount,
            NewCladdingDescription = costs.NewCladdingDescription,
            ExternalWorksAmount = costs.OtherEligibleWorkToExternalWallAmount,
            ExternalWorksDescription = costs.OtherEligibleWorkToExternalWallDescription,
            InternalWorksAmount = costs.InternalMitigationWorksAmount,
            InternalWorksDescription = costs.InternalMitigationWorksDescription,

            MainContractorPreliminariesAmount = costs.MainContractorPreliminariesAmount,
            MainContractorPreliminariesDescription = costs.MainContractorPreliminariesDescription,
            AccessAmount = costs.AccessAmount,
            AccessDescription = costs.AccessDescription,
            MainContractorOverheadAmount = costs.OverheadsAndProfitAmount,
            MainContractorOverheadDescription = costs.OverheadsAndProfitDescription,
            ContractorContingenciesAmount = costs.ContractorContingenciesAmount,
            ContractorContingenciesDescription = costs.ContractorContingenciesDescription,

            FraewSurveyAmount = costs.FraewSurveyAmount,
            FeasibilityStageAmount = costs.FeasibilityStageAmount,
            FeasibilityStageDescription = costs.FeasibilityStageDescription,
            PostTenderStageAmount = costs.PostTenderStageAmount,
            PostTenderStageDescription = costs.PostTenderStageDescription,
            PropertyManagerAmount = costs.PropertyManagerAmount,
            PropertyManagerDescription = costs.PropertyManagerDescription,
            IrrecoverableVatAmount = costs.IrrecoverableVatAmount,
            IrrecoverableVatDescription = costs.IrrecoverableVatDescription,
            IneligibleAmount = costs.IneligibleAmount
        };
    }
}

public class GetCostScheduleRequest : IRequest<GetCostScheduleResponse>
{
    private GetCostScheduleRequest()
    {
    }

    public static readonly GetCostScheduleRequest Request = new();
}

public class GetCostScheduleResponse
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public decimal? UnsafeCladdingRemovalAmount { get; set; }
    public string UnsafeCladdingRemovalDescription { get; set; }
    
    public decimal? NewCladdingAmount { get; set; }
    public string NewCladdingDescription { get; set; }
    public decimal? ExternalWorksAmount { get; set; }
    public string ExternalWorksDescription { get; set; }
    public decimal? InternalWorksAmount { get; set; }
    public string InternalWorksDescription { get; set; }
    
    public decimal? MainContractorPreliminariesAmount { get; set; }
    public string MainContractorPreliminariesDescription { get; set; }
    public decimal? AccessAmount { get; set; }
    public string AccessDescription { get; set; }
    public decimal? MainContractorOverheadAmount { get; set; }
    public string MainContractorOverheadDescription { get; set; }
    public decimal? ContractorContingenciesAmount { get; set; }
    public string ContractorContingenciesDescription { get; set; }

    
    public decimal? FraewSurveyAmount { get; set; }
    public decimal? FeasibilityStageAmount { get; set; }
    public string FeasibilityStageDescription { get; set; }
    public decimal? PostTenderStageAmount { get; set; }
    public string PostTenderStageDescription { get; set; }
    public decimal? PropertyManagerAmount { get; set; }
    public string PropertyManagerDescription { get; set; }
    public decimal? IrrecoverableVatAmount { get; set; }
    public string IrrecoverableVatDescription { get; set; }
    public decimal? IneligibleAmount { get; set; }

}