
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PreTenderSupport.CheckYourAnswers;

public class GetCheckYourAnswersHandler : IRequestHandler<GetCheckYourAnswersRequest, GetCheckYourAnswersResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IDbConnectionWrapper _db;
    private readonly IPreTenderRepository _preTenderRepo;
    private readonly IApplicationRepository _applicationRepository;

    public GetCheckYourAnswersHandler(IApplicationDataProvider applicationDataProvider, IDbConnectionWrapper db,
                                      IPreTenderRepository preTenderRepo, IApplicationRepository applicationRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _db = db;
        _preTenderRepo = preTenderRepo;
        _applicationRepository = applicationRepository;
    }

    public async Task<GetCheckYourAnswersResponse> Handle(GetCheckYourAnswersRequest request, CancellationToken cancellationToken)
    {
        return await GetCheckYourAnswers();
    }

    private async Task<GetCheckYourAnswersResponse> GetCheckYourAnswers()
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var answers = await _db.QuerySingleOrDefaultAsync<GetCheckYourAnswersResponse>("GetPreTenderCheckYourAnswers", new
        {
            ApplicationId = _applicationDataProvider.GetApplicationId()
        });

        var signatures = await _preTenderRepo.GetGrantFundingSignatories(applicationId);
        if (signatures != null)
        {
            answers.Signatures = signatures;
        }

        var bankDetailsRelationship = await _applicationRepository.GetBankDetailsRelationship(applicationId);

        answers.BankDetailsRelationship = bankDetailsRelationship;

        return answers;
    }
}
