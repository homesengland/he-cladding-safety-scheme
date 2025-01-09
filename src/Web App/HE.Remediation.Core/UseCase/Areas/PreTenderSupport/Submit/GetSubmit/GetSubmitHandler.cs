using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PreTenderSupport.Submit.GetSubmit;

public class GetSubmitHandler : IRequestHandler<GetSubmitRequest, GetSubmitResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IPreTenderRepository _preTenderRepository;

    public GetSubmitHandler(
        IApplicationDataProvider applicationDataProvider, 
        IApplicationRepository applicationRepository, 
        IPreTenderRepository preTenderRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _preTenderRepository = preTenderRepository;
    }


    public async Task<GetSubmitResponse> Handle(GetSubmitRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        await _preTenderRepository.SubmitPreTender(applicationId);

        var referenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);

        return new GetSubmitResponse
        {
            ReferenceNumber = referenceNumber
        };
    }
}
