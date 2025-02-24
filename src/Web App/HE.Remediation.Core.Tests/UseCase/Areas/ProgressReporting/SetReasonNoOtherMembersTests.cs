using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonNoOtherMembers.SetReasonNoOtherMembers;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.ProgressReporting;

public class SetReasonNoOtherMembersTests
{
    private readonly Mock<IProgressReportingRepository> _progressReportingRepository;

    private readonly SetReasonNoOtherMembersHandler _handler;

    public SetReasonNoOtherMembersTests()
    {
        _progressReportingRepository = new Mock<IProgressReportingRepository>(MockBehavior.Strict);

        _handler = new SetReasonNoOtherMembersHandler(_progressReportingRepository.Object);
    }

    [Fact]
    public async Task Handler_Sets_LeadOtherMembersNotAppointedReason()
    {
        //Arrange
        const string reason = "Test reason.";
        const bool needsSupport = true;

        var request = new SetReasonNoOtherMembersRequest
        {
            OtherMembersNotAppointedReason = reason,
            OtherMembersNeedsSupport = needsSupport
        };

        _progressReportingRepository.Setup(x => x.UpdateOtherMembersNotAppointedReason(It.IsAny<string>(), It.IsAny<bool?>()))
                                    .Returns(Task.CompletedTask)
                                    .Verifiable();

        //// Act
        var result = await _handler.Handle(request, CancellationToken.None);

        //// Assert
        _progressReportingRepository.Verify();
        _progressReportingRepository.Verify(x => x.UpdateOtherMembersNotAppointedReason(reason, needsSupport),
                                                 Times.Once);
    }
}