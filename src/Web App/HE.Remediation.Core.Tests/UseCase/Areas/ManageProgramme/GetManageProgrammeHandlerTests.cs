using Dapper;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Application.ManageProgramme;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.ManageProgramme
{
    public class GetManageProgrammeHandlerTests
    {
        private readonly Mock<IApplicationDataProvider> _applicationDataProviderMock;
        private readonly Mock<IManageProgrammeRepository> _manageProgrammeRepositoryMock;
        private readonly GetManageProgrammeHandler _handler;

        public GetManageProgrammeHandlerTests()
        {
            _applicationDataProviderMock = new Mock<IApplicationDataProvider>();
            _manageProgrammeRepositoryMock = new Mock<IManageProgrammeRepository>();
            _handler = new GetManageProgrammeHandler(_applicationDataProviderMock.Object, _manageProgrammeRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ReturnsProgrammeApplications_WhenNoFilters()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _applicationDataProviderMock.Setup(x => x.GetUserId()).Returns(userId);

            var expected = new List<GetManageProgrammeResponse>
            {
                new GetManageProgrammeResponse { ApplicationId = Guid.NewGuid(), ApplicationNumber = "A1" }
            };

            _manageProgrammeRepositoryMock
                .Setup(x => x.GetManageProgrammeDetails(It.IsAny<GetManageProgrammeRequest>(), It.IsAny<Guid?>()))
                .ReturnsAsync(expected);

            var request = new GetManageProgrammeRequest
            {
                Search = null,
                SelectedSchemeTypeFilters = new List<EApplicationScheme>(),
                SelectedInvestigationCompletionYearFilters = new List<EFinancialYearFilter>(),
                SelectedStartOnSiteYearFilters = new List<EFinancialYearFilter>(),
                SelectedPracticalCompletionYearFilters = new List<EFinancialYearFilter>()
            };

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Single(result);
            Assert.Equal("A1", result.First().ApplicationNumber);
        }

        [Fact]
        public async Task Handle_PassesSearchParameterToRepository()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _applicationDataProviderMock.Setup(x => x.GetUserId()).Returns(userId);

            var expected = new List<GetManageProgrammeResponse>();
            GetManageProgrammeRequest? capturedParams = null;

            _manageProgrammeRepositoryMock
                .Setup(x => x.GetManageProgrammeDetails(It.IsAny<GetManageProgrammeRequest>(), It.IsAny<Guid?>()))
                .Callback<GetManageProgrammeRequest, Guid?>((p, u) => capturedParams = p)
                .ReturnsAsync(expected);

            var request = new GetManageProgrammeRequest
            {
                Search = "search-term",
                SelectedSchemeTypeFilters = new List<EApplicationScheme>(),
                SelectedInvestigationCompletionYearFilters = new List<EFinancialYearFilter>(),
                SelectedStartOnSiteYearFilters = new List<EFinancialYearFilter>(),
                SelectedPracticalCompletionYearFilters = new List<EFinancialYearFilter>()
            };

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(capturedParams);
            Assert.Equal("search-term", capturedParams.Search);
        }

        [Fact]
        public async Task Handle_FiltersByInvestigationCompletionYear()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _applicationDataProviderMock.Setup(x => x.GetUserId()).Returns(userId);

            var matchingDate = new DateTime(2024, 5, 1); // FY 2024/25
            var nonMatchingDate = new DateTime(2023, 5, 1); // FY 2023/24

            var responses = new List<GetManageProgrammeResponse>
            {
                new GetManageProgrammeResponse { ApplicationId = Guid.NewGuid(), InvestigationCompletionDate = matchingDate },
                new GetManageProgrammeResponse { ApplicationId = Guid.NewGuid(), InvestigationCompletionDate = nonMatchingDate }
            };

            _manageProgrammeRepositoryMock
                .Setup(x => x.GetManageProgrammeDetails(It.IsAny<GetManageProgrammeRequest>(), It.IsAny<Guid?>()))
                .ReturnsAsync(responses);

            var request = new GetManageProgrammeRequest
            {
                Search = null,
                SelectedSchemeTypeFilters = new List<EApplicationScheme>(),
                SelectedInvestigationCompletionYearFilters = new List<EFinancialYearFilter> { EFinancialYearFilter.FY24_25 },
                SelectedStartOnSiteYearFilters = new List<EFinancialYearFilter>(),
                SelectedPracticalCompletionYearFilters = new List<EFinancialYearFilter>()
            };

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Single(result);
            Assert.Equal(matchingDate, result.First().InvestigationCompletionDate);
        }
    }
}
