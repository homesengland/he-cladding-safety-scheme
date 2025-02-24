using Dapper;
using HE.Remediation.Core.Data.StoredProcedureParameters.VariationRequest;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Data.StoredProcedureResults.VariationRequest;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using System.Data;
using System.Transactions;
using HE.Remediation.Core.Extensions;

namespace HE.Remediation.Core.Data.Repositories;

public class VariationRequestRepository : IVariationRequestRepository
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public VariationRequestRepository(IDbConnectionWrapper connection,
        IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }

    public async Task<bool> HasInProgressVariationRequest()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return false;
        }

        return await _connection.QuerySingleOrDefaultAsync<bool>("HasInProgressVariationRequest", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task<bool> IsVariationRequestSubmitted()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return false;
        }

        return await _connection.QuerySingleOrDefaultAsync<bool>("IsVariationRequestSubmitted", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task<EVariationRequestApprovalStatus?> GetApprovalStatus()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<EVariationRequestApprovalStatus?>("GetVariationRequestApprovalStatus", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task<int?> GetVariationNumber()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<int?>(
            "GetVariationRequestNumber",
            new
            {
                ApplicationId = applicationId
            });
    }

    public async Task<GetVariationReasonResult> GetVariationReason()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<GetVariationReasonResult>(
            "GetVariationReason",
            new
            {
                ApplicationId = applicationId
            });
    }

    public async Task UpsertVariationSelection(UpsertVariationSelectionParameters parameters)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpsertVariationSelection", new
        {
            ApplicationId = applicationId,
            parameters.IsScopeVariation,
            parameters.IsCostVariation,
            parameters.IsTimescaleVariation,
            parameters.IsThirdPartyContributionVariation
        });

        scope.Complete();
    }

    public async Task<GetAdjustEndDateResult> GetVariationAdjustEndDate()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<GetAdjustEndDateResult>(
            "GetVariationAdjustEndDate",
            new
            {
                ApplicationId = applicationId
            });
    }

    public async Task UpdateVariationAdjustEndDate(UpdateVariationAdjustEndDateParameters parameters)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateVariationAdjustEndDate", new
        {
            ApplicationId = applicationId,
            parameters.NewEndMonth,
            parameters.NewEndYear
        });

        scope.Complete();
    }

    public async Task<GetVariationScopeResult> GetVariationScope()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<GetVariationScopeResult>(
            "GetVariationScope",
            new
            {
                ApplicationId = applicationId
            });
    }

    public async Task UpdateVariationScope(UpdateVariationScopeParameters parameters)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateVariationScope", new
        {
            ApplicationId = applicationId,
            parameters.ChangeOfScope
        });

        scope.Complete();
    }

    public async Task<GetVariationsCheckYourAnswersResult> GetVariationsCheckYourAnswers()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<GetVariationsCheckYourAnswersResult>(
            "GetVariationsCheckYourAnswers",
            new
            {
                ApplicationId = applicationId
            });
    }

    public async Task<GetConfirmationResult> GetConfirmation()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<GetConfirmationResult>(
            "GetVariationRequestConfirmation",
            new
            {
                ApplicationId = applicationId
            });
    }

    public async Task UpdateConfirmationVariationSummary(UpdateConfirmationVariationSummaryParameters parameters)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateConfirmationVariationSummary", new
        {
            ApplicationId = applicationId,
            parameters.VariationSummary
        });

        scope.Complete();
    }

    public async Task<DeclarationResult> GetDeclaration()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<DeclarationResult>(
            "GetVariationRequestDeclaration",
            new
            {
                ApplicationId = applicationId
            });
    }

    public async Task UpdateDeclaration(UpdateDeclarationParameters parameters)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateVariationRequestDeclaration", new
        {
            ApplicationId = applicationId,
            parameters.ConfirmedAwareOfApproval,
            parameters.ConfirmedCostsReasonable,
            parameters.ConfirmedCoversRecommendations
        });

        scope.Complete();
    }

    public async Task<IReadOnlyCollection<FileResult>> GetEvidence()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return new List<FileResult>();
        }

        return await _connection.QueryAsync<FileResult>("GetVariationRequestEvidence", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task InsertEvidence(Guid fileId)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("InsertVariationRequestEvidence", new
        {
            ApplicationId = applicationId,
            FileId = fileId
        });

        scope.Complete();
    }

    public async Task DeleteEvidence(Guid fileId)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("DeleteVariationRequestEvidence", new
        {
            ApplicationId = applicationId,
            FileId = fileId
        });

        scope.Complete();
    }

    public async Task SubmitVariationRequest()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("SubmitVariationRequest", new
        {
            ApplicationId = applicationId
        });

        scope.Complete();
    }

    private bool TryGetApplicationId(out Guid applicationId)
    {
        applicationId = _applicationDataProvider.GetApplicationId();

        return applicationId != default;
    }

    private bool TryGetApplicationAndVariationRequestIds(out Guid applicationId, out Guid variationRequestId)
    {
        if (!TryGetApplicationId(out applicationId))
        {
            variationRequestId = default;
            return false;
        }

        variationRequestId = _connection.QuerySingleOrDefaultAsync<Guid?>("GetCurrentVariationRequestId", new
        {
            ApplicationId = applicationId
        }).GetAwaiter().GetResult()
        ?? default;

        return variationRequestId != default;
    }

    public async Task<GetVariationCostsResult> GetVariationCosts(Guid variationRequestId)
    {
        var result = await _connection.QuerySingleOrDefaultAsync<GetVariationCostsResult>(nameof(GetVariationCosts),
            new
            {
                VariationRequestId = variationRequestId
            });

        return result;
    }

    public async Task<OverviewResult> GetOverview(Guid? variationRequestId)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<OverviewResult>(
            "GetVariationRequestCostsOverview",
            new
            {
                ApplicationId = applicationId,
                VariationRequestId = variationRequestId
            });
    }

    public async Task<IReadOnlyCollection<CostsProfileResult>> GetCostsProfile(Guid? variationRequestId)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QueryAsync<CostsProfileResult>(
            "GetVariationRequestCostsProfile",
            new
            {
                ApplicationId = applicationId,
                VariationRequestId = variationRequestId
            });
    }

    public async Task<Guid?> GetCurrentVariationRequestId()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<Guid?>(
            "GetCurrentVariationRequestId",
            new
            {
                ApplicationId = applicationId
            });
    }

    public async Task InsertVariationCosts(Guid variationRequestId, Guid workPackageCostsId)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("InsertVariationCosts", new
        {
            VariationRequestId = variationRequestId,
            WorkPackageCostsId = workPackageCostsId
        });

        scope.Complete();
    }

    public async Task<GetVariationUnsafeCladdingCostsResult> GetVariationUnsafeCladdingCosts(Guid variationRequestId)
    {
        return await _connection.QuerySingleOrDefaultAsync<GetVariationUnsafeCladdingCostsResult>(
            "GetVariationUnsafeCladdingCosts",
            new
            {
                @VariationRequestId = variationRequestId
            });
    }

    public async Task UpdateVariationUnsafeCladdingCosts(UpdateVariationUnsafeCladdingCostsParameters parameters)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateVariationUnsafeCladdingCosts", new
        {
            parameters.VariationRequestId,
            parameters.VariationRemovalOfCladdingAmount,
            parameters.VariationRemovalOfCladdingDescription
        });

        scope.Complete();
    }

    public async Task<GetVariationNewCladdingCostsResult> GetVariationNewCladdingCosts(Guid variationRequestId)
    {
        return await _connection.QuerySingleOrDefaultAsync<GetVariationNewCladdingCostsResult>(
            "GetVariationNewCladdingCosts",
            new
            {
                @VariationRequestId = variationRequestId
            });
    }

    public async Task UpdateVariationNewCladdingCosts(UpdateVariationNewCladdingCostsParameters parameters)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateVariationNewCladdingCosts", new
        {
            parameters.VariationRequestId,
            parameters.NewCladdingAmount,
            parameters.NewCladdingDescription,
            parameters.OtherEligibleWorkToExternalWallAmount,
            parameters.OtherEligibleWorkToExternalWallDescription,
            parameters.InternalMitigationWorksAmount,
            parameters.InternalMitigationWorksDescription
        });

        scope.Complete();
    }

    public async Task<GetVariationPreliminaryCostsResult> GetVariationPreliminaryCosts(Guid variationRequestId)
    {
        return await _connection.QuerySingleOrDefaultAsync<GetVariationPreliminaryCostsResult>(
            "GetVariationPreliminaryCosts",
            new
            {
                @VariationRequestId = variationRequestId
            });
    }

    public async Task UpdateVariationPreliminaryCosts(UpdateVariationPreliminaryCostsParameters parameters)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateVariationPreliminaryCosts", new
        {
            parameters.VariationRequestId,
            MainContractorPreliminariesAmount = parameters.VariationMainContractorPreliminariesAmount,
            MainContractorPreliminariesDescription = parameters.VariationMainContractorPreliminariesDescription,
            AccessAmount = parameters.VariationAccessAmount,
            AccessDescription = parameters.VariationAccessDescription,
            OverheadsAndProfitAmount = parameters.VariationOverheadsAndProfitAmount,
            OverheadsAndProfitDescription = parameters.VariationOverheadsAndProfitDescription,
            ContractorContingenciesAmount = parameters.VariationContractorContingenciesAmount,
            ContractorContingenciesDescription = parameters.VariationContractorContingenciesDescription
        });

        scope.Complete();
    }

    public async Task<GetVariationOtherCostsResult> GetVariationOtherCosts(Guid variationRequestId)
    {
        return await _connection.QuerySingleOrDefaultAsync<GetVariationOtherCostsResult>(
            "GetVariationOtherCosts",
            new
            {
                @VariationRequestId = variationRequestId
            });
    }

    public async Task UpdateVariationOtherCosts(UpdateVariationOtherCostsParameters parameters)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateVariationOtherCosts", new
        {
            parameters.VariationRequestId,
            FraewSurveyAmount = parameters.VariationFraewSurveyCostsAmount,
            FraewSurveyDescription = parameters.VariationFraewSurveyCostsDescription,
            FeasibilityStageAmount = parameters.VariationFeasibilityStageAmount,
            FeasibilityStageDescription = parameters.VariationFeasibilityStageDescription,
            PostTenderStageAmount = parameters.VariationPostTenderStageAmount,
            PostTenderStageDescription = parameters.VariationPostTenderStageDescription,
            IrrecoverableVatAmount = parameters.VariationIrrecoverableVatAmount,
            IrrecoverableVatDescription = parameters.VariationIrrecoverableVatDescription,
            PropertyManagerAmount = parameters.VariationPropertyManagerAmount,
            PropertyManagerDescription = parameters.VariationPropertyManagerDescription
        });

        scope.Complete();
    }

    public async Task<GetVariationCostDescriptionsResult> GetVariationCostDescriptions(Guid variationRequestId)
    {
        return await _connection.QuerySingleOrDefaultAsync<GetVariationCostDescriptionsResult>(
            "GetVariationCostDescriptions",
            new
            {
                @VariationRequestId = variationRequestId
            });
    }

    public async Task<ENoYes?> HasVariationIneligibleCosts(Guid variationRequestId)
    {
        return await _connection.QuerySingleOrDefaultAsync<ENoYes?>(
            "HasVariationIneligibleCosts",
            new
            {
                @VariationRequestId = variationRequestId
            });
    }

    public async Task UpdateHasVariationIneligibleCosts(UpdateHasVariationIneligibleCostsParameters parameters)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateHasVariationIneligibleCosts", new
        {
            parameters.VariationRequestId,
            parameters.HasVariationIneligibleCosts
        });

        scope.Complete();
    }


    public async Task<GetVariationIneligibleCostsChangesResult> GetVariationIneligibleCostsChanges(Guid variationRequestId)
    {
        return await _connection.QuerySingleOrDefaultAsync<GetVariationIneligibleCostsChangesResult>(
            "GetVariationIneligibleCostsChanges",
            new
            {
                @VariationRequestId = variationRequestId
            });
    }

    public async Task UpdateVariationIneligibleCosts(UpdateVariationIneligibleCostsParameters parameters)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateVariationIneligibleCosts", new
        {
            parameters.VariationRequestId,
            parameters.HasVariationIneligibleCosts,
            parameters.VariationIneligibleAmount,
            parameters.VariationIneligibleDescription
        });

        scope.Complete();
    }

    public async Task<Guid?> GetLatestVariationRequestId()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<Guid?>(
            "GetLatestVariationRequest",
            new
            {
                ApplicationId = applicationId
            });
    }

    public async Task<VariationRequestThirdPartyContributionResult?> GetThirdPartyContributionsThirdPartyContribution(Guid? variationRequestId)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }
        
        return await _connection.QuerySingleOrDefaultAsync<VariationRequestThirdPartyContributionResult?>("GetVariationRequestThirdPartyContribution", new
        {
            ApplicationId = applicationId,
            VariationRequestId = variationRequestId
        });
    }

    public async Task<List<EFundingStillPursuing>> GetThirdPartyContributionsThirdPartyContributionPursuingTypes(Guid? variationRequestId)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        var contributionPursuingTypes = await _connection.QueryAsync<EFundingStillPursuing>("GetVariationRequestThirdPartyContributionPursuingTypes", new
        {
            ApplicationId = applicationId,
            VariationRequestId = variationRequestId
        });
        return contributionPursuingTypes.ToList();
    }

    public async Task<List<string>> GetThirdPartyContributionsThirdPartyContributionPursuingTypesDescription(Guid? variationRequestId)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        var contributionPursuingTypes = await _connection.QueryAsync<string>("GetVariationRequestThirdPartyContributionPursuingTypesDescription", new
        {
            ApplicationId = applicationId,
            VariationRequestId = variationRequestId
        });
        return contributionPursuingTypes.ToList();
    }

    public async Task UpdateThirdPartyContributionsThirdPartyContribution(UpdateThirdPartyContributionParameters parameters)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var p = new DynamicParameters();
        p.Add("@ApplicationId", applicationId);
        p.Add("@VariationRequestId", parameters.VariationRequestId);
        p.Add("@ContributionPursuingTypes", parameters.ContributionPursuingTypes.ToDataTable().AsTableValuedParameter("[dbo].[IntListType]"), DbType.Object);
        p.Add("@ContributionAmount", parameters.ContributionAmount);
        p.Add("@ContributionNotes", parameters.ContributionNotes);

        await _connection.ExecuteAsync("UpdateVariationRequestThirdPartyContribution", p);

        scope.Complete();
    }
}