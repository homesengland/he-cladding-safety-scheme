using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Data.StoredProcedureResults.FireRiskAppraisal;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using System.Transactions;

namespace HE.Remediation.Core.Data.Repositories
{
    public class FireRiskWorksRepository : IFireRiskWorksRepository
    {
        private readonly IDbConnectionWrapper _dbConnection;
        public FireRiskWorksRepository(IDbConnectionWrapper dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Task<FireRiskWorksRequiredResult> GetWorksRequired(Guid applicationId)
        {
            return _dbConnection.QuerySingleOrDefaultAsync<FireRiskWorksRequiredResult>("GetFireRiskWorksRequiredForApplication", new { applicationId });
        }

        public Task SetExternalWorksRequired(Guid applicationId, ENoYes required)
        {
            return _dbConnection.ExecuteAsync("SetExternalFireRiskWorksRequired", new { ApplicationId = applicationId, ExternalWorksRequired = required });
        }

        public Task SetInternalWorksRequired(Guid applicationId, ENoYes required)
        {
            return _dbConnection.ExecuteAsync("SetInternalFireRiskWorksRequired", new { ApplicationId = applicationId, InternalWorksRequired = required });
        }        

        public async Task<List<GetWallWorksListResult>> GetFireRiskWallWorks(Guid applicationId, EWorkType workType)
        {
            var results = await _dbConnection.QueryAsync<GetWallWorksListResult>("GetFireRiskWallWorks", new
            {
                ApplicationId = applicationId,
                WorkTypeId = workType
            });

            return results.ToList();
        }
        
        public async Task<List<CladdingSystemsListResult>> GetFireRiskCladdingWorks(Guid applicationId)
        {
            var results = await _dbConnection.QueryAsync<CladdingSystemsListResult>("GetWorksToCladdingSystems", new
            {
                ApplicationId = applicationId
            });

            return results.ToList();
        }


        public async Task<GetWallWorksListResult> GetFireRiskWallWorksDetail(Guid Id)
        {
            return await _dbConnection.QuerySingleOrDefaultAsync<GetWallWorksListResult>("GetFireRiskWallWorkDetails", new 
                                                                                        { 
                                                                                            Id
                                                                                        });
        }
                
        public async Task DeleteFireRiskWallWorks(Guid Id)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _dbConnection.ExecuteAsync("DeleteFireRiskWallWorks", new
                {                 
                    @Id=Id 
                });            

                scope.Complete();
            }
        }

        public async Task InsertWallWorks(EWorkType WorkTypeId, string Description, Guid ApplicationId)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _dbConnection.ExecuteAsync("InsertOrUpdateFireRiskWallWorks", new
                {                 
                    @ApplicationId = ApplicationId,
                    @Description = Description,
                    @WorkTypeId = WorkTypeId
                });            

                scope.Complete();
            }
        }

        public async Task UpdateWallWorks(EWorkType WorkTypeId, string Description, Guid ApplicationId, Guid Id)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _dbConnection.ExecuteAsync("InsertOrUpdateFireRiskWallWorks", new
                {                 
                    @ApplicationId = ApplicationId,
                    @Id = Id,
                    @Description = Description,
                    @WorkTypeId = WorkTypeId
                });            

                scope.Complete();
            }
        }

        public async Task ResetFireRiskWallWorks(Guid applicationId, EWorkType workType)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _dbConnection.ExecuteAsync(nameof(ResetFireRiskWallWorks), new
                {                 
                    ApplicationId = applicationId,
                    WorkTypeId = workType
                });            

                scope.Complete();
            }
        }
    }
}
