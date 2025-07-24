using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.InformedLeaseholder.GetInformedLeaseholder;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.ProgressReporting;

public class GetInformedLeaseholderTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IApplicationRepository> _applicationRepository;
    private readonly Mock<IBuildingDetailsRepository> _buildingDetailsRepository;
    private readonly Mock<IProgressReportingRepository> _progressReportingRepository;

    private readonly GetInformedLeaseholderHandler _handler;

    private const string ApplicationReference = "APP12345";
    private const string BuildingName = "Test Building";

    public GetInformedLeaseholderTests()
    {
        _applicationDataProvider = new Mock<IApplicationDataProvider>();
        _applicationRepository = new Mock<IApplicationRepository>();
        _buildingDetailsRepository = new Mock<IBuildingDetailsRepository>();
        _progressReportingRepository = new Mock<IProgressReportingRepository>();

        _handler = new GetInformedLeaseholderHandler(
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

        _progressReportingRepository.Setup(x => x.GetProgressReportLeaseholdersInformed())
                                    .ReturnsAsync(true)
                                    .Verifiable();

        //// Act
        var result = await _handler.Handle(GetInformedLeaseholderRequest.Request, CancellationToken.None);

        //// Assert
        Assert.NotNull(result);

        var resultValid = ((result != null) &&
                           (result.ApplicationReferenceNumber == ApplicationReference) &&
                           (result.BuildingName == BuildingName) &&
                           (result.LeaseholdersInformed == true));
        Assert.True(resultValid);

        _applicationDataProvider.Verify();
        _applicationRepository.Verify();
        _buildingDetailsRepository.Verify();
        _progressReportingRepository.Verify();
    }
}