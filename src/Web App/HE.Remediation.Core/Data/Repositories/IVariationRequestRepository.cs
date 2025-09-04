using HE.Remediation.Core.Data.StoredProcedureParameters.VariationRequest;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Data.StoredProcedureResults.VariationRequest;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.Data.Repositories;

public interface IVariationRequestRepository
{
    Task<bool> HasInProgressVariationRequest();

    Task<bool> IsVariationRequestSubmitted();

    Task<EVariationRequestApprovalStatus?> GetApprovalStatus();

    Task<int?> GetVariationNumber();

    Task<GetVariationReasonResult> GetVariationReason();

    Task UpsertVariationSelection(UpsertVariationSelectionParameters request);

    Task<GetAdjustEndDateResult> GetVariationAdjustEndDate();

    Task UpdateVariationAdjustEndDate(UpdateVariationAdjustEndDateParameters parameters);

    Task<GetVariationScopeResult> GetVariationScope();

    Task UpdateVariationScope(UpdateVariationScopeParameters parameters);

    Task<GetVariationsCheckYourAnswersResult> GetVariationsCheckYourAnswers();

    Task<GetConfirmationResult> GetConfirmation();

    Task UpdateConfirmationVariationSummary(UpdateConfirmationVariationSummaryParameters parameters);

    Task<DeclarationResult> GetDeclaration();

    Task UpdateDeclaration(UpdateDeclarationParameters parameters);

    Task InsertEvidence(Guid fileId);

    Task DeleteEvidence(Guid fileId);

    Task<IReadOnlyCollection<FileResult>> GetEvidence();

    Task SubmitVariationRequest(Guid? userId);

    Task<GetVariationCostsResult> GetVariationCosts(Guid variationRequestId);

    Task<OverviewResult> GetOverview(Guid? variationRequestId);

    Task<IReadOnlyCollection<CostsProfileResult>> GetCostsProfile(Guid? variationRequestId);

    Task<Guid?> GetCurrentVariationRequestId();

    Task InsertVariationCosts(Guid variationRequestId, Guid workPackageCostsId);

    Task<GetVariationUnsafeCladdingCostsResult> GetVariationUnsafeCladdingCosts(Guid variationRequestId);

    Task UpdateVariationUnsafeCladdingCosts(UpdateVariationUnsafeCladdingCostsParameters parameters);

    Task<GetVariationNewCladdingCostsResult> GetVariationNewCladdingCosts(Guid variationRequestId);

    Task UpdateVariationNewCladdingCosts(UpdateVariationNewCladdingCostsParameters parameters);

    Task<GetVariationPreliminaryCostsResult> GetVariationPreliminaryCosts(Guid variationRequestId);

    Task UpdateVariationPreliminaryCosts(UpdateVariationPreliminaryCostsParameters parameters);

    Task<GetVariationOtherCostsResult> GetVariationOtherCosts(Guid variationRequestId);

    Task UpdateVariationOtherCosts(UpdateVariationOtherCostsParameters parameters);

    Task<GetVariationCostDescriptionsResult> GetVariationCostDescriptions(Guid variationRequestId);

    Task<ENoYes?> HasVariationIneligibleCosts(Guid variationRequestId);

    Task<GetVariationContractorContingencyResult> GetVariationContractorContingency(Guid? variationRequestId);

    Task<ENoYes?> UsedVariationContractorContingency(Guid? variationRequestId);

    Task UpdateVariationContractorContingency(UpdateVariationContractorContingencyParameters parameters);

    Task<GetVariationIneligibleCostsChangesResult> GetVariationIneligibleCostsChanges(Guid variationRequestId);

    Task UpdateVariationIneligibleCosts(UpdateVariationIneligibleCostsParameters parameters);

    Task UpdateHasVariationIneligibleCosts(UpdateHasVariationIneligibleCostsParameters parameters);

    Task<Guid?> GetLatestVariationRequestId();

    #region Third Party Contributions
    Task<VariationRequestThirdPartyContributionResult> GetThirdPartyContributionsThirdPartyContribution(Guid? variationRequestId);

    Task<List<EFundingStillPursuing>> GetThirdPartyContributionsThirdPartyContributionPursuingTypes(Guid? variationRequestId);

    Task<List<string>> GetThirdPartyContributionsThirdPartyContributionPursuingTypesDescription(Guid? variationRequestId);

    Task UpdateThirdPartyContributionsThirdPartyContribution(UpdateThirdPartyContributionParameters parameters);

    #endregion
}
