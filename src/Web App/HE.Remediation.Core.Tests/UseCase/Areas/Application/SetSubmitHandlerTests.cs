using Moq;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Services.Communication;
using HE.Remediation.Core.Services.StatusTransition;
using HE.Remediation.Core.UseCase.Areas.Application.Submit.SetSubmit;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureResults;

namespace HE.Remediation.Core.Tests.UseCase.Areas.Application
{

    public class SetSubmitHandlerTests
    {
        private readonly Mock<IApplicationDataProvider> _mockApplicationDataProvider;
        private readonly Mock<IDbConnectionWrapper> _mockDbConnectionWrapper;
        private readonly Mock<IApplicationRepository> _mockApplicationRepository;
        private readonly Mock<ITaskRepository> _mockTaskRepository;
        private readonly Mock<ICommunicationService> _mockCommunicationService;
        private readonly Mock<IStatusTransitionService> _mockStatusTransitionService;
        private readonly SetSubmitHandler _handler;

        public SetSubmitHandlerTests()
        {
            _mockApplicationDataProvider = new Mock<IApplicationDataProvider>();
            _mockDbConnectionWrapper = new Mock<IDbConnectionWrapper>();
            _mockApplicationRepository = new Mock<IApplicationRepository>();
            _mockTaskRepository = new Mock<ITaskRepository>();
            _mockCommunicationService = new Mock<ICommunicationService>();
            _mockStatusTransitionService = new Mock<IStatusTransitionService>();

            _handler = new SetSubmitHandler(
                _mockDbConnectionWrapper.Object,
                _mockApplicationDataProvider.Object,
                _mockApplicationRepository.Object,
                _mockTaskRepository.Object,
                _mockCommunicationService.Object,
                _mockStatusTransitionService.Object);
        }

        [Fact]
        public async Task UpdateSubmitRequest_ShouldInsertTaskAndQueueEmail_WhenApplicationSchemeIsCladdingSafetyScheme()
        {
            // Arrange
            var applicationId = Guid.NewGuid();
            _mockApplicationDataProvider.Setup(x => x.GetApplicationScheme()).Returns(EApplicationScheme.CladdingSafetyScheme);
            _mockTaskRepository.Setup(x => x.GetTaskType(It.IsAny<GetTaskTypeParameters>())).ReturnsAsync(new GetTaskTypeResult { Id = 1 });
            _mockApplicationRepository.Setup(x => x.GetApplicationReferenceNumber(applicationId)).ReturnsAsync("REF123");

            // Act
            await InvokeUpdateSubmitRequest(applicationId);

            // Assert
            _mockTaskRepository.Verify(x => x.InsertTask(It.IsAny<InsertTaskParameters>()), Times.Once);
            _mockCommunicationService.Verify(x => x.QueueEmailCommunication(It.IsAny<EmailCommunicationRequest>()), Times.Once);
        }

        [Theory]
        [InlineData(EApplicationScheme.SelfRemediating)]
        [InlineData(EApplicationScheme.ResponsibleActorsScheme)]
        [InlineData(EApplicationScheme.SocialSector)]
        public async Task UpdateSubmitRequest_ShouldNotInsertTaskOrQueueEmail_WhenApplicationSchemeIsNotCladdingSafetyScheme(EApplicationScheme applicationScheme)
        {
            // Arrange
            var applicationId = Guid.NewGuid();
            _mockApplicationDataProvider.Setup(x => x.GetApplicationScheme()).Returns(applicationScheme);

            // Act
            await InvokeUpdateSubmitRequest(applicationId);

            // Assert
            _mockTaskRepository.Verify(x => x.InsertTask(It.IsAny<InsertTaskParameters>()), Times.Never);
            _mockCommunicationService.Verify(x => x.QueueEmailCommunication(It.IsAny<EmailCommunicationRequest>()), Times.Never);
        }

        [Theory]
        [InlineData(EApplicationScheme.SelfRemediating)]
        [InlineData(EApplicationScheme.ResponsibleActorsScheme)]
        [InlineData(EApplicationScheme.SocialSector)]
        public async Task UpdateSubmitRequest_ShouldAlwaysUpdateApplicationSubmitAndTransitionStatus(EApplicationScheme applicationScheme)
        {
            // Arrange
            var applicationId = Guid.NewGuid();
            _mockApplicationDataProvider.Setup(x => x.GetApplicationScheme()).Returns(applicationScheme);

            // Act
            await InvokeUpdateSubmitRequest(applicationId);

            // Assert
            _mockDbConnectionWrapper.Verify(x => x.ExecuteAsync("UpdateApplicationSubmit", It.IsAny<object>()), Times.Once);
            _mockStatusTransitionService.Verify(x => x.TransitionToInternalStatus(EApplicationInternalStatus.FinalApplicationSubmitted, null, applicationId), Times.Once);
        }

        private async Task InvokeUpdateSubmitRequest(Guid applicationId)
        {
            // A helper to call the private UpdateSubmitRequest method
            var method = typeof(SetSubmitHandler).GetMethod("UpdateSubmitRequest", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (method == null) throw new InvalidOperationException("Method UpdateSubmitRequest not found.");

            await (Task)method.Invoke(_handler, new object[] { applicationId });
        }
    }
}
