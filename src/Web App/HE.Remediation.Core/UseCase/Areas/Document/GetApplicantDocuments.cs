using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Document;

public class GetApplicantDocumentsHandler : IRequestHandler<GetApplicantDocumentsRequest, GetApplicantDocumentsResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IDocumentRepository _documentRepository;

    public GetApplicantDocumentsHandler(IApplicationDataProvider applicationDataProvider, IApplicationRepository applicationRepository, IBuildingDetailsRepository buildingDetailsRepository, IDocumentRepository documentRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _documentRepository = documentRepository;
    }

    public async Task<GetApplicantDocumentsResponse> Handle(GetApplicantDocumentsRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();
        var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var documents = await _documentRepository.GetApplicantDocuments(new GetApplicantDocumentsParameters
        {
            ApplicationId = applicationId,
            SearchTerm = request.SearchTerm
        });

        return new GetApplicantDocumentsResponse
        {
            ApplicationReference = reference,
            BuildingName = buildingName,
            SearchTerm = request.SearchTerm,
            Files = documents.Select(d => new GetApplicantDocumentsResponse.DocumentFile
            {
                Id = d.Id,
                Filename = d.Name,
                UploadDate = d.UploadDate,
                Category = d.Category,
                Type = d.Type
            }).ToList()
        };
    }
}

public class GetApplicantDocumentsRequest : IRequest<GetApplicantDocumentsResponse>
{
    public string SearchTerm { get; set; }
}

public class GetApplicantDocumentsResponse
{
    public string ApplicationReference { get; set; }
    public string BuildingName { get; set; }
    public string SearchTerm { get; set; }
    public IReadOnlyCollection<DocumentFile> Files { get; set; }

    public class DocumentFile
    {
        public Guid Id { get; set; }
        public string Filename { get; set; }
        public DateTime UploadDate { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
    }
}