using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.InformedLeaseholder.SetInformedLeaseholder;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.ProgressReporting;

public class SetInformedLeaseholderTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IApplicationRepository> _applicationRepository;
    private readonly Mock<IProgressReportingRepository> _progressReportingRepository;

    private readonly SetInformedLeaseholderHandler _handler;

    public SetInformedLeaseholderTests()
    {
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _applicationRepository = new Mock<IApplicationRepository>(MockBehavior.Strict);
        _progressReportingRepository = new Mock<IProgressReportingRepository>(MockBehavior.Strict);

        _handler = new SetInformedLeaseholderHandler(_applicationDataProvider.Object, _applicationRepository.Object, _progressReportingRepository.Object);
    }

    [Fact(Skip ="Need to fix")]
    public async Task Handler_Sets_LeaseholdersInformed()
    {
        //Arrange
        const bool leaseholdersInformed = true;

        var request = new SetInformedLeaseholderRequest
        {
            LeaseholdersInformed = leaseholdersInformed
        };

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                        .Returns(Guid.NewGuid())
                        .Verifiable();

        _applicationRepository.Setup(x => x.UpdateInternalStatus(It.IsAny<Guid>(), It.IsAny<EApplicationInternalStatus>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        _progressReportingRepository.Setup(x => x.UpdateLeaseholdersInformed(It.IsAny<bool?>()))
                                    .Returns(Task.CompletedTask)
                                    .Verifiable();

        //// Act
        var result = await _handler.Handle(request, CancellationToken.None);

        //// Assert
        _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);
        _applicationRepository.Verify();
        _progressReportingRepository.Verify();
        _progressReportingRepository.Verify(x => x.UpdateLeaseholdersInformed(leaseholdersInformed),
                                                 Times.Once);
    }
}