using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonNeedsSupport.GetReasonNeedsSupport;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.ProgressReporting;

public class GetReasonNeedsSupportTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IApplicationRepository> _applicationRepository;
    private readonly Mock<IBuildingDetailsRepository> _buildingDetailsRepository;
    private readonly Mock<IProgressReportingRepository> _progressReportingRepository;

    private readonly GetReasonNeedsSupportHandler _handler;

    private const string ApplicationReference = "APP12345";
    private const string BuildingName = "Test Building";

    public GetReasonNeedsSupportTests()
    {
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _applicationRepository = new Mock<IApplicationRepository>(MockBehavior.Strict);
        _buildingDetailsRepository = new Mock<IBuildingDetailsRepository>(MockBehavior.Strict);
        _progressReportingRepository = new Mock<IProgressReportingRepository>(MockBehavior.Strict);

        _handler = new GetReasonNeedsSupportHandler(
            _applicationDataProvider.Object,
            _buildingDetailsRepository.Object,
            _applicationRepository.Object,
            _progressReportingRepository.Object);
    }

    [Fact]
    public async Task Handler_Returns_Data_From_DB()
    {
        //Arrange
        var applicationId = Guid.NewGuid();
        const string reason = "Test reason.";

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(applicationId)
                                .Verifiable();

        _applicationRepository.Setup(x => x.GetApplicationReferenceNumber(applicationId))
                              .ReturnsAsync(ApplicationReference)
                              .Verifiable();

        _buildingDetailsRepository.Setup(x => x.GetBuildingUniqueName(applicationId))
                                  .ReturnsAsync(BuildingName)
                                  .Verifiable();

        _progressReportingRepository.Setup(x => x.GetProgressReportSupportNeededReason())
                                    .ReturnsAsync(reason)
                                    .Verifiable();

        _progressReportingRepository.Setup(x => x.GetProgressReportLeadDesignerNeedsSupport())
                            .ReturnsAsync(true)
                            .Verifiable();
        _progressReportingRepository.Setup(x => x.GetProgressReportOtherMembersNeedsSupport())
                                    .ReturnsAsync(true)
                                    .Verifiable();
        _progressReportingRepository.Setup(x => x.GetProgressReportPlanningPermissionNeedsSupport())
                            .ReturnsAsync(true)
                            .Verifiable();
        _progressReportingRepository.Setup(x => x.GetProgressReportQuotesNeedsSupport())
                            .ReturnsAsync(true)
                            .Verifiable();

        //// Act
        var result = await _handler.Handle(GetReasonNeedsSupportRequest.Request, CancellationToken.None);

        //// Assert
        Assert.NotNull(result);

        var resultValid = ((result != null) &&
                           (result.ApplicationReferenceNumber == ApplicationReference) &&
                           (result.BuildingName == BuildingName) &&
                           (result.SupportNeededReason == reason) &&
                           (result.LeadDesignerNeedsSupport == true) &&
                           (result.OtherMembersNeedsSupport == true) &&
                           (result.PlanningPermissionNeedsSupport == true) &&
                           (result.QuotesNeedsSupport == true));
        Assert.True(resultValid);

        _applicationDataProvider.Verify();
        _applicationRepository.Verify();
        _buildingDetailsRepository.Verify();
        _progressReportingRepository.Verify();
    }
}