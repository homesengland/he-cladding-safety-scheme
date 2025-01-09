using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.PreTenderSupport.ClaimPreTender.GetClaimPretenderSupport;

public class GetClaimPretenderSupportHandler: IRequestHandler<GetClaimPretenderSupportRequest, GetClaimPretenderSupportResponse>
{
    private readonly IDbConnectionWrapper _dbConnectionWrapper;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IPreTenderRepository _preTenderRepo;

    public GetClaimPretenderSupportHandler(IDbConnectionWrapper dbConnectionWrapper, 
                                           IApplicationDataProvider applicationDataProvider,
                                           IPreTenderRepository preTenderRepo)
    {
        _dbConnectionWrapper = dbConnectionWrapper;
        _applicationDataProvider = applicationDataProvider;
        _preTenderRepo = preTenderRepo;
    }

    public async Task<GetClaimPretenderSupportResponse> Handle(GetClaimPretenderSupportRequest request, CancellationToken cancellationToken)
    {           
        var applicationId = _applicationDataProvider.GetApplicationId();

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var relationship = await _preTenderRepo.GetApplicationBankAccountRelationship(applicationId);
        var claimAmount = await _preTenderRepo.GetApplicationPTFSClaimAmount(applicationId);        
        scope.Complete();
                
        var viewModel = new GetClaimPretenderSupportResponse
        {
            bankRelationship = relationship,
            PTFSClaimAmount = claimAmount
        };

        return viewModel;
    }
}
