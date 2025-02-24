using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.AppliedForPlanningPermission.SetAppliedForPlanningPermission;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.ProgressReporting;

public class SetAppliedForPlanningPermissionTests
{
    private readonly Mock<IProgressReportingRepository> _progressReportingRepository;

    private readonly SetAppliedForPlanningPermissionHandler _handler;

    public SetAppliedForPlanningPermissionTests()
    {
        _progressReportingRepository = new Mock<IProgressReportingRepository>(MockBehavior.Strict);

        _handler = new SetAppliedForPlanningPermissionHandler(_progressReportingRepository.Object);
    }

    [Fact]
    public async Task Handler_Sets_AppliedForPlanningPermission_When_True()
    {
        //Arrange
        const bool appliedForPlanningPermission = true;

        var request = new SetAppliedForPlanningPermissionRequest
        {
            AppliedForPlanningPermission = appliedForPlanningPermission
        };

        _progressReportingRepository.Setup(x => x.UpdateAppliedForPlanningPermission(It.IsAny<bool?>()))
                                    .Returns(Task.CompletedTask)
                                    .Verifiable();

        //// Act
        var result = await _handler.Handle(request, CancellationToken.None);

        //// Assert
        _progressReportingRepository.Verify();
        _progressReportingRepository.Verify(x => x.UpdateAppliedForPlanningPermission(appliedForPlanningPermission),
                                                 Times.Once);
    }

    [Fact]
    public async Task Handler_Sets_AppliedForPlanningPermission_When_False()
    {
        //Arrange
        const bool appliedForPlanningPermission = false;

        var request = new SetAppliedForPlanningPermissionRequest
        {
            AppliedForPlanningPermission = appliedForPlanningPermission
        };

        _progressReportingRepository.Setup(x => x.UpdateAppliedForPlanningPermission(It.IsAny<bool?>()))
                                    .Returns(Task.CompletedTask)
                                    .Verifiable();

        //// Act
        var result = await _handler.Handle(request, CancellationToken.None);

        //// Assert
        _progressReportingRepository.Verify();
        _progressReportingRepository.Verify(x => x.UpdateAppliedForPlanningPermission(appliedForPlanningPermission),
                                                 Times.Once);
    }
}