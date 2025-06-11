using System.Data;
using System.Transactions;
using Dapper;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingsInsurance.GetBuildingsInsurance;
using HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingsInsurance.SetBuildingsInsurance;
using HE.Remediation.Core.Extensions;

namespace HE.Remediation.Core.Data.Repositories;

public class BuildingsInsuranceRepository(IDbConnectionWrapper db) : IBuildingsInsuranceRepository
{
    private readonly IDbConnectionWrapper _db = db;

    public async Task<List<InsuranceProvider>> GetBuildingInsuranceProviders()
    {
        var insuranceProviders = await _db.QueryAsync<InsuranceProvider>("GetBuildingInsuranceProviders");
        return insuranceProviders?.ToList();
    }

    public async Task<GetBuildingsInsuranceResponse> GetBuildingsInsurance(Guid applicationId)
    {
        return await GetBuildingsInsurance<GetBuildingsInsuranceResponse>(applicationId, "GetBuildingInsurance");
    }

    public async Task<UseCase.Areas.ClosingReport.BuildingsInsurance.GetBuildingsInsurance.GetBuildingsInsuranceResponse> GetClosingReportBuildingsInsurance(Guid applicationId)
    {
        return await GetBuildingsInsurance<UseCase.Areas.ClosingReport.BuildingsInsurance.GetBuildingsInsurance.GetBuildingsInsuranceResponse>(applicationId, "GetClosingReportBuildingInsurance");
    }

    public async Task<SetBuildingsInsuranceResponse> SaveBuildingInsurance(SetBuildingsInsuranceRequest request)
    {
        using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            Guid? buildingInsuranceId = request.Id;

            DynamicParameters teamParameters = await UpsertBuildingInsurance(request, buildingInsuranceId);

            if ((buildingInsuranceId ?? Guid.Empty) == Guid.Empty)
            {
                {
                    buildingInsuranceId = teamParameters.Get<Guid?>("@BuildingInsuranceId");
                }

                await AssignInsuranceProviders(request, buildingInsuranceId);

                // commit
                transactionScope.Complete();

            }

            return new SetBuildingsInsuranceResponse();
        }
    }
    public async Task<UseCase.Areas.ClosingReport.BuildingsInsurance.SetBuildingsInsurance.SetBuildingsInsuranceResponse> SaveClosingReportBuildingInsurance(UseCase.Areas.ClosingReport.BuildingsInsurance.SetBuildingsInsurance.SetBuildingsInsuranceRequest request)
    {
        using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            Guid? buildingInsuranceId = request.Id;

            DynamicParameters teamParameters = await UpsertClosingReportBuildingInsurance(request, buildingInsuranceId);

            if ((buildingInsuranceId ?? Guid.Empty) == Guid.Empty)
            {
                {
                    buildingInsuranceId = teamParameters.Get<Guid?>("@ClosingReportInsuranceId");
                }

                await AssignClosingReportInsuranceProviders(request, buildingInsuranceId);

                // commit
                transactionScope.Complete();

            }

            return new UseCase.Areas.ClosingReport.BuildingsInsurance.SetBuildingsInsurance.SetBuildingsInsuranceResponse();
        }
    }

    private async Task<DynamicParameters> UpsertBuildingInsurance(SetBuildingsInsuranceRequest request, Guid? buildingInsuranceId)
    {
        var teamParameters = new DynamicParameters();
        teamParameters.Add("@ApplicationId", value: request.ApplicationId, dbType: DbType.Guid, direction: ParameterDirection.Input);
        teamParameters.Add("@SumInsuredAmount", value: request.SumInsuredAmount, dbType: DbType.Decimal, direction: ParameterDirection.Input);
        teamParameters.Add("@CurrentBuildingInsurancePremiumAmount", value: request.CurrentBuildingInsurancePremiumAmount, dbType: DbType.Decimal, direction: ParameterDirection.Input);
        teamParameters.Add("@IfOtherInsuranceProviderName", value: request.IfOtherInsuranceProviderName, dbType: DbType.String, direction: ParameterDirection.Input);
        teamParameters.Add("@AdditionalInfo", value: request.AdditionalInfo, dbType: DbType.String, direction: ParameterDirection.Input);
        teamParameters.Add("@BuildingInsuranceId", dbType: DbType.Guid, direction: ParameterDirection.Output);
        await _db.ExecuteAsync("InsertOrUpdateBuildingInsurance", teamParameters);
        return teamParameters;
    }
    private async Task<DynamicParameters> UpsertClosingReportBuildingInsurance(UseCase.Areas.ClosingReport.BuildingsInsurance.SetBuildingsInsurance.SetBuildingsInsuranceRequest request, Guid? buildingInsuranceId)
    {
        var teamParameters = new DynamicParameters();
        teamParameters.Add("@ApplicationId", value: request.ApplicationId, dbType: DbType.Guid, direction: ParameterDirection.Input);
        teamParameters.Add("@SumInsuredAmount", value: request.SumInsuredAmount, dbType: DbType.Decimal, direction: ParameterDirection.Input);
        teamParameters.Add("@CurrentBuildingInsurancePremiumAmount", value: request.CurrentBuildingInsurancePremiumAmount, dbType: DbType.Decimal, direction: ParameterDirection.Input);
        teamParameters.Add("@IfOtherInsuranceProviderName", value: request.IfOtherInsuranceProviderName, dbType: DbType.String, direction: ParameterDirection.Input);
        teamParameters.Add("@AdditionalInfo", value: request.AdditionalInfo, dbType: DbType.String, direction: ParameterDirection.Input);
        teamParameters.Add("@ClosingReportInsuranceId", dbType: DbType.Guid, direction: ParameterDirection.Output);
        await _db.ExecuteAsync("InsertOrUpdateClosingReportBuildingInsurance", teamParameters);
        return teamParameters;
    }

    private async Task AssignInsuranceProviders(SetBuildingsInsuranceRequest request, Guid? buildingInsuranceId)
    {
            var insuranceProviderParameters = BuildAddUpdateOrDeleteInsuranceProvidersParameters(request.SelectedInsuranceProviderIds, buildingInsuranceId);
            await _db.ExecuteAsync("InsertUpdateOrDeleteBuildingInsuranceProviders", insuranceProviderParameters);
    }

    private static DynamicParameters BuildAddUpdateOrDeleteInsuranceProvidersParameters(List<int> insuranceProviders, Guid? buildingInsuranceId)
    {
        var addPermissionParameters = new DynamicParameters();
        addPermissionParameters.Add("@NewInsuranceProviders", insuranceProviders
            .Select(insuranceProviderId => new { Id = Guid.NewGuid(), ApplicationBuildingDetailsInsuranceId = buildingInsuranceId.Value, InsuranceProviderId = insuranceProviderId })
            .ToDataTable()
            .AsTableValuedParameter("[dbo].[ApplicationBuildingDetailsInsuranceInsuranceProvidersType]"));
        return addPermissionParameters;
    }
    private async Task AssignClosingReportInsuranceProviders(UseCase.Areas.ClosingReport.BuildingsInsurance.SetBuildingsInsurance.SetBuildingsInsuranceRequest request, Guid? buildingInsuranceId)
    {
            var insuranceProviderParameters = BuildAddUpdateOrDeleteClosingReportInsuranceProvidersParameters(request.SelectedInsuranceProviderIds, buildingInsuranceId);
            await _db.ExecuteAsync("InsertUpdateOrDeleteClosingReportBuildingInsuranceProviders", insuranceProviderParameters);
    }

    private static DynamicParameters BuildAddUpdateOrDeleteClosingReportInsuranceProvidersParameters(List<int> insuranceProviders, Guid? closingReportInsuranceId)
    {
        var addPermissionParameters = new DynamicParameters();
        addPermissionParameters.Add("@NewInsuranceProviders", insuranceProviders
            .Select(insuranceProviderId => new { Id = Guid.NewGuid(), ClosingReportInsuranceId = closingReportInsuranceId.Value, InsuranceProviderId = insuranceProviderId })
            .ToDataTable()
            .AsTableValuedParameter("[dbo].[ClosingReportInsuranceInsuranceProvidersType]"));
        return addPermissionParameters;
    }

    private async Task<T> GetBuildingsInsurance<T>(Guid applicationId, string storedProcedureName)
    {
        var response = await _db.QuerySingleOrDefaultAsync<T>(storedProcedureName, new
        {
            ApplicationId = applicationId
        });

        response ??= Activator.CreateInstance<T>();
        return response;
    }
}
