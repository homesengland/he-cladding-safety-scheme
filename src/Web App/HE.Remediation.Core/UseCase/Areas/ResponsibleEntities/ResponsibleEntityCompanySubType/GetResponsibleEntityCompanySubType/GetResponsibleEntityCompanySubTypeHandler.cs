using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.ResponsibleEntityCompanySubType.GetResponsibleEntityCompanySubType;

public class GetResponsibleEntityCompanySubTypeHandler : IRequestHandler<GetResponsibleEntityCompanySubTypeRequest, GetResponsibleEntityCompanySubTypeResponse>
{
    private readonly IResponsibleEntityRepository _responsibleEntityRepository;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public GetResponsibleEntityCompanySubTypeHandler(IResponsibleEntityRepository responsibleEntityRepository, IApplicationDataProvider applicationDataProvider)
    {
        _responsibleEntityRepository = responsibleEntityRepository;
        _applicationDataProvider = applicationDataProvider;
    }

    public async Task<GetResponsibleEntityCompanySubTypeResponse> Handle(GetResponsibleEntityCompanySubTypeRequest request, CancellationToken cancellationToken)
    {
        var result = await _responsibleEntityRepository.GetResponsibleEntityCompanyType(
                _applicationDataProvider.GetApplicationId());

        return new GetResponsibleEntityCompanySubTypeResponse()
        {
            OrganisationSubType = result?.OrganisationSubType,
            OrganisationSubTypeDescription = result?.OrganisationSubTypeDescription        
        };
    }
}
