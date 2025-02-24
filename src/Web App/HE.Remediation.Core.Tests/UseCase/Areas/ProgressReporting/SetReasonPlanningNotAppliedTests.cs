using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonPlanningNotApplied.SetReasonPlanningNotApplied;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.ProgressReporting;

public class SetReasonPlanningNotAppliedTests
{
    private readonly Mock<IProgressReportingRepository> _progressReportingRepository;

    private readonly SetReasonPlanningNotAppliedHandler _handler;

    public SetReasonPlanningNotAppliedTests()
    {
        _progressReportingRepository = new Mock<IProgressReportingRepository>(MockBehavior.Strict);

        _handler = new SetReasonPlanningNotAppliedHandler(_progressReportingRepository.Object);
    }

    [Fact]
    public async Task Handler_Sets_PlanningNotAppliedReason()
    {
        //Arrange
        const string reason = "Test reason.";
        const bool needsSupport = true;

        var request = new SetReasonPlanningNotAppliedRequest
        {
            ReasonPlanningPermissionNotApplied = reason,
            PlanningPermissionNeedsSupport = needsSupport
        };

        _progressReportingRepository.Setup(x => x.UpdatePlanningPermissionNotAppliedReason(It.IsAny<string>(), It.IsAny<bool?>()))
                                    .Returns(Task.CompletedTask)
                                    .Verifiable();

        //// Act
        var result = await _handler.Handle(request, CancellationToken.None);

        //// Assert
        _progressReportingRepository.Verify();
        _progressReportingRepository.Verify(x => x.UpdatePlanningPermissionNotAppliedReason(reason, needsSupport),
                                                 Times.Once);
    }
}