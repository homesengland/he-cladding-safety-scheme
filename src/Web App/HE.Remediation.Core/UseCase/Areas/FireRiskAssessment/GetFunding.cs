using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAssessment;

public class GetFundingHandler : IRequestHandler<GetFundingRequest, GetFundingResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IFireRiskAssessmentRepository _fireRiskAssessmentRepository;

    public GetFundingHandler(IApplicationDataProvider applicationDataProvider, IFireRiskAssessmentRepository fireRiskAssessmentRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _fireRiskAssessmentRepository = fireRiskAssessmentRepository;
    }

    public async Task<GetFundingResponse> Handle(GetFundingRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        var funding = await _fireRiskAssessmentRepository.GetFraFunding(applicationId);

        var defects = await _fireRiskAssessmentRepository.GetFireRiskRating(applicationId);

        var visitedCheckYourAnswers = await _fireRiskAssessmentRepository.GetFraVisitedCheckYourAnswers(applicationId);

        return new GetFundingResponse
        {
            HasFunding = funding?.HasFunding,
            HasDefects = defects.HasInternalFireSafetyRisks,
            HasFundingType = funding?.HasFunding == true ? funding.FraFundingTypeId : null,
            HasNoFundingType = funding?.HasFunding == false ? funding.FraFundingTypeId : null,
            VisitedCheckYourAnswers = visitedCheckYourAnswers
        };
    }
}

public class GetFundingRequest : IRequest<GetFundingResponse>
{
    private GetFundingRequest()
    {
    }

    public static readonly GetFundingRequest Request = new();
}

public class GetFundingResponse
{
    public bool? HasFunding { get; set; }
    public bool? HasDefects { get; set; }
    public EFraFundingType? HasFundingType { get; set; }
    public EFraFundingType? HasNoFundingType { get; set; }
    public bool VisitedCheckYourAnswers { get; set; }
}