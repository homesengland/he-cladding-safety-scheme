using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities
{
    public class GetResponsibleEntityAnswersHandler : IRequestHandler<GetResponsibleEntityAnswersRequest, GetResponsibleEntityAnswersResponse>
    {
        private readonly IDbConnectionWrapper _connection;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IRightToManageRepository _rightToManageRepository;

        public GetResponsibleEntityAnswersHandler(
            IDbConnectionWrapper connection, 
            IApplicationDataProvider applicationDataProvider, 
            IApplicationRepository applicationRepository, 
            IRightToManageRepository rightToManageRepository)
        {
            _connection = connection;
            _applicationDataProvider = applicationDataProvider;
            _applicationRepository = applicationRepository;
            _rightToManageRepository = rightToManageRepository;
        }

        public async Task<GetResponsibleEntityAnswersResponse> Handle(GetResponsibleEntityAnswersRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var applicationStatus = await _applicationRepository.GetApplicationStatus(applicationId);

            var answers = await _connection.QuerySingleOrDefaultAsync<GetResponsibleEntityAnswersResponse>("GetResponsibleEntityAnswers",
                new
                {
                    ApplicationId = applicationId
                });

            var evidenceFiles = await _connection.QueryAsync<EvidenceFile>("GetResponsibleEntityEvidenceFileNames", new
            {
                ApplicationId = applicationId
            });

            var rightToManageFiles = await _rightToManageRepository.GetRightToManageEvidence(applicationId);

            if (answers is not null)
            {
                answers.RepresentEvidenceFiles = evidenceFiles.Where(x=> x.UploadType == EResponsibleEntityUploadType.Represent).ToList();
                answers.S151EvidenceFiles = evidenceFiles.Where(x => x.UploadType == EResponsibleEntityUploadType.S151).ToList();
                answers.CheifExecEvidenceFiles = evidenceFiles.Where(x => x.UploadType == EResponsibleEntityUploadType.ChiefExec).ToList();
                answers.ReadOnly = applicationStatus.DeclarationConfirmed;
                answers.GrantFundingSignatories = (await _connection.QueryAsync<GrantFundingSignatory>("GetResponsibleEntitiesGrantFundingSignatories", new
                {
                    ApplicationId = applicationId
                })).ToList();
                answers.RightToManageEvidenceFiles = rightToManageFiles.Select(x => x.Name).ToList();

                var applicationScheme = _applicationDataProvider.GetApplicationScheme();
                var isSelfFunded = applicationScheme != EApplicationScheme.CladdingSafetyScheme;
                var isSocialSector = applicationScheme == EApplicationScheme.SocialSector;

                answers.IsSocialSector = isSocialSector;
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
        public string ResponsibleEntityOrganisationDetails { get; set; }
        public string ResponsibleEntityDetails { get; set; }
        public string ResponsibleEntityCompanyAddress { get; set; }

        public string ResponsibleEntityPrimaryContact { get; set; }
        public string ResponsibleEntityAuthorisationEvidence { get; set; }

        public bool? ResponsibleEntityHasOwners { get; set; }
        public int? ResponsibleEntitySharedOwners { get; set; }
        public bool? ResponsibleEntityClaimingGrant { get; set; }
        public bool? ResponsibleEntityResponsibleForGrantFunding { get; set; }
        public bool? ConfirmedNotViable { get; set; }
        public List<EvidenceFile> RepresentEvidenceFiles { get; set; }
        public List<EvidenceFile> S151EvidenceFiles { get; set; }
        public List<EvidenceFile> CheifExecEvidenceFiles { get; set; }
        public List<GrantFundingSignatory> GrantFundingSignatories { get; set; }

        public bool? HasAcquiredRightToManage { get; set; }
        public DateTime? RightToManageAcquisitionDate { get; set; }
        public List<string> RightToManageEvidenceFiles { get; set; }

        public Guid? FreeholderId { get; set; }
        public int? FreeholderResponsibleEntityTypeId { get; set; }
        public string FreeholderIndividualOrCompany { get; set; }
        public string FreeholderCompanyDetails { get; set; }
        public string FreeholderDetails { get; set; }
        public string FreeholderAddress { get; set; }
        public bool ReadOnly { get; set; }
        public bool IsSocialSector { get; set; }
    }

    public class EvidenceFile
    {
        public string Name { get; set; }
        public EResponsibleEntityUploadType UploadType { get; set; }
    }

    public class GrantFundingSignatory
    {
        public string FullName { get; set; }

        public string EmailAddress { get; set; }

        public string Role { get; set; }
    }
}