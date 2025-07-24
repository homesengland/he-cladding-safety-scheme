using Dapper;
using HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.CostsScheduling;
using HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.Declaration;
using HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.GrantCertifyingOfficer;
using HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage;
using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.CostsScheduling;
using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.Declaration;
using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.GrantCertifyingOfficer;
using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.KeyDates;
using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.PlanningPermission;
using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.ProjectTeam;
using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.WorkPackageDutyOfCareDeedResult;
using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.WorkPackageSignatories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using System.Transactions;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Data.StoredProcedureResults.WorkPackage.ThirdPartyContributions;
using HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.ThirdPartyContributions;
using System.Data;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Extensions;
using HE.Remediation.Core.Services.StatusTransition;
using UpsertTeamMemberParameters = HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.ProjectTeam.UpsertTeamMemberParameters;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;

namespace HE.Remediation.Core.Data.Repositories;

public class WorkPackageRepository : IWorkPackageRepository
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IStatusTransitionService _statusTransitionService;

    public WorkPackageRepository(IDbConnectionWrapper connection,
        IApplicationDataProvider applicationDataProvider, 
        IStatusTransitionService statusTransitionService)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
        _statusTransitionService = statusTransitionService;
    }

    public async Task<bool?> GetWorkPackageConfirmToProceed()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<bool?>(
            "GetWorkPackageConfirmToProceed", new
            {
                ApplicationId = applicationId
            });
    }

    public async Task UpdateWorkPackageConfirmToProceed(bool? isConfirmedToProceed)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            throw new InvalidOperationException("Unable to retrieve ApplicationId.");
        }

        await _connection.ExecuteAsync(
            "UpdateWorkPackageConfirmToProceed", new
            {
                ApplicationId = applicationId,
                IsConfirmedToProceed = isConfirmedToProceed
            });
    }

    public async Task<WorkPackageTaskListSummaryResult> GetWorkPackageTaskListSummary()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<WorkPackageTaskListSummaryResult>(
            "GetWorkPackageTaskListSummary", new
            {
                ApplicationId = applicationId
            });
    }

    public async Task<bool> HasWorkPackage()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return false;
        }

        return await _connection.QuerySingleOrDefaultAsync<bool>("HasWorkPackage", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task SubmitWorkPackage(Guid? userId)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("SubmitWorkPackage", new
        {
            ApplicationId = applicationId,
            UserId = userId
        });

        scope.Complete();
    }

    public async Task<bool> IsWorkPackageSubmitted()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return false;
        }

        return await _connection.QuerySingleOrDefaultAsync<bool>("GetIsWorkPackageSubmitted", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task<bool> IsCladdingBeingRemoved()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return false;
        }

        return await _connection.QuerySingleOrDefaultAsync<bool>("GetIsCladdingBeingRemoved", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task<bool> IsSignedUpToConsiderateConstructorsScheme(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        var result = await _connection.QuerySingleOrDefaultAsync<bool>(nameof(IsSignedUpToConsiderateConstructorsScheme), parameters);
        return result;
    }

    #region Grant funding officer

    public async Task<IReadOnlyCollection<GrantCertifyingOfficerCandidateResult>> GetGrantCertifyingOfficerCandidates()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QueryAsync<GrantCertifyingOfficerCandidateResult>(
            "GetWorkPackageGrantCertifyingOfficerCandidates", new
            {
                ApplicationId = applicationId
            });
    }

    public async Task<GrantCertifyingOfficerAddressResult> GetGrantCertifyingOfficerAddress()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<GrantCertifyingOfficerAddressResult>(
            "GetWorkPackageGrantCertifyingOfficerAddress",
            new
            {
                ApplicationId = applicationId
            });
    }

    public async Task<GrantCertifyingOfficerAnswersResult> GetGrantCertifyingOfficerAnswers()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<GrantCertifyingOfficerAnswersResult>(
            "GetWorkPackageGrantCertifyingOfficerCheckYourAnswers",
            new
            {
                ApplicationId = applicationId
            });
    }

    public async Task<GrantCertifyingOfficerDetailsResult> GetGrantCertifyingOfficerDetails()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<GrantCertifyingOfficerDetailsResult>(
            "GetWorkPackageGrantCertifyingOfficerDetails", new
            {
                ApplicationId = applicationId
            });
    }

    public async Task<GrantCertifyingOfficerIsCorrectPersonResult> GetGrantCertifyingOfficerIsCorrectPerson()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<GrantCertifyingOfficerIsCorrectPersonResult>(
            "GetWorkPackageGrantCertifyingOfficerIsCorrectPerson", new
            {
                ApplicationId = applicationId
            });
    }

    public async Task<bool> GetGrantCertifyingOfficerDutyOfCareDeedTaskRaised()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return false;
        }

        return await _connection.QuerySingleOrDefaultAsync<bool>(
            "GetWorkPackageGrantCertifyingOfficerDutyOfCareDeedTaskRaised", new
            {
                ApplicationId = applicationId
            });
    }

    public async Task<Guid?> GetGrantCertifyingOfficerProjectTeamMemberId()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<Guid?>("GetWorkPackageGrantCertifyingOfficerProjectTeamMemberId", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task<ETaskStatus?> GetGrantCertifyingOfficerStatus()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<ETaskStatus?>("GetWorkPackageGrantCertifyingOfficerStatus",
            new
            {
                ApplicationId = applicationId
            });
    }

    public async Task InsertGrantCertifyingOfficer(InsertGrantCertifyingOfficerParameters parameters)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("InsertWorkPackageGrantCertifyingOfficer", new
        {
            ApplicationId = applicationId,
            parameters.ProjectTeamMemberId,
            parameters.Name,
            parameters.CompanyName,
            parameters.CompanyRegistrationNumber,
            parameters.EmailAddress,
            parameters.PrimaryContactNumber,
            parameters.ContractSigned,
            parameters.IndemnityInsurance,
            parameters.IndemnityInsuranceReason,
            parameters.InvolvedInOriginalInstallation,
            parameters.InvolvedRoleReason,
            parameters.CertifyingOfficerResponseId
        });

        scope.Complete();
    }

    public async Task UpdateGrantCertifyingOfficer(UpdateGrantCertifyingOfficerParameters parameters)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateWorkPackageGrantCertifyingOfficer", new
        {
            ApplicationId = applicationId,
            parameters.ProjectTeamMemberId,
            parameters.Name,
            parameters.CompanyName,
            parameters.CompanyRegistrationNumber,
            parameters.EmailAddress,
            parameters.PrimaryContactNumber,
            parameters.ContractSigned,
            parameters.IndemnityInsurance,
            parameters.IndemnityInsuranceReason,
            parameters.InvolvedInOriginalInstallation,
            parameters.InvolvedRoleReason
        });

        scope.Complete();
    }

    public async Task UpdateGrantCertifyingOfficerDetails(UpdateGrantCertifyingOfficerParameters parameters)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateWorkPackageGrantCertifyingOfficerDetails", new
        {
            ApplicationId = applicationId,
            parameters.Name,
            parameters.CompanyName,
            parameters.CompanyRegistrationNumber,
            parameters.EmailAddress,
            parameters.PrimaryContactNumber,
            parameters.ContractSigned,
            parameters.IndemnityInsurance,
            parameters.IndemnityInsuranceReason,
            parameters.InvolvedInOriginalInstallation,
            parameters.InvolvedRoleReason
        });

        scope.Complete();
    }

    public async Task UpdateWorkPackageGrantCertifyingOfficerAddress(
        UpdateWorkPackageGrantCertifyingOfficerAddressParameters parameters)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        parameters.ApplicationId = applicationId;

        await _connection.ExecuteAsync("SetWorkPackageGrantCertifyingOfficerAddress", parameters);

        scope.Complete();
    }

    public async Task UpdateGrantCertifyingOfficerConfirmation(ECertifyingOfficerResponse certifyingOfficerResponse)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateWorkPackageGrantCertifyingOfficerConfirmation", new
        {
            ApplicationId = applicationId,
            CertifyingOfficerResponseId = (int)certifyingOfficerResponse
        });

        scope.Complete();
    }

    public async Task UpdateGrantCertifyingOfficerConfirmation(bool? isCorrectPerson, bool? updateRequested)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateWorkPackageGrantCertifyingOfficerConfirmation", new
        {
            ApplicationId = applicationId,
            IsCorrectPerson = isCorrectPerson,
            UpdateRequested = updateRequested
        });

        scope.Complete();
    }

    public async Task UpdateGrantCertifyingOfficerDutyOfCareDeedTask(bool dutyOfCareDeedTaskRaised)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateWorkPackageGrantCertifyingOfficerDutyOfCareDeedTask", new
        {
            ApplicationId = applicationId,
            DutyOfCareDeedTaskRaised = dutyOfCareDeedTaskRaised
        });

        scope.Complete();
    }

    public async Task UpdateGrantCertifyingOfficerStatus(ETaskStatus taskStatus)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateWorkPackageGrantCertifyingOfficerStatus", new
        {
            ApplicationId = applicationId,
            TaskStatusId = taskStatus
        });

        scope.Complete();
    }

    public async Task ResetGrantCertifyingOfficer()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("ResetWorkPackageGrantCertifyingOfficer", new
        {
            ApplicationId = applicationId
        });

        scope.Complete();
    }

    #endregion

    #region Planning Permission

    public async Task<WorkPackagePlanningPermissionResult> GetRequirePlanningPermission()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<WorkPackagePlanningPermissionResult>(
            "GetWorkPackageRequirePlanningPermission", new
            {
                ApplicationId = applicationId
            });
    }

    public async Task<ETaskStatus?> GetWorkPackagePlanningPermissionStatus()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<ETaskStatus?>("GetWorkPackagePlanningPermissionStatus", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task ResetWorkPackagePlanningPermission()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("ResetWorkPackagePlanningPermission", new
        {
            ApplicationId = applicationId
        });

        scope.Complete();
    }

    public async Task InsertRequirePlanningPermission(bool? planningPermissionRequired,
        string planningPermissionNotRequiredReason)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("InsertWorkPackageRequirePlanningPermission", new
        {
            ApplicationId = applicationId,
            RequirePlanningPermission = planningPermissionRequired,
            ReasonPlanningPermissionNotRequired = planningPermissionNotRequiredReason
        });

        scope.Complete();
    }

    public async Task UpdateRequirePlanningPermission(bool? planningPermissionRequired,
        string planningPermissionNotRequiredReason)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateWorkPackageRequirePlanningPermission", new
        {
            ApplicationId = applicationId,
            RequirePlanningPermission = planningPermissionRequired,
            ReasonPlanningPermissionNotRequired = planningPermissionNotRequiredReason
        });

        scope.Complete();
    }

    public async Task UpdateWorkPackagePlanningPermissionStatus(ETaskStatus taskStatus)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateWorkPackagePlanningPermissionStatus", new
        {
            ApplicationId = applicationId,
            @TaskStatusId = taskStatus
        });

        if (taskStatus == ETaskStatus.InProgress)
        {
            await _statusTransitionService.TransitionToStatus(EApplicationStatus.WorksPackageInProgress, applicationIds: applicationId);
        }

        scope.Complete();
    }

    #endregion

   

    #region Key Dates

    public async Task ResetKeyDates()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("ResetWorkPackageKeyDates", new
        {
            ApplicationId = applicationId
        });

        scope.Complete();
    }

    public async Task InsertKeyDates(InsertKeyDatesParameters parameters)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("InsertWorkPackageKeyDates", new
        {
            ApplicationId = applicationId,
            parameters.StartDate,
            parameters.UnsafeCladdingRemovalDate,
            parameters.ExpectedDateForCompletion
        });

        scope.Complete();
    }

    public async Task UpdateKeyDates(UpdateKeyDatesParameters parameters)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateWorkPackageKeyDates", new
        {
            ApplicationId = applicationId,
            parameters.StartDate,
            parameters.UnsafeCladdingRemovalDate,
            parameters.ExpectedDateForCompletion
        });

        scope.Complete();
    }

    public async Task<KeyDatesResult> GetKeyDates()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<KeyDatesResult>(
            "GetWorkPackageKeyDates",
            new
            {
                ApplicationId = applicationId
            });
    }

    public async Task<KeyDatesResult> GetLatestWorkPackageKeyDatesByApplication()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<KeyDatesResult>(
            "GetLatestWorkPackageKeyDatesByApplication",
            new
            {
                ApplicationId = applicationId
            });
    }

    public async Task UpdateKeyDatesStatus(ETaskStatus taskStatus)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateWorkPackageKeyDatesStatus", new
        {
            ApplicationId = applicationId,
            TaskStatusId = taskStatus
        });

        if (taskStatus == ETaskStatus.InProgress)
        {
            await _statusTransitionService.TransitionToStatus(EApplicationStatus.WorksPackageInProgress, applicationIds: applicationId);
        }

        scope.Complete();
    }

    public async Task<ETaskStatus?> GetKeyDatesStatus()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<ETaskStatus?>("GetWorkPackageKeyDatesStatus", new
        {
            ApplicationId = applicationId
        });
    }

    #endregion

    #region Duty of Care Deed

    public async Task<DutyOfCareDeedResult> GetDutyOfCareDeed()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<DutyOfCareDeedResult>("GetWorkPackageDutyOfCareDeed", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task<ETaskStatus?> GetDutyOfCareDeedStatus()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<ETaskStatus?>("GetWorkPackageDutyOfCareDeedStatus", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task InsertDutyOfCareDeed()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        await _connection.ExecuteAsync("InsertWorkPackageDutyOfCareDeed", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task UpdateDutyOfCareDeedStatus(ETaskStatus taskStatus)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateWorkPackageDutyOfCareDeedStatus", new
        {
            ApplicationId = applicationId,
            TaskStatusId = taskStatus
        });

        scope.Complete();
    }

    #endregion

    #region Declaration

    public async Task ResetDeclaration()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("ResetWorkPackageDeclaration", new
        {
            ApplicationId = applicationId
        });

        scope.Complete();
    }

    public async Task InsertDeclaration(InsertDeclarationParameters parameters)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("InsertWorkPackageDeclaration", new
        {
            ApplicationId = applicationId,
            parameters.AllCostsReasonable,
            parameters.AllContractualRequirementsMet,
            parameters.AllCostsSetOutInFull,
            parameters.AcceptGrantAwardBasedOnCosts
        });

        await _statusTransitionService.TransitionToStatus(EApplicationStatus.WorksPackageInProgress, applicationIds: applicationId);

        scope.Complete();
    }

    public async Task UpdateDeclaration(UpdateDeclarationParameters parameters)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateWorkPackageDeclaration", new
        {
            ApplicationId = applicationId,
            parameters.AllCostsReasonable,
            parameters.AllContractualRequirementsMet,
            parameters.AllCostsSetOutInFull,
            parameters.AcceptGrantAwardBasedOnCosts
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
            "GetWorkPackageDeclaration",
            new
            {
                ApplicationId = applicationId
            });
    }

    public async Task UpdateDeclarationStatus(ETaskStatus taskStatus)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateWorkPackageDeclarationStatus", new
        {
            ApplicationId = applicationId,
            TaskStatusId = taskStatus
        });

        if (taskStatus == ETaskStatus.InProgress)
        {
            await _statusTransitionService.TransitionToStatus(EApplicationStatus.WorksPackageInProgress, applicationIds: applicationId);
        }

        scope.Complete();
    }

    public async Task<ETaskStatus?> GetDeclarationStatus()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<ETaskStatus?>("GetWorkPackageDeclarationStatus", new
        {
            ApplicationId = applicationId
        });
    }

    #endregion

    #region Project Team

    public async Task InsertTeam()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("InsertWorkPackageTeam", new
        {
            ApplicationId = applicationId
        });

        scope.Complete();
    }

    public async Task<ETaskStatus?> GetTeamStatus()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<ETaskStatus?>("GetWorkPackageTeamStatus", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task UpdateTeamStatus(ETaskStatus taskStatus)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateWorkPackageTeamStatus", new
        {
            ApplicationId = applicationId,
            TaskStatusId = taskStatus
        });

        if (taskStatus == ETaskStatus.InProgress)
        {
            await _statusTransitionService.TransitionToStatus(EApplicationStatus.WorksPackageInProgress, applicationIds: applicationId);
        }

        scope.Complete();
    }

    public async Task<List<ProjectTeamMembersResult>> GetTeamMembers()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        var results = await _connection.QueryAsync<ProjectTeamMembersResult>(
            "GetWorkPackageTeamMembers",
            new
            {
                ApplicationId = applicationId
            });

        return results.ToList();
    }

    public async Task<ProjectTeamMemberResult> GetTeamMember(Guid? teamMemberId)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        var result = await _connection.QuerySingleOrDefaultAsync<ProjectTeamMemberResult>("GetWorkPackageTeamMember",
            new
            {
                TeamMemberId = teamMemberId,
                ApplicationId = applicationId
            });

        return result;
    }

    public async Task<Guid> UpsertTeamMember(UpsertTeamMemberParameters parameters)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return default;
        }

        parameters.ApplicationId = applicationId;
        return await _connection.QuerySingleOrDefaultAsync<Guid>("UpsertWorkPackageTeamMember", parameters);
    }

    public async Task DeleteTeamMember(Guid teamMemberId)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("DeleteWorkPackageTeamMember", new
        {
            TeamMemberId = teamMemberId
        });

        scope.Complete();
    }

    public async Task<Guid?> GetLatestTeamProgressReportId()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return default;
        }

        return await _connection.QuerySingleOrDefaultAsync<Guid?>("GetLatestTeamProgressReportId", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task CopyLatestTeamFromProgressReport(Guid? progressReportId)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("CopyTeamFromProgressReport", new
        {
            ApplicationId = applicationId,
            ProgressReportId = progressReportId
        });

        scope.Complete();
    }

    public async Task<Guid?> GetWorkPackageRegulatoryCompliance()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        var result = await _connection.QuerySingleOrDefaultAsync<Guid?>(nameof(GetWorkPackageRegulatoryCompliance),
            new
            {
                ApplicationId = applicationId
            });

        return result;
    }

    public async Task UpdateWorkPackageRegulatoryCompliance(Guid teamMemberId)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        await _connection.ExecuteAsync(nameof(UpdateWorkPackageRegulatoryCompliance), new
        {
            ApplicationId = applicationId,
            TeamMemberId = teamMemberId
        });
    }

    public async Task<GetRegulatoryComplianceTeamMemberResult> GetRegulatoryComplianceTeamMember()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        var result = await _connection.QuerySingleOrDefaultAsync<GetRegulatoryComplianceTeamMemberResult>(
            nameof(GetRegulatoryComplianceTeamMember),
            new
            {
                ApplicationId = applicationId
            });

        return result;
    }

    #endregion

    #region Cost Schedule

    public async Task InsertCostsSchedule()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("InsertWorkPackageCostsSchedule", new
        {
            ApplicationId = applicationId
        });

        scope.Complete();
    }

    public async Task<ETaskStatus?> GetCostsScheduleStatus()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<ETaskStatus?>("GetWorkPackageCostsScheduleStatus", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task UpdateCostsScheduleStatus(ETaskStatus taskStatus)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateWorkPackageCostsScheduleStatus", new
        {
            ApplicationId = applicationId,
            TaskStatusId = taskStatus
        });

        if (taskStatus == ETaskStatus.InProgress)
        {
            await _statusTransitionService.TransitionToStatus(EApplicationStatus.WorksPackageInProgress, applicationIds: applicationId);
        }

        scope.Complete();
    }

    public async Task<ENoYes?> GetCostsScheduleSoughtQuotes()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<ENoYes?>("GetWorkPackageCostsScheduleSoughtQuotes", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task UpdateCostsScheduleSoughtQuotes(ENoYes? soughtQuotes)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateWorkPackageCostsScheduleSoughtQuotes", new
        {
            ApplicationId = applicationId,
            SoughtQuotes = soughtQuotes
        });

        scope.Complete();
    }

    public async Task<string> GetCostsScheduleNoQuotesReason()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<string>("GetWorkPackageCostsScheduleSoughtQuotesReason", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task UpdateCostsScheduleNoQuotesReason(string reason)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateWorkPackageCostsScheduleNoQuotesReason", new
        {
            ApplicationId = applicationId,
            SoughtQuotesReason = reason
        });

        scope.Complete();
    }

    public async Task<ENoYes?> GetCostsScheduleHasIneligibleCosts()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<ENoYes?>("GetWorkPackageCostsScheduleHasIneligibleCosts",
            new
            {
                ApplicationId = applicationId
            });
    }

    public async Task UpdateCostsScheduleHasIneligibleCosts(ENoYes? hasIneligibleCosts)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateWorkPackageCostsScheduleHasIneligibleCosts", new
        {
            ApplicationId = applicationId,
            HasIneligibleCosts = hasIneligibleCosts
        });

        scope.Complete();
    }

    public async Task<ENoYes?> GetCostsScheduleRequiresSubcontractors()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<ENoYes?>("GetWorkPackageCostsScheduleRequiresSubcontractors",
            new
            {
                ApplicationId = applicationId
            });
    }

    public async Task UpdateCostsScheduleRequiresSubcontractors(ENoYes? requiresSubcontractors)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateWorkPackageCostsScheduleRequiresSubcontractors", new
        {
            ApplicationId = applicationId,
            RequiresSubcontractors = requiresSubcontractors
        });

        scope.Complete();
    }

    public async Task<List<CostsScheduleSubcontractorResult>> GetCostsScheduleSubcontractors()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        var results = await _connection.QueryAsync<CostsScheduleSubcontractorResult>(
            "GetWorkPackageCostsScheduleSubcontractors",
            new
            {
                ApplicationId = applicationId
            });

        return results.ToList();
    }

    public async Task<CostsScheduleSubcontractorResult> GetCostsScheduleSubcontractor(Guid? subcontractorId)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        var result = await _connection.QuerySingleOrDefaultAsync<CostsScheduleSubcontractorResult>("GetWorkPackageCostsScheduleSubcontractor",
            new
            {
                SubcontractorId = subcontractorId,
                ApplicationId = applicationId
            });

        return result;
    }

    public async Task<Guid> UpsertCostsScheduleSubcontractor(UpsertCostsSchedulingSubcontractorParameters parameters)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return default;
        }

        parameters.ApplicationId = applicationId;
        return await _connection.QuerySingleOrDefaultAsync<Guid>("UpsertGetWorkPackageCostsScheduleSubcontractor", parameters);
    }

    public async Task DeleteCostsScheduleSubcontractor(Guid subcontractorId)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("DeleteWorkPackageCostsScheduleSubcontractor", new
        {
            SubcontractorId = subcontractorId
        });

        scope.Complete();
    }

    public async Task UpdateInstallationOfCladdingCosts(UpdateInstallationOfCladdingCostsParameters parameters)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        var sprocParams = new DynamicParameters(parameters);
        sprocParams.Add("@ApplicationId", applicationId);

        await _connection.ExecuteAsync(nameof(UpdateInstallationOfCladdingCosts), sprocParams);
    }

    public async Task UpdatePreliminaryCosts(UpdatePreliminaryCostsParameters parameters)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        var sprocParams = new DynamicParameters(parameters);
        sprocParams.Add("@ApplicationId", applicationId);

        await _connection.ExecuteAsync(nameof(UpdatePreliminaryCosts), sprocParams);
    }

    public async Task UpdateOtherCosts(UpdateOtherCostsParameters parameters)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        var sprocParams = new DynamicParameters(parameters);
        sprocParams.Add("@ApplicationId", applicationId);

        await _connection.ExecuteAsync(nameof(UpdateOtherCosts), sprocParams);
    }

    public async Task<GetWorkPackageCostsResult> GetWorkPackageCosts()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        var result = await _connection.QuerySingleOrDefaultAsync<GetWorkPackageCostsResult>(nameof(GetWorkPackageCosts),
            new
            {
                ApplicationId = applicationId
            });

        return result;
    }

    public async Task<GetWorkPackageCostsResult> GetWorkPackageCostsDraft()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        var result = await _connection.QuerySingleOrDefaultAsync<GetWorkPackageCostsResult>(nameof(GetWorkPackageCostsDraft),
            new
            {
                ApplicationId = applicationId
            });

        return result;
    }

    public async Task CreateWorkPackageCosts()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        await _connection.ExecuteAsync(nameof(CreateWorkPackageCosts), new
        {
            ApplicationId = applicationId
        });
    }

    public async Task UpdateUnsafeCladdingCosts(UpdateUnsafeCladdingCostsParameters parameters)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        var sprocParams = new DynamicParameters(parameters);
        sprocParams.Add("@ApplicationId", applicationId);

        await _connection.ExecuteAsync(nameof(UpdateUnsafeCladdingCosts), sprocParams);
    }

    public async Task UpdateIneligibleCosts(UpdateIneligibleCostsParameters parameters)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        var sprocParams = new DynamicParameters(parameters);
        sprocParams.Add("@ApplicationId", applicationId);

        await _connection.ExecuteAsync(nameof(UpdateIneligibleCosts), sprocParams);
    }

    #endregion

    #region Costs schedule FRAEW

    public async Task<CostsScheduleFireRiskCladdingWorksResult> GetCostsScheduleFireRiskCladdingWorks()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        var result = default(CostsScheduleFireRiskCladdingWorksResult);
        await _connection.QueryAsync<CostsScheduleFireRiskCladdingWorksResult, CostsScheduleFireRiskCladdingSystemItemResult, CostsScheduleFireRiskCladdingWorksResult>(
            "GetWorkPackageFireRiskCladdingWorks",
             (fireRiskCladdingWorksResult, item) =>
             {
                 result ??= fireRiskCladdingWorksResult;
                 if (item is not null)
                 {
                     result.CladdingWorks.Add(item);
                 }
                 return result;
             },
            new
            {
                ApplicationId = applicationId
            },
            splitOn: "FireRiskCladdingSystemsId");

        return result;
    }

    public async Task<CostsScheduleCladdingSystemResult> GetCostsScheduleCladdingSystemIsBeingRemoved(Guid? fireRiskCladdingSystemsId)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<CostsScheduleCladdingSystemResult>(
            "GetWorkPackageCostsScheduleCladdingSystemIsBeingRemoved",
            new
            {
                ApplicationId = applicationId,
                FireRiskCladdingSystemsId = fireRiskCladdingSystemsId
            });
    }

    public async Task<CostsScheduleCladdingSystemDetailsResult> GetCostsScheduleCladdingSystemDetails(Guid? fireRiskCladdingSystemsId)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<CostsScheduleCladdingSystemDetailsResult>(
            "GetWorkPackageCostsScheduleCladdingSystemDetails",
            new
            {
                ApplicationId = applicationId,
                FireRiskCladdingSystemsId = fireRiskCladdingSystemsId
            });
    }

    public async Task<CostsScheduleCladdingSystemAnswersResult> GetCostsScheduleCladdingSystemAnswers(Guid? fireRiskCladdingSystemsId)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<CostsScheduleCladdingSystemAnswersResult>(
            "GetWorkPackageCostsScheduleCladdingSystemCheckYourAnswers",
            new
            {
                ApplicationId = applicationId,
                FireRiskCladdingSystemsId = fireRiskCladdingSystemsId
            });
    }

    public async Task InsertCostsScheduleCladdingSystem(InsertCladdingSystemParameters parameters)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("InsertWorkPackageCostsScheduleCladdingSystem", new
        {
            ApplicationId = applicationId,
            parameters.FireRiskCladdingSystemsId,
            parameters.IsBeingRemoved
        });

        scope.Complete();
    }

    public async Task UpdateCostsScheduleCladdingSystemIsBeingRemoved(UpdateCladdingSystemParameters parameters)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateWorkPackageCostsScheduleCladdingSystemIsBeingRemoved", new
        {
            ApplicationId = applicationId,
            parameters.FireRiskCladdingSystemsId,
            parameters.IsBeingRemoved
        });

        scope.Complete();
    }

    public async Task UpdateCostsScheduleCladdingSystemDetails(UpdateCladdingSystemDetailsParameters parameters)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var sprocParams = new DynamicParameters(parameters);
        sprocParams.Add("@ApplicationId", applicationId);

        await _connection.ExecuteAsync("UpdateWorkPackageCostsScheduleCladdingSystemDetails",
            sprocParams);

        scope.Complete();
    }

    public async Task UpdateCostsScheduleCladdingSystemStatus(Guid fireRiskCladdingSystemsId, ETaskStatus taskStatus)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateWorkPackageCostsScheduleCladdingSystemStatus", new
        {
            ApplicationId = applicationId,
            FireRiskCladdingSystemsId = fireRiskCladdingSystemsId,
            TaskStatusId = taskStatus
        });

        scope.Complete();
    }

    public async Task ResetCladdingSystem(Guid fireRiskCladdingSystemsId)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("ResetWorkPackageCostsScheduleCladdingSystem", new
        {
            ApplicationId = applicationId,
            FireRiskCladdingSystemsId = fireRiskCladdingSystemsId
        });

        scope.Complete();
    }

    public async Task<List<CostScheduleCladdingSystemReplacementResult>> GetCladdingReplacementSystemsForApplication()
    {
        var results = await _connection.QueryAsync<CostScheduleCladdingSystemReplacementResult>("GetWorkPackageCostSchedulingApplicationCladdingDetails", new { ApplicationId = _applicationDataProvider.GetApplicationId() });

        return results.ToList();
    }

    #endregion

    #region Third Party Contribution

    public async Task<GetLatestCostScheduleResult> GetLatestCostSchedule(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        var result = await _connection.QuerySingleOrDefaultAsync<GetLatestCostScheduleResult>(nameof(GetLatestCostSchedule), parameters);
        return result;
    }

    public async Task InsertThirdPartyContributions()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("InsertWorkPackageThirdPartyContributions", new
        {
            ApplicationId = applicationId
        });

        scope.Complete();
    }

    public async Task<EThirdPartyContributionPursuitStatus?> GetThirdPartyContributionsPursuingThirdPartyContribution()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<EThirdPartyContributionPursuitStatus?>("GetWorkPackageThirdPartyContributionsPursuingThirdPartyContribution", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task UpdateThirdPartyContributionsPursuingThirdPartyContribution(EThirdPartyContributionPursuitStatus? thirdPartyContributionPursuitStatusTypeId)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateWorkPackageThirdPartyContributionsPursuingThirdPartyContribution", new
        {
            ApplicationId = applicationId,
            ThirdPartyContributionPursuitStatusTypeId = (int)thirdPartyContributionPursuitStatusTypeId
        });

        scope.Complete();
    }

    public async Task<ThirdPartyContributionResult> GetThirdPartyContributionsThirdPartyContribution()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<ThirdPartyContributionResult>("GetWorkPackageThirdPartyContributionsThirdPartyContribution", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task<List<EFundingStillPursuing>> GetThirdPartyContributionsThirdPartyContributionPursuingTypes()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        var contributionPursuingTypes = await _connection.QueryAsync<EFundingStillPursuing>("GetThirdPartyContributionsThirdPartyContributionPursuingTypes", new
        {
            ApplicationId = applicationId
        });
        return contributionPursuingTypes.ToList();
    }

    public async Task UpdateThirdPartyContributionsThirdPartyContribution(ThirdPartyContributionParameters parameters)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var p = new DynamicParameters();
        p.Add("@ApplicationId", applicationId);
        p.Add("@ContributionPursuingTypes", parameters.ContributionPursuingTypes.ToDataTable().AsTableValuedParameter("[dbo].[IntListType]"), DbType.Object);
        p.Add("@ContributionAmount", parameters.ContributionAmount);
        p.Add("@ContributionNotes", parameters.ContributionNotes);

        await _connection.ExecuteAsync("UpdateThirdPartyContributionsThirdPartyContribution", p);

        scope.Complete();
    }

    public async Task UpdateThirdPartyContributionsStatus(ETaskStatus taskStatus)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("UpdateWorkPackageThirdPartyContributionsStatus", new
        {
            ApplicationId = applicationId,
            TaskStatusId = taskStatus
        });

        if (taskStatus == ETaskStatus.InProgress)
        {
            await _statusTransitionService.TransitionToStatus(EApplicationStatus.WorksPackageInProgress, applicationIds: applicationId);
        }

        scope.Complete();
    }

    public async Task<CheckYourAnswersResult> GetWorkPackageThirdPartyContributionsCheckYourAnswers()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        var contributionTypes = new List<string>();
        var results = await _connection.QueryAsync<CheckYourAnswersResult, ContributionType, CheckYourAnswersResult>(
            "GetWorkPackageThirdPartyContributionsCheckYourAnswers",
            (checkYourAnswersResult, contributionType) =>
            {
                if (!string.IsNullOrEmpty(contributionType?.Type))
                {
                    contributionTypes.Add(contributionType.Type);
                }

                return checkYourAnswersResult;
            },
            new
            {
                ApplicationId = applicationId
            });

        var uniqueResult = results.FirstOrDefault();
        if (uniqueResult != null)
        {
            uniqueResult.ContributionTypes = contributionTypes;
        }

        return uniqueResult;
    }

    public async Task ResetThirdPartyContributions()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        await _connection.ExecuteAsync("ResetWorkPackageThirdPartyContributions", new
        {
            ApplicationId = applicationId
        });

        scope.Complete();
    }

    public async Task<ETaskStatus?> GetThirdPartyContributionsStatus()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<ETaskStatus?>("GetWorkPackageThirdPartyContributionsStatus", new
        {
            ApplicationId = applicationId
        });
    }

    #endregion

    private bool TryGetApplicationId(out Guid applicationId)
    {
        applicationId = _applicationDataProvider.GetApplicationId();

        return applicationId != default;
    }

    public async Task<Guid?> GetWorkPackageCostsDraftByApplicationId()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        var result = await _connection.QuerySingleOrDefaultAsync<Guid?>(nameof(GetWorkPackageCostsDraftByApplicationId),
        new
        {
            ApplicationId = applicationId
        });

        return result;
    }

    public async Task<GetWorkPackageCostsResult> GetWorkPackageCostsByVariationRequestId(Guid variationRequestId)
    {
        var result = await _connection.QuerySingleOrDefaultAsync<GetWorkPackageCostsResult>(nameof(GetWorkPackageCostsByVariationRequestId),
        new
        {
            VariationRequestId = variationRequestId
        });

        return result;
    }

    public async Task<Guid?> GetLatestWorkPackageCostsId()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        var result = await _connection.QuerySingleOrDefaultAsync<Guid?>(nameof(GetLatestWorkPackageCostsId),
        new
        {
            ApplicationId = applicationId
        });

        return result;
    }

    public async Task<GrantCertifyingOfficerAuthorisedSignatoriesResult> GetGrantCertifyingOfficerAuthorisedSignatories()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<GrantCertifyingOfficerAuthorisedSignatoriesResult>(
            "GetGrantCertifyingOfficerAuthorisedSignatories",
            new
            {
                ApplicationId = applicationId
            });
    }

    public async Task UpdateGrantCertifyingOfficerAuthorisedSignatories(UpdateGrantCertifyingOfficerAuthorisedSignatoriesParameters parameters)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var sprocParams = new DynamicParameters(parameters);
        sprocParams.Add("@ApplicationId", applicationId);

        await _connection.ExecuteAsync("UpdateGrantCertifyingOfficerAuthorisedSignatories",
            sprocParams);

        scope.Complete();
    }

    #region Programme Plan
    public async Task CreateWorkPackageProgrammePlan(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        await _connection.ExecuteAsync(nameof(CreateWorkPackageProgrammePlan), parameters);
    }

    public async Task SetWorkPackageProgrammePlanTaskStatus(SetWorkPackageProgrammePlanTaskStatusParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(SetWorkPackageProgrammePlanTaskStatus), parameters);
    }

    public async Task<bool?> GetHasProgrammePlan(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        var result = await _connection.QuerySingleOrDefaultAsync<bool?>(nameof(GetHasProgrammePlan), parameters);

        return result;
    }

    public async Task SetHasProgrammePlan(SetHasProgrammePlanParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(SetHasProgrammePlan), parameters);
    }

    public async Task<IReadOnlyCollection<FileResult>> GetProgrammePlanDocuments(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        var results = await _connection.QueryAsync<FileResult>(nameof(GetProgrammePlanDocuments), parameters);
        return results;
    }

    public async Task InsertProgrammePlanDocument(InsertProgrammePlanDocumentParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(InsertProgrammePlanDocument), parameters);
    }

    public async Task DeleteProgrammePlanDocument(DeleteProgrammePlanDocumentParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(DeleteProgrammePlanDocument), parameters);
    }

    public async Task<GetProgrammePlanCheckYourAnswersResult> GetProgrammePlanCheckYourAnswers(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        GetProgrammePlanCheckYourAnswersResult result = null;

        await _connection
            .QueryAsync<GetProgrammePlanCheckYourAnswersResult, FileResult, GetProgrammePlanCheckYourAnswersResult>(
                nameof(GetProgrammePlanCheckYourAnswers),
                (answers, file) =>
                {
                    result ??= answers;

                    if (file is not null)
                    {
                        result.FileNames.Add(file.Name);
                    }

                    return result;
                },
                parameters);

        return result;
    }

    #endregion
}