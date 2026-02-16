
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PreTenderSupport.EmailContactDetails;

public class GetEmailContactDetailsHandler : IRequestHandler<GetEmailContactDetailsRequest, GetEmailContactDetailsResponse>
{
    private readonly IDbConnectionWrapper _dbConnectionWrapper;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IPreTenderRepository _preTenderRepository;

    public GetEmailContactDetailsHandler(
        IDbConnectionWrapper dbConnectionWrapper, 
        IApplicationDataProvider applicationDataProvider, 
        IPreTenderRepository preTenderRepository)
    {
        _dbConnectionWrapper = dbConnectionWrapper;
        _applicationDataProvider = applicationDataProvider;
        _preTenderRepository = preTenderRepository;
    }

    public async ValueTask<GetEmailContactDetailsResponse> Handle(GetEmailContactDetailsRequest request, CancellationToken cancellationToken)
    {
        var results = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<GetEmailContactDetailsResponse>("GetGrantFundingSignatoryDetails", new
        {
            Id = request.Id
        });

        var isSubmitted = await _preTenderRepository.IsPreTenderSubmitted(_applicationDataProvider.GetApplicationId());

        if (results is not null)
        {
            results.IsSubmitted = isSubmitted;
        }

        return results ?? new GetEmailContactDetailsResponse
        {
            IsSubmitted = isSubmitted
        };
    }
}
        