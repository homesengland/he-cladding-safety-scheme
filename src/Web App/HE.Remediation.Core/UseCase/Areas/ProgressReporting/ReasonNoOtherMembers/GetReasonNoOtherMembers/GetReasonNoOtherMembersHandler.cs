using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonNoOtherMembers.GetReasonNoOtherMembers;

public class GetReasonNoOtherMembersHandler : IRequestHandler<GetReasonNoOtherMembersRequest, GetReasonNoOtherMembersResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public GetReasonNoOtherMembersHandler(IApplicationDataProvider applicationDataProvider,
                                          IBuildingDetailsRepository buildingDetailsRepository,
                                          IApplicationRepository applicationRepository,
                                          IProgressReportingRepository progressReportingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _progressReportingRepository = progressReportingRepository;
    }

    public async ValueTask<GetReasonNoOtherMembersResponse> Handle(GetReasonNoOtherMembersRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var otherMembersNotAppointed = await _progressReportingRepository.GetProgressReportOtherMembersNotAppointedReason();
        var hasVisitedCheckYourAnswers = await _progressReportingRepository.GetHasVisitedCheckYourAnswers(
            new GetHasVisitedCheckYourAnswersParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = _applicationDataProvider.GetProgressReportId()
            });

        return new GetReasonNoOtherMembersResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            OtherMembersNotAppointedReason = otherMembersNotAppointed?.OtherMembersNotAppointedReason,
            OtherMembersNeedsSupport = otherMembersNotAppointed?.OtherMembersNeedsSupport,
            HasVisitedCheckYourAnswers = hasVisitedCheckYourAnswers
        };
    }
}
