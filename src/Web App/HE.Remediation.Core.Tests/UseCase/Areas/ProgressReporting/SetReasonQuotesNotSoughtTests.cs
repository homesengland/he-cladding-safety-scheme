using AutoFixture;
using AutoFixture.Xunit2;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.ReasonQuotesNotSought.SetReasonQuotesNotSought;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.ProgressReporting;

public class SetReasonQuotesNotSoughtTests : TestBase
{
    private readonly Mock<IProgressReportingRepository> _progressReportingRepository;
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<ITaskRepository> _taskRepository;
    private readonly Mock<IDateRepository> _dateRepository;

    private readonly SetReasonQuotesNotSoughtHandler _handler;

    public SetReasonQuotesNotSoughtTests()
    {
        _progressReportingRepository = new Mock<IProgressReportingRepository>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _taskRepository = new Mock<ITaskRepository>();
        _dateRepository = new Mock<IDateRepository>(MockBehavior.Strict);

        _handler = new SetReasonQuotesNotSoughtHandler(_progressReportingRepository.Object, _applicationDataProvider.Object, _taskRepository.Object, _dateRepository.Object);
    }

    [Fact]
    public async Task Handler_Sets_QuotesNotSoughtReason()
    {
        //Arrange
        const EWhyYouHaveNotSoughtQuotes whyYouHaveNotSoughtQuotes = EWhyYouHaveNotSoughtQuotes.NotReadyForTender;
        const string reason = "Test reason.";
        const bool needsSupport = true;

        var request = new SetReasonQuotesNotSoughtRequest
        {
            WhyYouHaveNotSoughtQuotes = whyYouHaveNotSoughtQuotes,
            QuotesNotSoughtReason = reason,
            QuotesNeedsSupport = needsSupport
        };

        _progressReportingRepository.Setup(x => x.UpdateQuotesNotSoughtReason(It.IsAny<EWhyYouHaveNotSoughtQuotes?>(), It.IsAny<string>(), It.IsAny<bool?>()))
                                    .Returns(Task.CompletedTask)
                                    .Verifiable();

        var dateInFiveWorkingDays = DateTime.UtcNow.AddDays(5);
        _dateRepository.Setup(x => x.AddWorkingDays(It.IsAny<AddWorkingDaysParameters>())).ReturnsAsync(dateInFiveWorkingDays);

        var applicationId = Guid.NewGuid();
        _applicationDataProvider.Setup(x => x.GetApplicationId())
            .Returns(applicationId)
            .Verifiable();

        var taskType = Fixture.Create<GetTaskTypeResult>();
        _taskRepository.Setup(x => x.GetTaskType(It.Is<GetTaskTypeParameters>(obj =>
                obj.ParentType == "Progress Report"
                && obj.ChildType == "Review")))
            .ReturnsAsync(taskType)
            .Verifiable();

        if (request.WhyYouHaveNotSoughtQuotes == EWhyYouHaveNotSoughtQuotes.IDontPlanTo)
        {
            _taskRepository
            .Setup(x => x.InsertTask(It.Is<InsertTaskParameters>(obj =>
                obj.ReferenceId == applicationId
                && obj.AssignedToTeamId == (int)ETeam.DaviesOps
                && obj.AssignedToUserId == null
                && obj.CreatedByUserId == null
                && obj.Description == "Please contact applicant as not planning on running open tender"
                && obj.RequiredByDate == DateOnly.FromDateTime(dateInFiveWorkingDays)
                && obj.TopicId == null
                && obj.Notes == string.Empty
                && obj.TaskStatus == ETaskStatus.NotStarted.ToString()
                && obj.TaskTypeId == taskType.Id)))
            .Verifiable();
        }

        //// Act
        var result = await _handler.Handle(request, CancellationToken.None);

        //// Assert
        _progressReportingRepository.Verify();
        _progressReportingRepository.Verify(x => x.UpdateQuotesNotSoughtReason(whyYouHaveNotSoughtQuotes, reason, needsSupport),
                                                 Times.Once);
    }
}