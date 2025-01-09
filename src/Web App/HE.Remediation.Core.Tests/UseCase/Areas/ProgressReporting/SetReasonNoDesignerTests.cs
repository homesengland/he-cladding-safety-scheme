using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonNoDesigner.SetReasonNoDesigner;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.ProgressReporting;

public class SetReasonNoDesignerTests
{
    private readonly Mock<IProgressReportingRepository> _progressReportingRepository;

    private readonly SetReasonNoDesignerHandler _handler;

    public SetReasonNoDesignerTests()
    {
        _progressReportingRepository = new Mock<IProgressReportingRepository>(MockBehavior.Strict);

        _handler = new SetReasonNoDesignerHandler(_progressReportingRepository.Object);
    }

    [Fact]
    public async Task Handler_Sets_LeadDesignerNotAppointedReason()
    {
        //Arrange
        const string reason = "Test reason.";
        const bool needsSupport = true;

        var request = new SetReasonNoDesignerRequest
        {
            LeadDesignerNotAppointedReason = reason,
            LeadDesignerNeedsSupport = needsSupport
        };

        _progressReportingRepository.Setup(x => x.UpdateLeadDesignerNotAppointedReason(It.IsAny<string>(), It.IsAny<bool?>()))
                                    .Returns(Task.CompletedTask)
                                    .Verifiable();

        //// Act
        var result = await _handler.Handle(request, CancellationToken.None);

        //// Assert
        _progressReportingRepository.Verify();
        _progressReportingRepository.Verify(x => x.UpdateLeadDesignerNotAppointedReason(reason, needsSupport),
                                                 Times.Once);
    }
}