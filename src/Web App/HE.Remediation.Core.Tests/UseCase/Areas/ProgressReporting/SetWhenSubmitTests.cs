using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.WhenSubmit.SetWhenSubmit;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.ProgressReporting;

public class SetWhenSubmitTests
{
    private readonly Mock<IProgressReportingRepository> _progressReportingRepository;

    private readonly SetWhenSubmitHandler _handler;

    public SetWhenSubmitTests()
    {
        _progressReportingRepository = new Mock<IProgressReportingRepository>(MockBehavior.Strict);

        _handler = new SetWhenSubmitHandler(_progressReportingRepository.Object);
    }

    [Fact]
    public async Task Handler_Sets_WorksPackageSubmissionDate()
    {
        //Arrange
        var submissionDate = DateTime.Parse("2023-06-30");

        var request = new SetWhenSubmitRequest
        {
            SubmissionMonth = submissionDate.Month,
            SubmissionYear = submissionDate.Year
        };

        _progressReportingRepository.Setup(x => x.UpdateProgressReportExpectedWorksPackageSubmissionDate(It.IsAny<DateTime?>()))
                                    .Returns(Task.CompletedTask)
                                    .Verifiable();

        _progressReportingRepository.Setup(x => x.GetProgressReportLeadDesignerNeedsSupport())
                                    .ReturnsAsync(false)
                                    .Verifiable();
        _progressReportingRepository.Setup(x => x.GetProgressReportOtherMembersNeedsSupport())
                                    .ReturnsAsync(false)
                                    .Verifiable();
        _progressReportingRepository.Setup(x => x.GetProgressReportQuotesNeedsSupport())
                                    .ReturnsAsync(false)
                                    .Verifiable();
        _progressReportingRepository.Setup(x => x.GetProgressReportPlanningPermissionNeedsSupport())
                                    .ReturnsAsync(false)
                                    .Verifiable();

        //// Act
        var result = await _handler.Handle(request, CancellationToken.None);

        //// Assert
        _progressReportingRepository.Verify();
        _progressReportingRepository.Verify(x => x.UpdateProgressReportExpectedWorksPackageSubmissionDate(submissionDate.Date),
                                                 Times.Once);
    }
}