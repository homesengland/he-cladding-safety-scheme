using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BankAccount.Details.SetVerificationContact;

public class SetVerificationContactHandler : IRequestHandler<SetVerificationContactRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IBankDetailsRepository _bankDetailsRepository;

    public SetVerificationContactHandler(IApplicationDataProvider applicationDataProvider, IBankDetailsRepository bankDetailsRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _bankDetailsRepository = bankDetailsRepository;
    }

    public async Task<Unit> Handle(SetVerificationContactRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await _bankDetailsRepository.UpdateBankAccountVerificationContact(
            new UpdateBankAccountVerificationContactParameters
            {
                ApplicationId = _applicationDataProvider.GetApplicationId(),
                ContactNumber = request.ContactNumber,
                ContactName = request.ContactName
            });

        return Unit.Value;
    }
}