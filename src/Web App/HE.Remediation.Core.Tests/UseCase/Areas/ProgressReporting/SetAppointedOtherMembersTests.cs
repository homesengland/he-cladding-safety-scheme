using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.AppointedOtherMembers.SetAppointedOtherMembers;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.ProgressReporting;

public class SetAppointedOtherMembersTests
{
    private readonly Mock<IProgressReportingRepository> _progressReportingRepository;

    private readonly SetAppointedOtherMembersHandler _handler;

    public SetAppointedOtherMembersTests()
    {
        _progressReportingRepository = new Mock<IProgressReportingRepository>(MockBehavior.Strict);

        _handler = new SetAppointedOtherMembersHandler(_progressReportingRepository.Object);
    }

    [Fact]
    public async Task Handler_Sets_OtherMembersAppointed()
    {
        //Arrange
        const bool otherMembersAppointed = true;

        var request = new SetAppointedOtherMembersRequest
        {
            OtherMembersAppointed = otherMembersAppointed
        };

        _progressReportingRepository.Setup(x => x.UpdateOtherMembersAppointed(It.IsAny<bool?>()))
                                    .Returns(Task.CompletedTask)
                                    .Verifiable();

        //// Act
        var result = await _handler.Handle(request, CancellationToken.None);

        //// Assert
        _progressReportingRepository.Verify();
        _progressReportingRepository.Verify(x => x.UpdateOtherMembersAppointed(otherMembersAppointed),
                                                 Times.Once);
    }
}