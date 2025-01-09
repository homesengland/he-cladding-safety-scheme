using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Data.StoredProcedureResults.Costs;
using HE.Remediation.Core.Data.StoredProcedureParameters.ScheduleOfWorks;
using HE.Remediation.Core.Data.StoredProcedureResults.ScheduleOfWorks;
using Dapper;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Extensions;

namespace HE.Remediation.Core.Data.Repositories;

public class ScheduleOfWorksRepository : IScheduleOfWorksRepository
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;

    public ScheduleOfWorksRepository(IDbConnectionWrapper connection,
        IApplicationDataProvider applicationDataProvider)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
    }
    public async Task<bool> HasScheduleOfWorks()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return false;
        }

        return await _connection.QuerySingleOrDefaultAsync<bool>("HasScheduleOfWorks", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task<bool> IsScheduleOfWorksApproved()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return false;
        }

        return await _connection.QuerySingleOrDefaultAsync<bool>("IsScheduleOfWorksApproved", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task<bool> IsScheduleOfWorksSubmitted()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return false;
        }

        return await _connection.QuerySingleOrDefaultAsync<bool>("GetScheduleOfWorksIsSubmitted", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task InsertScheduleOfWorks()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        await _connection.ExecuteAsync("InsertScheduleOfWorks", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task ResetScheduleOfWorks()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        await _connection.ExecuteAsync("ResetScheduleOfWorks", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task SubmitScheduleOfWorks()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        await _connection.ExecuteAsync("SubmitScheduleOfWorks", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task<IReadOnlyCollection<FileResult>> GetContracts()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return new List<FileResult>();
        }

        return await _connection.QueryAsync<FileResult>("GetScheduleOfWorksContracts", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task<IReadOnlyCollection<FileResult>> GetScheduleOfWorksBuildingControlFiles(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        var results = await _connection.QueryAsync<FileResult>(nameof(GetScheduleOfWorksBuildingControlFiles), parameters);
        return results;
    }

    public async Task<IReadOnlyCollection<FileResult>> GetScheduleOfWorksLeaseholderEngagementFiles(Guid applicationId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@ApplicationId", applicationId);

        var results = await _connection.QueryAsync<FileResult>(nameof(GetScheduleOfWorksLeaseholderEngagementFiles), parameters);
        return results;
    }

    public async Task<IReadOnlyCollection<string>> GetContractFileNames()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return new List<string>();
        }

        return await _connection.QueryAsync<string>("GetScheduleOfWorksContractFileNames", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task InsertContract(Guid fileId)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        await _connection.ExecuteAsync("InsertScheduleOfWorksContract", new
        {
            ApplicationId = applicationId,
            FileId = fileId
        });
    }

    public async Task InsertBuildingControlFile(InsertBuildingControlFileParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(InsertBuildingControlFile), parameters);
    }

    public async Task InsertLeaseholderEngagement(InsertLeaseholderEngagementParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(InsertLeaseholderEngagement), parameters);
    }

    public async Task DeleteContract(Guid fileId)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        await _connection.ExecuteAsync("DeleteScheduleOfWorksContract", new
        {
            ApplicationId = applicationId,
            FileId = fileId
        });
    }

    public async Task DeleteBuildingControlFile(DeleteBuildingControlFileParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(DeleteBuildingControlFile), parameters);
    }

    public async Task DeleteLeaseholderEngagement(DeleteLeaseholderEngagementParameters parameters)
    {
        await _connection.ExecuteAsync(nameof(DeleteLeaseholderEngagement), parameters);
    }

    public async Task CreateCosts()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        await _connection.ExecuteAsync("CreateScheduleOfWorksCosts", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task<CostsResult> GetCosts()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        var result = default(CostsResult);

        await _connection.QueryAsync<CostsResult, MonthlyCostResult, CostsResult>(
            "GetScheduleOfWorksCosts",
            (costsResult, item) =>
            {
                result ??= costsResult;
                if (item is not null)
                {
                    result.MonthlyCosts.Add(item);
                }
                return result;
            },
            new
            {
                ApplicationId = applicationId
            });

        return result;
    }

    public async Task DeleteCosts()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        await _connection.ExecuteAsync("DeleteScheduleOfWorksCosts", new
        {
            ApplicationId = applicationId
        });
    }

    public async Task UpdateCosts(IEnumerable<UpdateCostParameters> parameters)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        var costsParameters = parameters
            .Where(x => x.Id is not null && x.Date is not null)
            .Select(x => new
            {
                Id = x.Id.Value,
                Date = x.Date.Value,
                Amount = x.Amount ?? 0,
                AmountIsNull = x.Amount is null
            })
            .ToDataTable()
            .AsTableValuedParameter("[dbo].[CostListType]");

        await _connection.ExecuteAsync("UpdateScheduleOfWorksCosts", new
        {
            ApplicationId = applicationId,
            Costs = costsParameters
        });
    }

    public async Task<DeclarationResult> GetDeclaration()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<DeclarationResult>(
            "GetScheduleOfWorksDeclaration",
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

        await _connection.ExecuteAsync("UpdateScheduleOfWorksDeclaration", new
        {
            ApplicationId = applicationId,
            parameters.ConfirmedAwareOfProcess,
            parameters.ConfirmedAwareOfVariationApproval,
            parameters.ConfirmedAccuratelyProfiledCosts
        });
    }

    public async Task<ProjectDatesResult> GetProjectDates()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<ProjectDatesResult>(
            "GetScheduleOfWorksProjectDates",
            new
            {
                ApplicationId = applicationId
            });
    }

    public async Task UpdateProjectDates(UpdateProjectDatesParameters parameters)
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return;
        }

        await _connection.ExecuteAsync("UpdateScheduleOfWorksProjectDates", new
        {
            ApplicationId = applicationId,
            parameters.ProjectStartDate,
            parameters.ProjectEndDate
        });
    }

    public async Task<AnswersResult> GetCheckYourAnswers()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<AnswersResult>(
            "GetScheduleOfWorksCheckYourAnswers",
            new
            {
                ApplicationId = applicationId
            });
    }

    public async Task<IReadOnlyCollection<CostsProfileResult>> GetCostsProfile()
    {
        if (!TryGetApplicationAndScheduleOfWorksIds(out var applicationId, out var scheduleOfWorksId))
        {
            return null;
        }

        return await _connection.QueryAsync<CostsProfileResult>(
            "GetApplicationScheduleOfWorksCosts",
            new
            {
                ApplicationId = applicationId,
                ScheduleOfWorksId = scheduleOfWorksId
            });
    }

    public async Task<OverviewResult> GetOverview()
    {
        if (!TryGetApplicationAndScheduleOfWorksIds(out var applicationId, out var scheduleOfWorksId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<OverviewResult>(
            "GetApplicationScheduleOfWorksOverview",
            new
            {
                ApplicationId = applicationId,
                ScheduleOfWorksId = scheduleOfWorksId
            });
    }

    public async Task<OverviewResult> GetApprovedScheduleOfWorksOverview()
    {
        if (!TryGetApplicationId(out var applicationId))
        {
            return null;
        }

        return await _connection.QuerySingleOrDefaultAsync<OverviewResult>(
            nameof(GetApprovedScheduleOfWorksOverview),
            new
            {
                ApplicationId = applicationId
            });
    }

    public async Task<IReadOnlyCollection<CostsProfileResult>> GetApprovedApplicationScheduleOfWorksCosts()
    {
        if (!TryGetApplicationAndScheduleOfWorksIds(out var applicationId, out var scheduleOfWorksId))
        {
            return null;
        }

        return await _connection.QueryAsync<CostsProfileResult>(
            nameof(GetApprovedApplicationScheduleOfWorksCosts),
            new
            {
                ApplicationId = applicationId,
                ScheduleOfWorksId = scheduleOfWorksId
            });
    }

    private bool TryGetApplicationId(out Guid applicationId)
    {
        applicationId = _applicationDataProvider.GetApplicationId();

        return applicationId != default;
    }

    private bool TryGetApplicationAndScheduleOfWorksIds(out Guid applicationId, out Guid scheduleOfWorksId)
    {
        if (!TryGetApplicationId(out applicationId))
        {
            scheduleOfWorksId = default;
            return false;
        }

        scheduleOfWorksId = _connection.QuerySingleOrDefaultAsync<Guid?>("GetScheduleOfWorksIdForApplication", new
        {
            ApplicationId = applicationId
        }).GetAwaiter().GetResult()
        ?? default;

        return scheduleOfWorksId != default;
    }
}
