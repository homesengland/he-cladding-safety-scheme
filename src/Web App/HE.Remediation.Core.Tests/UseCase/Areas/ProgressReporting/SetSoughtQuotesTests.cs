using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.SoughtQuotes.SetSoughtQuotes;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.ProgressReporting;

public class SetSoughtQuotesTests
{
    private readonly Mock<IProgressReportingRepository> _progressReportingRepository;

    private readonly SetSoughtQuotesHandler _handler;

    public SetSoughtQuotesTests()
    {
        _progressReportingRepository = new Mock<IProgressReportingRepository>(MockBehavior.Strict);

        _handler = new SetSoughtQuotesHandler(_progressReportingRepository.Object);
    }

    [Fact]
    public async Task Handler_Sets_QuotesSought()
    {
        //Arrange
        const bool quotesSought = true;

        var request = new SetSoughtQuotesRequest
        {
            QuotesSought = quotesSought
        };

        _progressReportingRepository.Setup(x => x.UpdateQuotesSought(It.IsAny<bool?>()))
                                    .Returns(Task.CompletedTask)
                                    .Verifiable();

        //// Act
        var result = await _handler.Handle(request, CancellationToken.None);

        //// Assert
        _progressReportingRepository.Verify();
        _progressReportingRepository.Verify(x => x.UpdateQuotesSought(quotesSought),
                                                 Times.Once);
    }
}