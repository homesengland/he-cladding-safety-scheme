using System.Globalization;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.WhenSubmit.SetWhenSubmit;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.ProgressReporting;

public class SetWhenSubmitTests
{
    private readonly Mock<IProgressReportingRepository> _progressReportingRepository;
    private readonly Mock<ITaskRepository> _taskRepository;
    private readonly Mock<IDateRepository> _dateRepository;
    private readonly Mock<IGrantFundingRepository> _grantFundingRepository;
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;

    private readonly SetWhenSubmitHandler _handler;

    public SetWhenSubmitTests()
    {
        _progressReportingRepository = new Mock<IProgressReportingRepository>();
        _taskRepository = new Mock<ITaskRepository>();
        _dateRepository = new Mock<IDateRepository>();
        _grantFundingRepository = new Mock<IGrantFundingRepository>();
        _applicationDataProvider = new Mock<IApplicationDataProvider>();

        _handler = new SetWhenSubmitHandler(_progressReportingRepository.Object, 
            _taskRepository.Object, _dateRepository.Object, 
            _grantFundingRepository.Object, _applicationDataProvider.Object);
    }

    [Theory] //    gfa date  = 31/01/2025
    [InlineData("01/05/2025", "31/05/2025", false)] // 5 months
    [InlineData("01/09/2025", "30/09/2025", false)] // 8 months
    [InlineData("31/08/2025", "31/08/2025", false)] // 9 months - 1 day
    [InlineData("01/10/2025", "31/10/2025", true)] // 9 months (exactly)
    [InlineData("01/11/2025", "30/11/2025", true)] // 9 months + 1 day
    public async Task Handler_Sets_WorksPackageSubmissionDate(string workPackageForecast, string expectedRoundedWorkPackageForecast, bool expectedTaskRaised)
    {
        //Arrange
        var gfaCompleteDate = DateTime.ParseExact("01/01/2025", "dd/MM/yyyy", CultureInfo.InvariantCulture); // dates are rounded to month end (so 31/01/2025)
        var forecastedSubmissionDate = DateTime.ParseExact(workPackageForecast, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        var expectedRoundedForecastedSubmissionDate = DateTime.ParseExact(expectedRoundedWorkPackageForecast, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        var expectedNineMonthTaskCreateTimes = expectedTaskRaised ? Times.Once() : Times.Never();

        var request = new SetWhenSubmitRequest
        {
            SubmissionMonth = forecastedSubmissionDate.Month,
            SubmissionYear = forecastedSubmissionDate.Year
        };

        _progressReportingRepository.Setup(x => x.UpdateProgressReportExpectedWorksPackageSubmissionDate(It.IsAny<DateTime?>()))
                                    .Returns(Task.CompletedTask)
                                    .Verifiable();

        _grantFundingRepository.Setup(x => x.GetGrantFundingAgreementCompleteDate(It.IsAny<Guid>()))
                                    .ReturnsAsync(gfaCompleteDate)
                                    .Verifiable();

        _taskRepository.Setup(x => x.GetTaskType(It.IsAny<GetTaskTypeParameters>()))
                                    .ReturnsAsync(new Data.StoredProcedureResults.GetTaskTypeResult())
                                    .Verifiable();

        _taskRepository.Setup(x => x.InsertTask(It.IsAny<InsertTaskParameters>()))
                                    .ReturnsAsync(new Data.StoredProcedureResults.GetTaskResult())
                                    .Verifiable();

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                    .Returns(Guid.NewGuid())
                                    .Verifiable();

        _dateRepository.Setup(x => x.AddWorkingDays(It.IsAny<AddWorkingDaysParameters>()))
                                    .ReturnsAsync(DateTime.UtcNow)
                                    .Verifiable();

        //// Act
        var result = await _handler.Handle(request, CancellationToken.None);

        //// Assert
        _progressReportingRepository.Verify();
        _progressReportingRepository.Verify(x => x.UpdateProgressReportExpectedWorksPackageSubmissionDate(expectedRoundedForecastedSubmissionDate.Date), Times.Once);

        _taskRepository.Verify(x => x.InsertTask(It.IsAny<InsertTaskParameters>()), expectedNineMonthTaskCreateTimes);
    }
}