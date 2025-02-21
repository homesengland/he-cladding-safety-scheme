using AutoFixture;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;
using MediatR;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.ResponsibleEntities;

public class ResetResponsibleEntitiesSectionHandlerTests : TestBase
{
    private readonly Mock<IApplicationDataProvider> _mockApplicationDataProvider;
    private readonly Mock<IApplicationRepository> _mockApplicationRepository;
    private readonly Mock<IFileService> _mockFileService;
    private readonly Mock<IResponsibleEntityRepository> _mockResponsibleEntityRepository;
    private readonly Mock<IBankDetailsRepository> _mockBankDetailsRepository;

    private readonly ResetResponsibleEntitiesSectionHandler _sut;

    public ResetResponsibleEntitiesSectionHandlerTests()
    {
        _mockApplicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _mockApplicationRepository = new Mock<IApplicationRepository>(MockBehavior.Strict);
        _mockFileService = new Mock<IFileService>(MockBehavior.Strict);
        _mockResponsibleEntityRepository = new Mock<IResponsibleEntityRepository>(MockBehavior.Strict);
        _mockBankDetailsRepository = new Mock<IBankDetailsRepository>(MockBehavior.Strict);

        _sut = new ResetResponsibleEntitiesSectionHandler(
            _mockApplicationDataProvider.Object,
            _mockApplicationRepository.Object,
            _mockFileService.Object,
            _mockResponsibleEntityRepository.Object,
            _mockBankDetailsRepository.Object);
    }

    [Fact]
    public async Task Handle_DeletesAllFiles()
    {
        // Arrange
        var request = Fixture.Create<ResetResponsibleEntitiesSectionRequest>();
        var applicationId = ConfigureGetApplicationId();
        var files = Fixture.CreateMany<FileResult>().ToList();

        _mockApplicationRepository
            .Setup(x => x.GetResponsibleEntityEvidenceFiles(applicationId))
            .ReturnsAsync(files)
            .Verifiable();

        foreach (var file in files)
            _mockFileService
                .Setup(x => x.DeleteFile(file.Id + file.Extension))
                .Returns(Task.CompletedTask)
                .Verifiable();

        _mockResponsibleEntityRepository
            .Setup(x => x.ResetResponsibleEntitiesSection(It.IsAny<Guid>()))
            .Returns(Task.CompletedTask);

        _mockBankDetailsRepository
            .Setup(x => x.ResetBankDetails(applicationId))
            .Returns(Task.CompletedTask)
            .Verifiable();


        // Act
        await _sut.Handle(request, CancellationToken.None);

        // Assert
        _mockApplicationDataProvider.Verify();
        _mockApplicationRepository.Verify();
        _mockFileService.Verify();
        _mockBankDetailsRepository.Verify();
    }

    [Fact]
    public async Task Handle_CallsResetResponsibleEntitiesSection_WithCorrectParameters()
    {
        // Arrange
        var request = Fixture.Create<ResetResponsibleEntitiesSectionRequest>();
        var applicationId = ConfigureGetApplicationId();

        _mockApplicationRepository
            .Setup(x => x.GetResponsibleEntityEvidenceFiles(It.IsAny<Guid>()))
            .ReturnsAsync(Array.Empty<FileResult>());

        _mockResponsibleEntityRepository
            .Setup(x => x.ResetResponsibleEntitiesSection(applicationId))
            .Returns(Task.CompletedTask)
            .Verifiable();

        _mockBankDetailsRepository
            .Setup(x => x.ResetBankDetails(applicationId))
            .Returns(Task.CompletedTask)
            .Verifiable();


        // Act
        await _sut.Handle(request, CancellationToken.None);

        // Assert
        _mockApplicationDataProvider.Verify();
        _mockResponsibleEntityRepository.Verify();
        _mockBankDetailsRepository.Verify();
    }

    [Fact]
    public async Task Handle_ReturnsExpectedResponse()
    {
        // Arrange
        var request = Fixture.Create<ResetResponsibleEntitiesSectionRequest>();
        var applicationId = ConfigureGetApplicationId();

        _mockApplicationRepository
            .Setup(x => x.GetResponsibleEntityEvidenceFiles(It.IsAny<Guid>()))
            .ReturnsAsync(Array.Empty<FileResult>());

        _mockResponsibleEntityRepository
            .Setup(x => x.ResetResponsibleEntitiesSection(applicationId))
            .Returns(Task.CompletedTask);

        _mockBankDetailsRepository
            .Setup(x => x.ResetBankDetails(applicationId))
            .Returns(Task.CompletedTask)
            .Verifiable();


        // Act
        var result = await _sut.Handle(request, CancellationToken.None);

        // Assert
        Assert.Equal(Unit.Value, result);
    }

    private Guid ConfigureGetApplicationId()
    {
        var applicationId = Fixture.Create<Guid>();

        _mockApplicationDataProvider
            .Setup(x => x.GetApplicationId())
            .Returns(applicationId)
            .Verifiable();

        return applicationId;
    }
}