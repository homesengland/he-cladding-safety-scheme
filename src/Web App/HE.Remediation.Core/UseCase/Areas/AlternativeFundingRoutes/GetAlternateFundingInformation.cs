using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes;

public class GetAlternateFundingInformationHandler : IRequestHandler<GetAlternateFundingInformationRequest, GetAlternateFundingInformationResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;

    public GetAlternateFundingInformationHandler(IApplicationDataProvider applicationDataProvider, IApplicationRepository applicationRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
    }

    public async Task<GetAlternateFundingInformationResponse> Handle(GetAlternateFundingInformationRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        var isSocialSector = await _applicationRepository.IsSocialSector(applicationId);

        return new GetAlternateFundingInformationResponse
        {
            IsSocialSector = isSocialSector
        };
    }
}

public class GetAlternateFundingInformationRequest : IRequest<GetAlternateFundingInformationResponse>
{
    private GetAlternateFundingInformationRequest()
    {
    }

    public static readonly GetAlternateFundingInformationRequest Request = new();
}

public class GetAlternateFundingInformationResponse
{
    public bool IsSocialSector { get; set; }
}