using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BankAccount.Details.GetVerificationContact;

public class GetVerificationContactHandler : IRequestHandler<GetVerificationContactRequest, GetVerificationContactResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IBankDetailsRepository _bankDetailsRepository;

    public GetVerificationContactHandler(IApplicationDataProvider applicationDataProvider, IBankDetailsRepository bankDetailsRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _bankDetailsRepository = bankDetailsRepository;
    }

    public async Task<GetVerificationContactResponse> Handle(GetVerificationContactRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        var contactDetails = await _bankDetailsRepository.GetBankAccountVerificationContact(applicationId);

        return new GetVerificationContactResponse
        {
            ContactNumber = contactDetails.VerificationContactNumber,
            ContactName = contactDetails.VerificationContactName,
            BankDetailsRelationship = contactDetails.ResponsibleEntityRelationship
        };
    }
}