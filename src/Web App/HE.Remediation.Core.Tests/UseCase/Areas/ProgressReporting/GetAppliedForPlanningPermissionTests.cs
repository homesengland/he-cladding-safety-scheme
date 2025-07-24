using AutoFixture;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.AppliedForPlanningPermission.GetAppliedForPlanningPermission;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.ProgressReporting;

public class GetAppliedForPlanningPermissionTests : TestBase
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IApplicationRepository> _applicationRepository;
    private readonly Mock<IBuildingDetailsRepository> _buildingDetailsRepository;
    private readonly Mock<IProgressReportingRepository> _progressReportingRepository;

    private readonly GetAppliedForPlanningPermissionHandler _handler;

    private const string ApplicationReference = "APP12345";
    private const string BuildingName = "Test Building";

    public GetAppliedForPlanningPermissionTests()
    {
        _applicationDataProvider = new Mock<IApplicationDataProvider>();
        _applicationRepository = new Mock<IApplicationRepository>();
        _buildingDetailsRepository = new Mock<IBuildingDetailsRepository>();
        _progressReportingRepository = new Mock<IProgressReportingRepository>();

        _handler = new GetAppliedForPlanningPermissionHandler(
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

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(applicationId)
                                .Verifiable();

        _applicationRepository.Setup(x => x.GetApplicationReferenceNumber(applicationId))
                              .ReturnsAsync(ApplicationReference)
                              .Verifiable();

        _buildingDetailsRepository.Setup(x => x.GetBuildingUniqueName(applicationId))
                                  .ReturnsAsync(BuildingName)
                                  .Verifiable();

        _progressReportingRepository.Setup(x => x.GetProgressReportAppliedForPlanningPermission())
                                    .ReturnsAsync(true)
                                    .Verifiable();

        var version = Fixture.Create<int>();
        _progressReportingRepository.Setup(x => x.GetProgressReportVersion())
            .ReturnsAsync(version)
            .Verifiable();

        //// Act
        var result = await _handler.Handle(GetAppliedForPlanningPermissionRequest.Request, CancellationToken.None);

        //// Assert
        Assert.NotNull(result);

        var resultValid = ((result != null) &&
                           (result.ApplicationReferenceNumber == ApplicationReference) &&
                           (result.BuildingName == BuildingName) &&
                           (result.AppliedForPlanningPermission == true) &&
                           (result.Version == version));
        Assert.True(resultValid);

        _applicationDataProvider.Verify();
        _applicationRepository.Verify();
        _buildingDetailsRepository.Verify();
        _progressReportingRepository.Verify();
    }
}