using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities
{
    public class GetResponsibleEntityAnswersHandler : IRequestHandler<GetResponsibleEntityAnswersRequest, GetResponsibleEntityAnswersResponse>
    {
        private readonly IDbConnectionWrapper _connection;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetResponsibleEntityAnswersHandler(IDbConnectionWrapper connection, IApplicationDataProvider applicationDataProvider)
        {
            _connection = connection;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<GetResponsibleEntityAnswersResponse> Handle(GetResponsibleEntityAnswersRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var answers = await _connection.QuerySingleOrDefaultAsync<GetResponsibleEntityAnswersResponse>("GetResponsibleEntityAnswers",
                new
                {
                    ApplicationId = applicationId
                });

            var evidenceFiles = await _connection.QueryAsync<EvidenceFile>("GetResponsibleEntityEvidenceFileNames", new
            {
                ApplicationId = applicationId
            });

            if (answers is not null)
            {
                answers.EvidenceFiles = evidenceFiles.ToList();
            }

            return answers ?? new GetResponsibleEntityAnswersResponse();
        }
    }

    public class GetResponsibleEntityAnswersRequest : IRequest<GetResponsibleEntityAnswersResponse>
    {
        private GetResponsibleEntityAnswersRequest() { }

        public static readonly GetResponsibleEntityAnswersRequest Request = new();
    }

    public class GetResponsibleEntityAnswersResponse
    {
        public Guid ApplicationId { get; set; }
        public Guid ResponsibleEntityId { get; set; }
        public int? RepresentationTypeId { get; set; }
        public int? ResponsibleEntityTypeId { get; set; }
        public int? BuildingRelationshipId { get; set; }

        public int? RepresentativeResponsibleEntityTypeId { get; set; }
        public bool? RepresentativeUKBased { get; set; }
        public string RepresentativeIndividualOrCompany { get; set; }
        public string RepresentativeCompanyDetails { get; set; }
        public string RepresentativeDetails { get; set; }
        public string RepresentativeAddress { get; set; }

        public int ResponsibleEntityRelationId { get; set; }
        public string ResponsibleEntityRelation { get; set; }
        public int? ResponsibleEntityCompanyTypeId { get; set; }
        public string ResponsibleEntityCompanyType { get; set; }
        public int? ResponsibleEntityCompanySubTypeId { get; set; }
        public bool? ResponsibleEntityRegisteredInUK { get; set; }
        public string ResponsibleEntityCompanyDetails { get; set; }
        public string ResponsibleEntityDetails { get; set; }
        public string ResponsibleEntityCompanyAddress { get; set; }

        public string ResponsibleEntityPrimaryContact { get; set; }
        public string ResponsibleEntityAuthorisationEvidence { get; set; }
        public int? ResponsibleEntitySharedOwners { get; set; }
        public bool? ResponsibleEntityClaimingGrant { get; set; }
        public bool? ConfirmedNotViable { get; set; }
        public List<EvidenceFile> EvidenceFiles { get; set; }

        public Guid? FreeholderId { get; set; }
        public int? FreeholderResponsibleEntityTypeId { get; set; }
        public string FreeholderIndividualOrCompany { get; set; }
        public string FreeholderCompanyDetails { get; set; }
        public string FreeholderDetails { get; set; }
        public string FreeholderAddress { get; set; }
    }

    public class EvidenceFile
    {
        public string Name { get; set; }
    }
}