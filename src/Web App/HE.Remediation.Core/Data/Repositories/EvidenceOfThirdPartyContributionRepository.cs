using Dapper;
using HE.Remediation.Core.Data.StoredProcedureResults.ClosingReport.EvidenceOfThirdPartyContribution;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Extensions;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.DeleteEvidence;
using HE.Remediation.Core.UseCase.Areas.ClosingReport.EvidenceOfThirdPartyContribution.EvidenceDetails;
using System.Data;

namespace HE.Remediation.Core.Data.Repositories
{
    public class EvidenceOfThirdPartyContributionRepository(IDbConnectionWrapper db) : IEvidenceOfThirdPartyContributionRepository
    {
        private readonly IDbConnectionWrapper _db = db;
        public async Task<List<GetEvidenceDetailsResult>> GetEvidenceDetails(Guid applicationID)
        {
            var evidenceDetails = await _db.QueryAsync<GetEvidenceDetailsResult>(
                "GetClosingPaymentThirdPartyEvidence",
                new { applicationID }
            );
            return evidenceDetails?.ToList();
        }

        public async Task<SetEvidenceDetailResponse> UpsertClosingReportEvidenceDetail(SetEvidenceDetailRequest request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("EvidenceId", request.Id);
            parameters.Add("ApplicationId", request.ApplicationId);
            parameters.Add("ThirdPartyName", request.ThirdPartyName);
            parameters.Add("EThirdPartyContributionStatusOfAttempt", request.StatusOfAttempt);
            parameters.Add("DateOfAttempt", request.DateOfAttempt);
            parameters.Add("Amount", request.Amount);
            parameters.Add("AttemptDetails", request.AttemptDetails);
            parameters.Add("TypeOfContribution", request.@TypeOfContribution.Select(x => (int)x).ToDataTable().AsTableValuedParameter("[dbo].[IntListType]"), DbType.Object);
            parameters.Add("InsertedId", dbType: DbType.Guid, direction: ParameterDirection.Output);

            await _db.ExecuteAsync(nameof(UpsertClosingReportEvidenceDetail), parameters);

            var insertedId = parameters.Get<Guid>("InsertedId");

            if (insertedId == Guid.Empty)
            {
                throw new Exception("Failed to insert evidence details.");
            }

            var result = new SetEvidenceDetailResponse
            {
                ApplicationId = request.ApplicationId,
                EvidenceDetailsResults = new GetEvidenceDetailsResult()
                    {
                        Id = insertedId
                    }
            };

            return result;
        }

        public async Task<bool> DeleteEvidenceDetails(DeleteEvidenceRequest request)
        {
            var result = await _db.QuerySingleOrDefaultAsync<bool>(
                "DeleteEvidenceDetails",
                new
                {
                    request.EvidenceId,
                    request.ApplicationId
                }
            );
            if (result == false)
            {
                throw new Exception("Failed to delete evidence details.");
            }
            return true;
        }

        public async Task InsertThirdPartyEvidenceFile(Guid applicationId, Guid fileId, Guid evidenceId)
        {
            await _db.ExecuteAsync("InsertClosingReportFile", new
            {
                ApplicationId = applicationId,
                FileId = fileId,
                UploadType = EClosingReportFileType.EvidenceOfThirdPartyContribution
            });

            await _db.ExecuteAsync("UpdateClosingReportThirdPartyEvidenceFile", new
            {
                FileId = fileId,
                EvidenceId = evidenceId
            });
        }

        public async Task DeleteThirdPartyEvidenceFile(Guid applicationId, Guid fileId, Guid evidenceId)
        {
            await _db.ExecuteAsync("UpdateClosingReportThirdPartyEvidenceFile", new
            {
                FileId = (Guid?)null,
                EvidenceId = evidenceId
            });

            await _db.QuerySingleOrDefaultAsync<int>("DeleteClosingReportFile", new
            {
                FileId = fileId
            });
        }

        public async Task ImportClosingReportEvidenceDetail(Guid applicationId)
        {
            await _db.ExecuteAsync(nameof(ImportClosingReportEvidenceDetail), new { ApplicationId = applicationId });
        }

        public async Task UpdateClosingReportThirdPartyEvidenceAsSubmitted(Guid applicationId, Guid evidenceId)
        {
            await _db.ExecuteAsync(nameof(UpdateClosingReportThirdPartyEvidenceAsSubmitted), new { ApplicationId = applicationId, EvidenceId = evidenceId });
        }
    }
}
