using Dapper;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Extensions;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.Core.Data.Repositories;

public class AlternateFundingRepository : IAlternateFundingRepository
{
    private readonly IDbConnectionWrapper _connection;

    public AlternateFundingRepository(IDbConnectionWrapper connection)
    {
        _connection = connection;
    }

    public async Task<ECostRecoveryType?> GetCostRecoveryType(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("ApplicationId", applicationId);

        var result = await _connection.QuerySingleOrDefaultAsync<ECostRecoveryType?>(nameof(GetCostRecoveryType), parameters);
        return result;
    }

    public async Task SetCostRecoveryType(SetCostRecoveryTypeParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(SetCostRecoveryType), parameters);
    }

    public async Task<IReadOnlyCollection<EPartyPursuedRole>> GetPartyPursuedRoles(Guid applciationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applciationId);

        var result = await _connection.QueryAsync<EPartyPursuedRole>(nameof(GetPartyPursuedRoles), parameters);
        return result;
    }

    public async Task SetPartyPursedRoles(SetPartyPursedRolesParameters parameters)
    {
        var @params = new DynamicParameters();
        @params.Add("@ApplicationId", parameters.ApplicationId);
        @params.Add("@PartyPursuedRoles", parameters.PartyPursuedRoles.ToDataTable().AsTableValuedParameter("[dbo].[IntListType]"));

        await _connection.ExecuteAsync(nameof(SetPartyPursedRoles), @params);
    }

    public async Task<string> GetOtherPartyPursuedRole(Guid applciationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applciationId);

        var result = await _connection.QuerySingleOrDefaultAsync<string>(nameof(GetOtherPartyPursuedRole), parameters);
        return result;
    }

    public async Task SetOtherPartyPursuedRole(SetOtherPartyPursuedRoleParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(SetOtherPartyPursuedRole), parameters);
    }

    public async Task<GetFundingRoutesCheckYourAnswersResult> GetFundingRoutesCheckYourAnswers(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        GetFundingRoutesCheckYourAnswersResult result = null;

        await _connection.QueryAsync<GetFundingRoutesCheckYourAnswersResult,
            GetFundingRoutesCheckYourAnswersResult.FundingRouteType,
            GetFundingRoutesCheckYourAnswersResult.PartyPursuedRole,
            GetFundingRoutesCheckYourAnswersResult>(nameof(GetFundingRoutesCheckYourAnswers),
            (answers, funding, role) =>
            {
                result ??= answers;

                if (funding is not null && result.FundingRouteTypes.All(x => x.Id != funding.Id))
                {
                    result.FundingRouteTypes.Add(funding);
                }

                if (role is not null && result.PartyPursuedRoles.All(x => x.Id != role.Id))
                {
                    result.PartyPursuedRoles.Add(role);
                }

                return result;
            },
            parameters);

        return result;
    }

    public async Task<bool> GetAlternateFundingVisitedCheckYourAnswers(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        var result = await _connection.QuerySingleOrDefaultAsync<bool>(nameof(GetAlternateFundingVisitedCheckYourAnswers), parameters);
        return result;
    }

    public async Task SetAlternateFundingVisitedCheckYourAnswers(SetAlternateFundingVisitedCheckYourAnswersParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(SetAlternateFundingVisitedCheckYourAnswers), parameters);
    }

    public async Task<EPursuedSourcesFundingType?> GetPursuedSourcesFunding(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        var result = await _connection.QuerySingleOrDefaultAsync<EPursuedSourcesFundingType?>(nameof(GetPursuedSourcesFunding), parameters);
        return result;
    }
}