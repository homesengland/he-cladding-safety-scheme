using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonNeedsSupport.SetReasonNeedsSupport;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.ProgressReporting;

public class SetReasonNeedsSupportTests
{
    private readonly Mock<IProgressReportingRepository> _progressReportingRepository;

    private readonly SetReasonNeedsSupportHandler _handler;

    public SetReasonNeedsSupportTests()
    {
        _progressReportingRepository = new Mock<IProgressReportingRepository>(MockBehavior.Strict);

        _handler = new SetReasonNeedsSupportHandler(_progressReportingRepository.Object);
    }

    [Fact]
    public async Task Handler_Sets_NeedsSupportReason()
    {
        //Arrange
        const string reason = "Test reason.";

        var request = new SetReasonNeedsSupportRequest
        {
            SupportNeededReason = reason
        };

        _progressReportingRepository.Setup(x => x.UpdateProgressReportSupportNeededReason(It.IsAny<string>()))
                                    .Returns(Task.CompletedTask)
                                    .Verifiable();

        //// Act
        var result = await _handler.Handle(request, CancellationToken.None);

        //// Assert
        _progressReportingRepository.Verify();
        _progressReportingRepository.Verify(x => x.UpdateProgressReportSupportNeededReason(reason),
                                                 Times.Once);
    }
}