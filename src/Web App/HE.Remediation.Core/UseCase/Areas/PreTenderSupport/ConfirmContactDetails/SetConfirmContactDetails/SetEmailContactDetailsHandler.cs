using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PreTenderSupport.ConfirmContactDetails.SetConfirmContactDetails;

public class SetEmailContactDetailsHandler : IRequestHandler<SetEmailContactDetailsRequest, Unit>
{
    private readonly IDbConnectionWrapper _dbConnectionWrapper;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IPreTenderRepository _preTenderRepository;

    public SetEmailContactDetailsHandler(
        IDbConnectionWrapper dbConnectionWrapper, 
        IApplicationDataProvider applicationDataProvider, 
        IPreTenderRepository preTenderRepository)
    {
        _dbConnectionWrapper = dbConnectionWrapper;
        _applicationDataProvider = applicationDataProvider;
        _preTenderRepository = preTenderRepository;
    }

    public async Task<Unit> Handle(SetEmailContactDetailsRequest request, CancellationToken cancellationToken)
    {
        if (await _preTenderRepository.IsPreTenderSubmitted(_applicationDataProvider.GetApplicationId()))
        {
            return Unit.Value;
        }
        await UpdateEmailContactDetials(request);
        return Unit.Value;
    }

    private async Task UpdateEmailContactDetials(SetEmailContactDetailsRequest request)
    {
        await _dbConnectionWrapper.ExecuteAsync("UpdateGrantFundingSignatureEmail", new 
        { 
            Id = request.Id, 
            @EmailAddress = request.EmailAddress
        });
    }
}
