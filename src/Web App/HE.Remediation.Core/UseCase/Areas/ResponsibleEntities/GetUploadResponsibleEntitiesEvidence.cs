using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;

public class GetUploadResponsibleEntitiesEvidenceHandler : IRequestHandler<GetUploadResponsibleEntitiesEvidenceRequest, GetUploadResponsibleEntitiesEvidenceResponse>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public GetUploadResponsibleEntitiesEvidenceHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }

    public async ValueTask<GetUploadResponsibleEntitiesEvidenceResponse> Handle(GetUploadResponsibleEntitiesEvidenceRequest request, CancellationToken cancellationToken)
    {
        var result = default(GetUploadResponsibleEntitiesEvidenceResponse);
        await _connection.QueryAsync<GetUploadResponsibleEntitiesEvidenceResponse, FileResult, GetUploadResponsibleEntitiesEvidenceResponse>("GetResponsibleEntityEvidence",
            (response, file) =>
            {
                result ??= response;
                if (file is not null)
                {
                    result.AddedFiles.Add(file);
                }

                return result;
            }, new
            {
                ApplicationId = _applicationDataProvider.GetApplicationId(),
                UploadType = request.UploadType
            });

        return result;
    }
}

public class GetUploadResponsibleEntitiesEvidenceResponse
{
    public EApplicationResponsibleEntityOrganisationType? OrganisationType { get; set; }
    public IList<FileResult> AddedFiles { get; set; }

    public GetUploadResponsibleEntitiesEvidenceResponse()
    {
        AddedFiles = new List<FileResult>();
    }
}

public class GetUploadResponsibleEntitiesEvidenceRequest : IRequest<GetUploadResponsibleEntitiesEvidenceResponse>
{
    public EResponsibleEntityUploadType UploadType { get; set; }
}