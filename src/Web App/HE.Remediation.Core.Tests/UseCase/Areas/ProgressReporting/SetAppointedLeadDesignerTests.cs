using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.AppointedLeadDesigner.SetAppointedLeadDesigner;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.ProgressReporting;

public class SetAppointedLeadDesignerTests
{
    private readonly Mock<IProgressReportingRepository> _progressReportingRepository;

    private readonly SetAppointedLeadDesignerHandler _handler;

    public SetAppointedLeadDesignerTests()
    {
        _progressReportingRepository = new Mock<IProgressReportingRepository>(MockBehavior.Strict);

        _handler = new SetAppointedLeadDesignerHandler(_progressReportingRepository.Object);
    }

    [Fact]
    public async Task Handler_Sets_LeadDesignerAppointed_When_True()
    {
        //Arrange
        const bool leadDesignerAppointed = true;

        var request = new SetAppointedLeadDesignerRequest
        {
            LeadDesignerAppointed = leadDesignerAppointed
        };

        _progressReportingRepository.Setup(x => x.UpdateLeadDesignerAppointed(It.IsAny<bool?>()))
                                    .Returns(Task.CompletedTask)
                                    .Verifiable();
        _progressReportingRepository.Setup(x => x.DeleteLeadDesignerNotAppointedReason())
                                    .Returns(Task.CompletedTask)
                                    .Verifiable();

        //// Act
        var result = await _handler.Handle(request, CancellationToken.None);

        //// Assert
        _progressReportingRepository.Verify();
        _progressReportingRepository.Verify(x => x.UpdateLeadDesignerAppointed(leadDesignerAppointed),
                                                 Times.Once);
    }

    [Fact]
    public async Task Handler_Sets_LeadDesignerAppointed_When_False()
    {
        //Arrange
        const bool leadDesignerAppointed = false;

        var request = new SetAppointedLeadDesignerRequest
        {
            LeadDesignerAppointed = leadDesignerAppointed
        };

        _progressReportingRepository.Setup(x => x.UpdateLeadDesignerAppointed(It.IsAny<bool?>()))
                                    .Returns(Task.CompletedTask)
                                    .Verifiable();
        _progressReportingRepository.Setup(x => x.DeleteLeadDesignerForCurrentProgressReport())
                                    .Returns(Task.CompletedTask)
                                    .Verifiable();

        //// Act
        var result = await _handler.Handle(request, CancellationToken.None);

        //// Assert
        _progressReportingRepository.Verify();
        _progressReportingRepository.Verify(x => x.UpdateLeadDesignerAppointed(leadDesignerAppointed),
                                                 Times.Once);
    }
}