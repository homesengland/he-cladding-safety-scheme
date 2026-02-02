
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PreTenderSupport.ConfirmContactDetails.GetConfirmContactDetails;

public class GetConfirmContactDetailsHandler : IRequestHandler<GetConfirmContactDetailsRequest, GetConfirmContactDetailsResponse>
{
    private readonly IDbConnectionWrapper _dbConnectionWrapper;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IPreTenderRepository _preTenderRepo;
    private readonly IApplicationRepository _applicationRepository;

    public GetConfirmContactDetailsHandler(IDbConnectionWrapper dbConnectionWrapper, 
                                           IApplicationDataProvider applicationDataProvider,
                                           IPreTenderRepository preTenderRepo,
                                           IApplicationRepository applicationRepository)
    {
        _dbConnectionWrapper = dbConnectionWrapper;
        _applicationDataProvider = applicationDataProvider;
        _preTenderRepo = preTenderRepo;
        _applicationRepository = applicationRepository;
    }

    public async ValueTask<GetConfirmContactDetailsResponse> Handle(GetConfirmContactDetailsRequest request, CancellationToken cancellationToken)
    {           
        var applicationId = _applicationDataProvider.GetApplicationId();
                    
        var results = await _preTenderRepo.GetGrantFundingSignatories(applicationId);
        var bankDetailsRelationship = await _applicationRepository.GetBankDetailsRelationship(applicationId);

        if (results != null)
        {
            var response = new GetConfirmContactDetailsResponse
            {
                Signatures = results,
                BankDetailsRelationship = bankDetailsRelationship
            };
            return response;
        }

        return new GetConfirmContactDetailsResponse();
    }
}
