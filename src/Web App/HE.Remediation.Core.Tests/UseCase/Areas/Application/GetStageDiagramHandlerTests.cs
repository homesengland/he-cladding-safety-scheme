using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Data.StoredProcedureResults.PaymentRequest;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Application.StageDiagram.GetStageDiagram;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.Application
{
    public class GetStageDiagramHandlerTests
    {
        private readonly Mock<IApplicationDataProvider> _mockApplicationDataProvider;
        private readonly Mock<IDbConnectionWrapper> _mockDb;
        private readonly Mock<IProgressReportingRepository> _mockProgressReportingRepository;
        private readonly Mock<IScheduleOfWorksRepository> _mockScheduleOfWorksRepository;
        private readonly Mock<IWorkPackageRepository> _mockWorkPackageRepository;
        private readonly Mock<IPaymentRequestRepository> _mockPaymentRequestRepository;
        private readonly Mock<IVariationRequestRepository> _mockVariationRequestRepository;
        private readonly Mock<IClosingReportRepository> _mockClosingReportRepository;
        private readonly Mock<IMilestoneRepository> _mockMilestoneRepository;
        private readonly GetStageDiagramHandler _sut;

        public GetStageDiagramHandlerTests()
        {
            _mockApplicationDataProvider = new Mock<IApplicationDataProvider>();
            _mockDb = new Mock<IDbConnectionWrapper>();
            _mockProgressReportingRepository = new Mock<IProgressReportingRepository>();
            _mockScheduleOfWorksRepository = new Mock<IScheduleOfWorksRepository>();
            _mockWorkPackageRepository = new Mock<IWorkPackageRepository>();
            _mockPaymentRequestRepository = new Mock<IPaymentRequestRepository>();
            _mockVariationRequestRepository = new Mock<IVariationRequestRepository>();
            _mockClosingReportRepository = new Mock<IClosingReportRepository>();
            _mockMilestoneRepository = new Mock<IMilestoneRepository>();

            _sut = new GetStageDiagramHandler(
                _mockApplicationDataProvider.Object,
                _mockDb.Object,
                _mockProgressReportingRepository.Object,
                _mockScheduleOfWorksRepository.Object,
                _mockWorkPackageRepository.Object,
                _mockPaymentRequestRepository.Object,
                _mockVariationRequestRepository.Object,
                _mockClosingReportRepository.Object,
                _mockMilestoneRepository.Object);
        }

        [Fact]
        public async Task HasThePrimaryReportBeenSubmittedShouldReturnTrue()
        {
            // Arrange
            _mockDb.Setup(d => d.QuerySingleOrDefaultAsync<GetStageDiagramResponse>("GetStageDiagram", It.IsAny<object>())).ReturnsAsync(new GetStageDiagramResponse());
            _mockDb.Setup(d => d.QuerySingleOrDefaultAsync<DateTime?>("GetApplicationSubmittedDate", It.IsAny<object>())).ReturnsAsync(DateTime.UtcNow);
            _mockPaymentRequestRepository.Setup(p => p.GetPaymentRequests()).ReturnsAsync(new List<PaymentRequestResult> { new() { } }.AsReadOnly());
            _mockProgressReportingRepository.Setup(p => p.HasSubmittedProgressReports()).ReturnsAsync(true);
            _mockProgressReportingRepository.Setup(p => p.GetProgressReports()).ReturnsAsync(new List<ProgressReportResult> { new() { DateSubmitted = DateTime.UtcNow, Version = 1 } }.AsReadOnly());

            // Act
            var response = await _sut.Handle(GetStageDiagramRequest.Request, CancellationToken.None);

            // Assert
            Assert.True(response.HasThePrimaryReportBeenSubmitted);
        }

        [Fact]
        public async Task HasThePrimaryReportBeenSubmittedShouldReturnFalseWithNoProgressReports()
        {
            // Arrange
            _mockDb.Setup(d => d.QuerySingleOrDefaultAsync<GetStageDiagramResponse>("GetStageDiagram", It.IsAny<object>())).ReturnsAsync(new GetStageDiagramResponse());
            _mockDb.Setup(d => d.QuerySingleOrDefaultAsync<DateTime?>("GetApplicationSubmittedDate", It.IsAny<object>())).ReturnsAsync(DateTime.UtcNow);
            _mockPaymentRequestRepository.Setup(p => p.GetPaymentRequests()).ReturnsAsync(new List<PaymentRequestResult> { new() { } }.AsReadOnly());
            _mockProgressReportingRepository.Setup(p => p.HasSubmittedProgressReports()).ReturnsAsync(true);
            _mockProgressReportingRepository.Setup(p => p.GetProgressReports()).ReturnsAsync(new List<ProgressReportResult> { new() }.AsReadOnly());

            // Act
            var response = await _sut.Handle(GetStageDiagramRequest.Request, CancellationToken.None);

            // Assert
            Assert.False(response.HasThePrimaryReportBeenSubmitted);
        }

        [Fact]
        public async Task HasThePrimaryReportBeenSubmittedShouldReturnFalseWithV1ButNoDateSubmitted()
        {
            // Arrange
            _mockDb.Setup(d => d.QuerySingleOrDefaultAsync<GetStageDiagramResponse>("GetStageDiagram", It.IsAny<object>())).ReturnsAsync(new GetStageDiagramResponse());
            _mockDb.Setup(d => d.QuerySingleOrDefaultAsync<DateTime?>("GetApplicationSubmittedDate", It.IsAny<object>())).ReturnsAsync(DateTime.UtcNow);
            _mockPaymentRequestRepository.Setup(p => p.GetPaymentRequests()).ReturnsAsync(new List<PaymentRequestResult> { new() { } }.AsReadOnly());
            _mockProgressReportingRepository.Setup(p => p.HasSubmittedProgressReports()).ReturnsAsync(true);
            _mockProgressReportingRepository.Setup(p => p.GetProgressReports()).ReturnsAsync(new List<ProgressReportResult> { new() { Version = 1 } }.AsReadOnly());

            // Act
            var response = await _sut.Handle(GetStageDiagramRequest.Request, CancellationToken.None);

            // Assert
            Assert.False(response.HasThePrimaryReportBeenSubmitted);
        }

        [Fact]
        public async Task HasThePrimaryReportBeenSubmittedShouldReturnFalseWithDateSubmittedButNoV1()
        {
            // Arrange
            _mockDb.Setup(d => d.QuerySingleOrDefaultAsync<GetStageDiagramResponse>("GetStageDiagram", It.IsAny<object>())).ReturnsAsync(new GetStageDiagramResponse());
            _mockDb.Setup(d => d.QuerySingleOrDefaultAsync<DateTime?>("GetApplicationSubmittedDate", It.IsAny<object>())).ReturnsAsync(DateTime.UtcNow);
            _mockPaymentRequestRepository.Setup(p => p.GetPaymentRequests()).ReturnsAsync(new List<PaymentRequestResult> { new() { } }.AsReadOnly());
            _mockProgressReportingRepository.Setup(p => p.HasSubmittedProgressReports()).ReturnsAsync(true);
            _mockProgressReportingRepository.Setup(p => p.GetProgressReports()).ReturnsAsync(new List<ProgressReportResult> { new() { DateSubmitted = DateTime.UtcNow, Version = 2 } }.AsReadOnly());

            // Act
            var response = await _sut.Handle(GetStageDiagramRequest.Request, CancellationToken.None);

            // Assert
            Assert.False(response.HasThePrimaryReportBeenSubmitted);
        }
    }
}
