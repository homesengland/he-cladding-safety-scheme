using Dapper;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.ProgressReporting.SummariseProgress;
using System.Transactions;
using HE.Remediation.Core.Enums;
using static HE.Remediation.Core.Data.StoredProcedureResults.GetProgressReportResult;

namespace HE.Remediation.Core.Data.Repositories;

public class ProgressReportingRepository : IProgressReportingRepository
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public ProgressReportingRepository(IDbConnectionWrapper connection,
                                       IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }

    public void SetProgressReportId(Guid? progressReportId)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }
        _applicationDataProvider.SetProgressReportId(progressReportId ?? default);
    }

    public async Task<ProgressReportCheckMyAnswersResult> GetProgressReportCheckMyAnswers()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<ProgressReportCheckMyAnswersResult>("GetProgressReportCheckMyAnswers", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });
    }

    public async Task<ProgressReportSecondaryCheckMyAnswersResult> GetProgressReportSecondaryCheckMyAnswers()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<ProgressReportSecondaryCheckMyAnswersResult>("GetProgressReportSecondaryCheckMyAnswers", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });
    }

    public async Task<TeamMemberDetails> GetLeadDesignerCompanyDetails()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<TeamMemberDetails>("GetLeadDesignerCompanyDetails", new
        {
            ApplicationId = applicationId,
            progressReportId
        });
    }


    public async Task<LatestProgressReportResult> GetLatestProgressReport(Guid? applicationId = null)
    {
        if (!TryGetApplicationId(out var appId) && !applicationId.HasValue)
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<LatestProgressReportResult>("GetLatestProgressReport", new
        {
            ApplicationId = applicationId ?? appId
        });
    }

    public async Task<bool?> GetProgressReportLeaseholdersInformed()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<bool?>("GetProgressReportLeaseholdersInformed", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });
    }

    public async Task<DateTime?> GetProgressReportLeaseholdersInformedLastDate()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<DateTime?>("GetProgressReportLeaseholdersInformedLastDate", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });
    }


    public async Task UpdateTeamMember(TeamMemberDetails teamMemberDetails)
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return;
        }

        await _connection.ExecuteAsync("UpdateTeamMember", new
        {
            Id = teamMemberDetails.Id,
            CompanyName = teamMemberDetails.CompanyName,
            CompanyRegistrationNumber = teamMemberDetails.CompanyRegistrationNumber,
            Name = teamMemberDetails.Name,
            EmailAddress = teamMemberDetails.EmailAddress,
            PrimaryContactNumber = teamMemberDetails.PrimaryContactNumber,
            ContractSigned = teamMemberDetails.ContractSigned,
            IndemnityInsurance = teamMemberDetails.IndemnityInsurance,
            LeadDesignerInvolvedInOriginalInstallation = teamMemberDetails.InvolvedInOriginalInstallation,
            IndemnityInsuranceReason = teamMemberDetails.IndemnityInsuranceReason,
            LeadDesignerInvolvedRoleReason = teamMemberDetails.InvolvedRoleReason
        });
    }

    public async Task InsertTeamMember(TeamMemberDetails teamMemberDetails)
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return;
        }

        await _connection.ExecuteAsync("InsertTeamMember", new
        {
            ProgressReportId = teamMemberDetails.ProgressReportId,
            CompanyName = teamMemberDetails.CompanyName,
            CompanyRegistrationNumber = teamMemberDetails.CompanyRegistrationNumber,
            Name = teamMemberDetails.Name,
            EmailAddress = teamMemberDetails.EmailAddress,
            PrimaryContactNumber = teamMemberDetails.PrimaryContactNumber,
            OtherRole = teamMemberDetails.OtherRole,
            ContractSigned = teamMemberDetails.ContractSigned,
            IndemnityInsurance = teamMemberDetails.IndemnityInsurance,
            LeadDesignerInvolvedInOriginalInstallation = teamMemberDetails.InvolvedInOriginalInstallation,
            RoleId = teamMemberDetails.RoleId,
            IndemnityInsuranceReason = teamMemberDetails.IndemnityInsuranceReason,
            LeadDesignerInvolvedRoleReason = teamMemberDetails.InvolvedRoleReason
        });
    }

    public async Task UpdateLeaseholdersInformed(bool? leaseholdersInformed)
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateProgressReportLeaseholdersInformed", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            LeaseholdersInformed = leaseholdersInformed
        });

        scope.Complete();
    }

    public async Task<bool?> GetProgressReportOtherMembersAppointed()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<bool?>("GetProgressReportOtherMembersAppointed", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });
    }

    public async Task ResetProgressReport()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("ResetProgressReport", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });

        scope.Complete();
    }

    public async Task<int> GetProgressReportVersion()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return 1;
        }

        var version = await _connection.QuerySingleOrDefaultAsync<int?>(nameof(GetProgressReportVersion), new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });

        return version ?? 1;
    }

    public async Task<GetProgressReportAnswersResult> GetProgressReportAnswers()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        var answers = await _connection.QuerySingleOrDefaultAsync<GetProgressReportAnswersResult>(
            nameof(GetProgressReportAnswers),
            new
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId
            });

        return answers;
    }

    public async Task<GetProgressReportSupportNeedsResult> GetProgressReportSupportNeeds()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        var result = await _connection.QuerySingleOrDefaultAsync<GetProgressReportSupportNeedsResult>(
            nameof(GetProgressReportSupportNeeds), new GetProgressReportSupportNeedsParameters
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId
            });
        
        return result;
    }

    public async Task UpdateOtherMembersAppointed(bool? otherMembersAppointed)
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateProgressReportOtherMembersAppointed", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            OtherMembersAppointed = otherMembersAppointed
        });

        scope.Complete();
    }

    public async Task<ProgressReportOtherMembersNotAppointedReasonResult> GetProgressReportOtherMembersNotAppointedReason()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<ProgressReportOtherMembersNotAppointedReasonResult>("GetProgressReportOtherMembersNotAppointedReason", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });
    }

    public async Task UpdateOtherMembersNotAppointedReason(string otherMembersNotAppointedReason, bool? otherMembersNeedsSupport)
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateProgressReportOtherMembersNotAppointedReason", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            OtherMembersNotAppointedReason = otherMembersNotAppointedReason,
            OtherMembersNeedsSupport = otherMembersNeedsSupport
        });

        scope.Complete();
    }

    public async Task<string> GetProgressReportTeamMemberName(Guid teamMemberId)
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<string>("GetTeamMemberName", new
        {
            TeamMemberId = teamMemberId
        });
    }

    public async Task DeleteProgressReportTeamMember(Guid teamMemberId)
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("DeleteTeamMember", new
        {
            TeamMemberId = teamMemberId
        });

        scope.Complete();
    }

    public async Task<bool?> GetProgressReportQuotesSought()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<bool?>("GetProgressReportQuotesSought", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });
    }

    public async Task UpdateQuotesSought(bool? quotesSought)
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateProgressReportQuotesSought", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            QuotesSought = quotesSought
        });

        scope.Complete();
    }

    public async Task<ProgressReportQuotesNotSoughtReasonResult> GetProgressReportQuotesNotSoughtReason()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<ProgressReportQuotesNotSoughtReasonResult>("GetProgressReportQuotesNotSoughtReason", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });
    }

    public async Task UpdateQuotesNotSoughtReason(EWhyYouHaveNotSoughtQuotes? whyYouHaveNotSoughtQuotes, string quotesNotSoughtReason, bool? quotesNeedsSupport)
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateProgressReportUpdateQuotesNotSoughtReason", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            WhyYouHaveNotSoughtQuotes = whyYouHaveNotSoughtQuotes,
            QuotesNotSoughtReason = quotesNotSoughtReason,
            QuotesNeedsSupport = quotesNeedsSupport
        });

        scope.Complete();
    }

    public async Task<GetTeamMemberResult> GetTeamMember(Guid? teamMemberId)
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        var result = await _connection.QuerySingleOrDefaultAsync<GetTeamMemberResult>(nameof(GetTeamMember), new
        {
            TeamMemberId = teamMemberId,
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });

        return result;
    }

    public async Task<Guid> UpsertTeamMember(UpsertTeamMemberParameters parameters)
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return default;
        }

        parameters.ProgressReportId = progressReportId;
        return await _connection.QuerySingleOrDefaultAsync<Guid>(nameof(UpsertTeamMember), parameters);
    }

    public async Task<EYesNoNonBoolean?> GetProgressReportRequirePlanningPermission()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<EYesNoNonBoolean?>("GetProgressReportRequirePlanningPermission", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });
    }

    public async Task UpdateRequirePlanningPermission(EYesNoNonBoolean? planningPermissionRequired)
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateProgressReportRequirePlanningPermission", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            RequirePlanningPermission = planningPermissionRequired
        });

        scope.Complete();
    }

    public async Task<bool?> GetProgressReportAppliedForPlanningPermission()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<bool?>("GetProgressReportAppliedForPlanningPermission", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });
    }

    public async Task UpdateAppliedForPlanningPermission(bool? appliedForPlanningPermission)
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateProgressReportAppliedForPlanningPermission", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            AppliedForPlanningPermission = appliedForPlanningPermission
        });

        scope.Complete();
    }

    public async Task<ProgressReportPlanningPermissionNotAppliedReasonResult> GetProgressReportPlanningPermissionNotAppliedReason()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<ProgressReportPlanningPermissionNotAppliedReasonResult>("GetProgressReportPlanningPermissionNotAppliedReason", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });
    }

    public async Task UpdatePlanningPermissionNotAppliedReason(string planningPermissionNotAppliedReason, bool? planningPermissionNeedsSupport)
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateProgressReportPlanningPermissionNotAppliedReason", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            ReasonPlanningPermissionNotApplied = planningPermissionNotAppliedReason,
            PlanningPermissionNeedsSupport = planningPermissionNeedsSupport
        });

        scope.Complete();
    }

    public async Task<List<GetTeamMembersResult>> GetTeamMembers()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        var results = await _connection.QueryAsync<GetTeamMembersResult>("GetTeamMembers", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });

        return results.ToList();
    }

    public async Task<DateTime?> GetProgressReportExpectedWorksPackageSubmissionDate()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<DateTime?>("GetProgressReportExpectedWorksPackageSubmissionDate", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });
    }

    public async Task<DateTime?> GetProgressReportExpectedStartDateOnSite()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<DateTime?>("GetProgressReportExpectedStartDateOnSite", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });
    }

    public async Task UpdateProgressReportExpectedWorksPackageSubmissionDate(DateTime? submissionDate)
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateProgressReportExpectedWorksPackageSubmissionDate", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            ExpectedWorksPackageSubmissionDate = submissionDate
        });

        scope.Complete();
    }

    public async Task UpdateProgressReportExpectedStartDateOnSite(DateTime? expectedStartDateOnSite)
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateProgressReportExpectedStartDateOnSite", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            ExpectedStartDateOnSite = expectedStartDateOnSite
        });

        scope.Complete();
    }

    public async Task<DateTime?> GetProgressReportPlanningPermissionPlannedSubmitDate()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<DateTime?>("GetProgressReportPlanningPermissionPlannedSubmitDate", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });
    }

    public async Task UpdateProgressReportPlanningPermissionPlannedSubmitDate(DateTime? planningPermissionPlannedSubmitDate)
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateProgressReportPlanningPermissionPlannedSubmitDate", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            PlanningPermissionPlannedSubmitDate = planningPermissionPlannedSubmitDate
        });

        scope.Complete();
    }

    public async Task<ProgressReportPlanningPermissionDetailsResult> GetProgressReportPlanningPermissionDetails()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<ProgressReportPlanningPermissionDetailsResult>("GetProgressReportPlanningPermissionDetails", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });
    }

    public async Task UpdateProgressReportPlanningPermissionDetails(DateTime? planningPermissionSubmittedDate, DateTime? planningPermissionApprovedDate)
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateProgressReportPlanningPermissionDetails", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            PlanningPermissionSubmittedDate = planningPermissionSubmittedDate,
            PlanningPermissionApprovedDate = planningPermissionApprovedDate
        });

        scope.Complete();
    }

    public async Task<bool> GetProgressReportShowBuildingSafetyRegulatorRegistrationCode()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return false;
        }

        return await _connection.QuerySingleOrDefaultAsync<bool>("GetProgressReportShowBuildingSafetyRegulatorRegistrationCode", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });
    }

    public async Task<bool?> GetProgressReportBuildingHasSafetyRegulatorRegistrationCode()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<bool?>("GetProgressReportingBuildingHasSafetyRegulatorRegistrationCode", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });
    }

    public async Task UpdateProgressReportingBuildingHasSafetyRegulatorRegistrationCode(bool? buildingHasSafetyRegulatorRegistrationCode)
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateProgressReportingBuildingHasSafetyRegulatorRegistrationCode", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            BuildingHasSafetyRegulatorRegistrationCode = buildingHasSafetyRegulatorRegistrationCode
        });

        scope.Complete();
    }

    public async Task<string> GetProgressReportBuildingSafetyRegulatorRegistrationCode()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<string>("GetProgressReportingBuildingSafetyRegulatorRegistrationCode", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });
    }

    public async Task UpdateProgressReportingBuildingSafetyRegulatorRegistrationCode(string buildingSafetyRegulatorRegistrationCode)
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateProgressReportingBuildingSafetyRegulatorRegistrationCode", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            BuildingSafetyRegulatorRegistrationCode = buildingSafetyRegulatorRegistrationCode
        });

        scope.Complete();
    }

    public async Task<string> GetProgressReportSupportNeededReason()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<string>("GetProgressReportSupportNeededReason", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });
    }

    public async Task<bool?> GetProgressReportLeadDesignerNeedsSupport()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<bool?>("GetProgressReportLeadDesignerNeedsSupport", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });
    }

    public async Task<bool?> GetProgressReportOtherMembersNeedsSupport()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<bool?>("GetProgressReportOtherMembersNeedsSupport", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });
    }

    public async Task<bool?> GetProgressReportQuotesNeedsSupport()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<bool?>("GetProgressReportQuotesNeedsSupport", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });
    }

    public async Task<bool?> GetProgressReportPlanningPermissionNeedsSupport()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<bool?>("GetProgressReportPlanningPermissionNeedsSupport", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });
    }

    public async Task UpdateProgressReportSupportNeededReason(string supportNeededReason)
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateProgressReportSupportNeededReason", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            SupportNeededReason = supportNeededReason
        });

        scope.Complete();
    }

    public async Task<DateTime?> GetProgressReportDateSubmitted()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        var submittedDate = await _connection.QuerySingleOrDefaultAsync<DateTime?>(nameof(GetProgressReportDateSubmitted), new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });

        return submittedDate;
    }

    public async Task UpdateProgressReportDateSubmitted(DateTime? dateSubmitted, Guid? userId)
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return;
        }

        await _connection.ExecuteAsync("UpdateProgressReportDateSubmitted", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            DateSubmitted = dateSubmitted,
            UserId = userId
        });
    }

    public async Task<GetProgressReportResult> GetProgressReportDetails(Guid applicationId, Guid progressReportId)
    {
        GetProgressReportResult result = null;

        await _connection.QueryAsync<GetProgressReportResult, TeamMemberResult, GetProgressReportResult>(
            nameof(GetProgressReportDetails),
            (report, teamMember) =>
            {
                result ??= report;
                if (teamMember is not null)
                {
                    result.TeamMembers.Add(teamMember);
                }

                return result;
            },
            new
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId
            }
        );

        return result;
    }

    public async Task<IReadOnlyCollection<FileResult>> GetProgressReportLeaseholdersInformedFiles()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        return await _connection.QueryAsync<FileResult>("GetProgressReportLeaseholdersInformedFiles", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });
    }

    public async Task UpdateProgressReportLeaseholdersInformedFileId(Guid? fileId)
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateProgressReportLeaseholdersInformedFile", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            LeaseholdersInformedFileId = fileId
        });

        scope.Complete();
    }

    public async Task<IReadOnlyCollection<ProgressReportResult>> GetProgressReports()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return new List<ProgressReportResult>();
        }

        return await _connection.QueryAsync<ProgressReportResult>("GetProgressReports", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task<bool> HasSubmittedProgressReports()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return false;
        }

        return await _connection.QuerySingleOrDefaultAsync<bool>("HasSubmittedProgressReports", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task DeleteLeadDesignerNotAppointedReason()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return;
        }

        await _connection.ExecuteAsync("DeleteLeadDesignerNotAppointedReason", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });
    }

    public async Task DeleteLeadDesignerForCurrentProgressReport()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return;
        }

        await _connection.ExecuteAsync("DeleteLeadDesignerForProgressReport", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });
    }

    public async Task<bool> IsProgressReportSubmitted(Guid applicationId, Guid progressReportId)
    {
        var result = await _connection.QuerySingleOrDefaultAsync<bool>(nameof(IsProgressReportSubmitted), new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });

        return result;
    }

    public async Task<bool?> GetLastSubmittedProgressReportOtherMembersAppointed()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<bool?>("GetLastSubmittedProgressReportOtherMembersAppointed", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task<int?> GetLastSubmittedProgressReportRequirePlanningPermission()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<int?>("GetLastSubmittedProgressReportRequirePlanningPermission", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task<List<GetTeamMembersResult>> GetLastSubmittedProgressReportTeamMembers()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        var results = await _connection.QueryAsync<GetTeamMembersResult>("GetLastSubmittedProgressReportTeamMembers", new
        {
            ApplicationId = applicationId
        });

        return results.ToList();
    }

    private bool TryGetApplicationAndProgressReportIds(out Guid applicationId, out Guid progressReportId)
    {
        progressReportId = _applicationDataProvider.GetProgressReportId();

        return TryGetApplicationId(out applicationId) && progressReportId != default;
    }

    private bool TryGetApplicationId(out Guid applicationId)
    {
        applicationId = _applicationDataProvider.GetApplicationId();

        return applicationId != default;
    }

    public async Task UpdateLeaseholdersInformedLastDate(DateTime? leaseholdersInformedLastDate)
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateProgressReportLeaseholdersInformedLastDate", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            LeaseholdersInformedLastDate = leaseholdersInformedLastDate
        });

        scope.Complete();
    }

    public async Task<GetProgressReportProgressSummaryResult> GetProgressReportProgressSummary()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<GetProgressReportProgressSummaryResult>("GetProgressReportSummariseProgress", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });
    }
    public async Task<GetProgressReportProgressSummaryResult> GetProgressReportProgressSummary(Guid progressReportId)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<GetProgressReportProgressSummaryResult>("GetProgressReportSummariseProgress", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });
    }

    public async Task UpdateSummariseProgress(SetSummariseProgressRequest request)
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateProgressReportSummariseProgress", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            ProgressSummary = request.ProgressSummary,
            RequiresSupport = request.IsSupportNeeded
        });

        scope.Complete();
    }

    public async Task<GetProgressReportSupportResult> GetProgressReportSupport()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        var result = await _connection.QuerySingleOrDefaultAsync<GetProgressReportSupportResult>(
            nameof(GetProgressReportSupport),
            new
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId
            });

        return result;
    }

    public async Task UpdateProgressReportSupport(UpdateProgressReportSupportParameters sprocParameters)
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return;
        }

        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);
        parameters.Add("@ProgressReportId", progressReportId);
        parameters.Add("@SupportNeededReason", sprocParameters.SupportNeedsReason);
        parameters.Add("@LeadDesignerNeedsSupport", sprocParameters.LeadDesignerNeedsSupport);
        parameters.Add("@OtherMembersNeedsSupport", sprocParameters.OtherMembersNeedsSupport);
        parameters.Add("@QuotesNeedsSupport", sprocParameters.QuotesNeedsSupport);
        parameters.Add("@PlanningPermissionNeedsSupport", sprocParameters.PlanningPermissionNeedsSupport);
        parameters.Add("@OtherNeedsSupport", sprocParameters.OtherNeedsSupport);

        await _connection.ExecuteAsync(nameof(UpdateProgressReportSupport), parameters);
    }

    public async Task<bool?> GetBuildingControlRequired()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        var result = await _connection.QuerySingleOrDefaultAsync<bool?>(nameof(GetBuildingControlRequired), new { applicationId, progressReportId });

        return result;
    }

    public async Task UpdateBuildingControlRequired(bool buildingControlRequired)
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return;
        }

        await _connection.ExecuteAsync(nameof(UpdateBuildingControlRequired), new { applicationId, progressReportId, buildingControlRequired });
    }

    public async Task<bool?> GetHasGrantCertifyingOfficer()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        var result = await _connection.QuerySingleOrDefaultAsync<bool?>(nameof(GetHasGrantCertifyingOfficer), new { ApplicationId = applicationId, ProgressReportId = progressReportId });
        return result;
    }

    public async Task UpdateHasGrantCertifyingOfficer(bool hasGco)
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return;
        }

        await _connection.ExecuteAsync(nameof(UpdateHasGrantCertifyingOfficer), new { ApplicationId = applicationId, ProgressReportId = progressReportId, HasGco = hasGco });
    }

    public async Task<Guid?> GetGrantCertifyingOfficerTeamMember()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        var result = await _connection.QuerySingleOrDefaultAsync<Guid?>(nameof(GetGrantCertifyingOfficerTeamMember),
            new { ApplicationId = applicationId, ProgressReportId = progressReportId });

        return result;
    }

    public async Task<IReadOnlyCollection<GetProjectManagersAndQuantitySurveyorsResult>> GetProjectManagersAndQuantitySurveyors()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        var result = await _connection.QueryAsync<GetProjectManagersAndQuantitySurveyorsResult>(nameof(GetProjectManagersAndQuantitySurveyors),
            new { ApplicationId = applicationId, ProgressReportId = progressReportId });

        return result;
    }

    public async Task UpdateGrantCertifyingOfficerTeamMember(Guid projectTeamMemberId)
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return;
        }

        await _connection.ExecuteAsync(nameof(UpdateGrantCertifyingOfficerTeamMember), new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            ProjectTeamMemberId = projectTeamMemberId
        });
    }

    public async Task<GetGcoDetailsResult> GetGcoDetails()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        var result = await _connection.QuerySingleOrDefaultAsync<GetGcoDetailsResult>(nameof(GetGcoDetails),
            new
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId
            });

        return result;
    }

    public async Task UpdateGrantCertifiyingOfficerResponse(ECertifyingOfficerResponse certifyingOfficerResponse)
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return;
        }

        await _connection.ExecuteAsync(nameof(UpdateGrantCertifiyingOfficerResponse), new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            CertifyingOfficerResponseId = (int)certifyingOfficerResponse
        });
    }

    public async Task UpdateGrantCertifyingOfficerDetails()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return;
        }

        await _connection.ExecuteAsync(nameof(UpdateGrantCertifyingOfficerDetails), new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });
    }

    public async Task<GetGrantCertifyingOfficerAddressResult> GetGrantCertifyingOfficerAddress()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        var result = await _connection.QuerySingleOrDefaultAsync<GetGrantCertifyingOfficerAddressResult>(
            nameof(GetGrantCertifyingOfficerAddress),
            new
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId
            });

        return result;
    }

    public async Task UpdateGrantCertifyingOfficerAddress(UpdateGrantCertifyingOfficerAddressParameters parameters)
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return;
        }

        parameters.ApplicationId = applicationId;
        parameters.ProgressReportId = progressReportId;

        await _connection.ExecuteAsync(nameof(UpdateGrantCertifyingOfficerAddress), parameters);
    }

    public async Task<GetGrantCertifyingOfficerSignatoryResult> GetGrantCertifyingOfficerSignatory()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        var result = await _connection.QuerySingleOrDefaultAsync<GetGrantCertifyingOfficerSignatoryResult>(
            nameof(GetGrantCertifyingOfficerSignatory),
            new
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId
            });

        return result;
    }

    public async Task UpdateGrantCertifyingOfficerSignatory(UpdateGrantCertifyingOfficerSignatoryParameters parameters)
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return;
        }

        parameters.ApplicationId = applicationId;
        parameters.ProgressReportId = progressReportId;

        await _connection.ExecuteAsync(nameof(UpdateGrantCertifyingOfficerSignatory), parameters);
    }

    public async Task<GetGrantCerifyingOfficerAnswersResult> GetGrantCerifyingOfficerAnswers()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        var result = await _connection.QuerySingleOrDefaultAsync<GetGrantCerifyingOfficerAnswersResult>(
            nameof(GetGrantCerifyingOfficerAnswers),
            new
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId
            });

        return result;
    }

    public async Task<bool> IsGrantCertifyingOfficerComplete()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return false;
        }

        var result = await _connection.QuerySingleOrDefaultAsync<bool>(nameof(IsGrantCertifyingOfficerComplete),
            new
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId
            });

        return result;
    }

    public async Task SetDutyOfCareDeedTaskRaised(bool taskRaised)
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return;
        }

        await _connection.ExecuteAsync(nameof(SetDutyOfCareDeedTaskRaised),
            new
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId,
                TaskRaised = taskRaised
            });
    }

    public async Task<bool> GetDutyOfCareDeedTaskRaised()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return false;
        }

        var result = await _connection.QuerySingleOrDefaultAsync<bool>(nameof(GetDutyOfCareDeedTaskRaised),
            new
            {
                ApplicationId = applicationId,
                ProgressReportId = progressReportId
            });

        return result;
    }

    public async Task<EIntentToProceedType?> GetIntentToProceedType(GetIntentToProceedTypeParameters parameters)
    {
        var result = await _connection.QuerySingleOrDefaultAsync<EIntentToProceedType?>(nameof(GetIntentToProceedType), parameters);
        return result;
    }

    public async Task UpdateIntentToProceedType(UpdateIntentToProceedTypeParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(UpdateIntentToProceedType), parameters);
    }

    public async Task<bool?> GetProgressReportHasProjectPlanMilestones()
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<bool?>("GetProgressReportHasProjectPlanMilestones", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });
    }

    public async Task UpdateProgressReportHasProjectPlanMilestones(bool? hasProjectPlanMilestones)
    {
        if (!TryGetApplicationAndProgressReportIds(out var applicationId, out var progressReportId))
        {
            return;
        }

        await _connection.ExecuteAsync("UpdateProgressReportHasProjectPlanMilestones", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId,
            HasProjectPlanMilestones = hasProjectPlanMilestones
        });
    }

    public async Task RemoveProgressReportLeaseholderInformationDocument(RemoveProgressReportLeaseholderInformationDocumentParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(RemoveProgressReportLeaseholderInformationDocument), parameters);
    }

    public async Task<GetBuildingControlDecisionResult> GetBuildingControlDecision(GetBuildingControlDecisionParameters parameters)
    {
        var result = await _connection.QuerySingleOrDefaultAsync<GetBuildingControlDecisionResult>(nameof(GetBuildingControlDecision), parameters);
        return result;
    }

    public async Task UpdateBuildingControlDecision(UpdateBuildingControlDecisionParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(UpdateBuildingControlDecision), parameters);
    }

    public async Task<GetBuildingControlForecastResult> GetBuildingControlForecast(GetBuildingControlForecastParameters parameters)
    {
        var result = await _connection.QuerySingleOrDefaultAsync<GetBuildingControlForecastResult>(nameof(GetBuildingControlForecast), parameters);
        return result;
    }

    public async Task UpdateBuildingControlForecast(UpdateBuildingControlForecastParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(UpdateBuildingControlForecast), parameters);
    }

    public async Task<GetBuildingControlSubmissionResult> GetBuildingControlSubmission(GetBuildingControlSubmissionParameters parameters)
    {
        var result = await _connection.QuerySingleOrDefaultAsync<GetBuildingControlSubmissionResult>(nameof(GetBuildingControlSubmission), parameters);
        return result;
    }

    public async Task UpdateBuildingControlSubmission(UpdateBuildingControlSubmissionParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(UpdateBuildingControlSubmission), parameters);
    }

    public async Task<GetBuildingControlValidationResult> GetBuildingControlValidation(GetBuildingControlValidationParameters parameters)
    {
        var result = await _connection.QuerySingleOrDefaultAsync<GetBuildingControlValidationResult>(nameof(GetBuildingControlValidation), parameters);
        return result;
    }

    public async Task UpdateBuildingControlValidation(UpdateBuildingControlValidationParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(UpdateBuildingControlValidation), parameters);
    }

    public async Task<GetHasAppliedForBuildingControlResult> GetHasAppliedForBuildingControl(GetHasAppliedForBuildingControlParameters parameters)
    {
        var result = await _connection.QuerySingleOrDefaultAsync<GetHasAppliedForBuildingControlResult>(nameof(GetHasAppliedForBuildingControl), parameters);
        return result;
    }

    public async Task UpdateHasAppliedForBuildingControl(UpdateHasAppliedForBuildingControlParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(UpdateHasAppliedForBuildingControl), parameters);
    }
}
