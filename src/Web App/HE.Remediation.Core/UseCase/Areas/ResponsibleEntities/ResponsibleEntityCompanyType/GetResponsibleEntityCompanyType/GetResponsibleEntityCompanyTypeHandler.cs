
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityCompanyType.GetResponsibleEntityCompanyType;

public class GetResponsibleEntityCompanyTypeHandler : IRequestHandler<GetResponsibleEntityCompanyTypeRequest, GetResponsibleEntityCompanyTypeResponse>
{
    private readonly IResponsibleEntityRepository _responsibleEntityRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public GetResponsibleEntityCompanyTypeHandler(IResponsibleEntityRepository responsibleEntityRepository, IApplicationDataProvider applicationDataProvider)
    {
        _responsibleEntityRepository = responsibleEntityRepository;
        _applicationDataProvider = applicationDataProvider;
    }


    public async ValueTask<GetResponsibleEntityCompanyTypeResponse> Handle(GetResponsibleEntityCompanyTypeRequest request, CancellationToken cancellationToken)
    {
        var result = await _responsibleEntityRepository.GetResponsibleEntityCompanyType(
                _applicationDataProvider.GetApplicationId());

        return new GetResponsibleEntityCompanyTypeResponse
        {
            OrganisationType = result?.OrganisationType,
            ResponsibleEntityRelationType = result?.ResponsibleEntityRelationType
        };
    }
}