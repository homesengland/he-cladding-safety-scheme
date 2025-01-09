using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.WorksRequirePermission.SetWorksRequirePermission;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.ProgressReporting;

public class SetWorksRequirePermissionTests
{
    private readonly Mock<IProgressReportingRepository> _progressReportingRepository;

    private readonly SetWorksRequirePermissionHandler _handler;

    public SetWorksRequirePermissionTests()
    {
        _progressReportingRepository = new Mock<IProgressReportingRepository>(MockBehavior.Strict);

        _handler = new SetWorksRequirePermissionHandler(_progressReportingRepository.Object);
    }

    [Fact]
    public async Task Handler_Sets_WorksRequirePermission_When_True()
    {
        //Arrange
        const EYesNoNonBoolean worksRequirePermission = EYesNoNonBoolean.Yes;

        var request = new SetWorksRequirePermissionRequest
        {
            PermissionRequired  = worksRequirePermission
        };

        _progressReportingRepository.Setup(x => x.UpdateRequirePlanningPermission(It.IsAny<EYesNoNonBoolean?>()))
                                    .Returns(Task.CompletedTask)
                                    .Verifiable();

        //// Act
        var result = await _handler.Handle(request, CancellationToken.None);

        //// Assert
        _progressReportingRepository.Verify();
        _progressReportingRepository.Verify(x => x.UpdateRequirePlanningPermission(worksRequirePermission),
                                                 Times.Once);
    }

    [Fact]
    public async Task Handler_Sets_WorksRequirePermission_When_False()
    {
        //Arrange
        const EYesNoNonBoolean worksRequirePermission = EYesNoNonBoolean.No;

        var request = new SetWorksRequirePermissionRequest
        {
            PermissionRequired = worksRequirePermission
        };

        _progressReportingRepository.Setup(x => x.UpdateRequirePlanningPermission(It.IsAny<EYesNoNonBoolean?>()))
                                    .Returns(Task.CompletedTask)
                                    .Verifiable();

        //// Act
        var result = await _handler.Handle(request, CancellationToken.None);

        //// Assert
        _progressReportingRepository.Verify();
        _progressReportingRepository.Verify(x => x.UpdateRequirePlanningPermission(worksRequirePermission),
                                                 Times.Once);
    }
}