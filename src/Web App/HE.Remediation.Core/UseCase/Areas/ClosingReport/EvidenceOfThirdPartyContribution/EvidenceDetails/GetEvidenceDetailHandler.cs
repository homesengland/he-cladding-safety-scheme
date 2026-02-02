using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;
using Microsoft.Extensions.Logging;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.EvidenceDetails
{
    public partial class GetEvidenceDetailHandler : IRequestHandler<GetEvidenceDetailRequest, GetEvidenceDetailResponse>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IEvidenceOfThirdPartyContributionRepository _evidenceOfThirdPartyContributionRepository;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IBuildingDetailsRepository _buildingDetailsRepository;

        public GetEvidenceDetailHandler(
            IApplicationDataProvider applicationDataProvider,
            IEvidenceOfThirdPartyContributionRepository evidenceOfThirdPartyContributionRepository,
            IApplicationRepository applicationRepository,
            IBuildingDetailsRepository buildingDetailsRepository,
            ILogger<GetEvidenceDetailsHandler> logger)
        {
            _applicationDataProvider = applicationDataProvider;
            _evidenceOfThirdPartyContributionRepository = evidenceOfThirdPartyContributionRepository;
            _applicationRepository = applicationRepository;
            _buildingDetailsRepository = buildingDetailsRepository;
        }

        public async ValueTask<GetEvidenceDetailResponse> Handle(GetEvidenceDetailRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var evidenceDetails = await _evidenceOfThirdPartyContributionRepository.GetEvidenceDetails(applicationId);

            var evidenceDetail = evidenceDetails.FirstOrDefault(detail => detail.Id == request.EvidenceId);

            var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
            var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

            if (evidenceDetail == null)
            {
                return new GetEvidenceDetailResponse { ApplicationReferenceNumber = applicationReferenceNumber , BuildingName = buildingName };
            }

            return new GetEvidenceDetailResponse()
            {
                ApplicationId = evidenceDetail.ApplicationId,
                Id = evidenceDetail.Id,
                ThirdPartyName = evidenceDetail.ThirdPartyName,
                DateOfAttempt = evidenceDetail.DateOfAttempt,
                StatusOfAttempt = evidenceDetail.StatusOfAttempt,
                AttemptDetails = evidenceDetail.AttemptDetails,
                TypeOfContribution = ConvertToEnumCollection(evidenceDetail.TypeOfContribution),
                Amount = evidenceDetail.Amount,
                IsSubmitted = evidenceDetail.IsSubmitted,
                IsEditable = evidenceDetail.IsEditable,
                FileId = evidenceDetail.FileId,
                Name = evidenceDetail.Name,
                Extension = evidenceDetail.Extension,
                MimeType = evidenceDetail.MimeType,
                Size = evidenceDetail.Size,
                ApplicationReferenceNumber = applicationReferenceNumber,
                BuildingName = buildingName
            };
        }

        private EFundingStillPursuing[] ConvertToEnumCollection(string csv)
        {
            if (string.IsNullOrEmpty(csv))
            {
                return [];
            }
            return [.. csv
                    .Split(",")
                    .Select(t => { int.TryParse(t, out int result); return result; })
                    .Select(c => (EFundingStillPursuing)c)];
        }
    }

    public class GetEvidenceDetailRequest : IRequest<GetEvidenceDetailResponse>
    {
        public Guid ApplicationId { get; set; }
        public Guid? EvidenceId { get; set; }
        public static GetEvidenceDetailRequest Request => new GetEvidenceDetailRequest();
    }

    public class GetEvidenceDetailResponse
    {
        public Guid ApplicationId { get; set; }
        public Guid? Id { get; set; }
        public string ThirdPartyName { get; set; }
        public DateTime? DateOfAttempt { get; set; }
        public EThirdPartyContributionStatusOfAttempt? StatusOfAttempt { get; set; }
        public string AttemptDetails { get; set; }
        public EFundingStillPursuing[] TypeOfContribution { get; set; }
        public decimal Amount { get; set; }

        public Guid? FileId { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string MimeType { get; set; }
        public int? Size { get; set; }

        public bool IsSubmitted { get; set; }
        public bool IsEditable { get; set; }
        public string ReturnUrl { get; set; }

        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }
    }

}
