using System.Transactions;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.UseCase.DataIngest.RAS.DataImporters;
using HE.Remediation.Core.UseCase.DataIngest.RAS.Lookups;
using HE.Remediation.Core.UseCase.DataIngest.RAS.Validation;
using Mediator;

namespace HE.Remediation.Core.UseCase.DataIngest.RAS;

public class DataIngestHandler(
    JsonDataIngestMapperIValidator validator,
    IDataIngestionLookupService dataIngestionLookupService,
    IBuildingDetailsDataImporter buildingDetailsDataImporter,
    IResponsibleEntityDataImporter responsibleEntityDataImporter,
    IFraDataImporter fraDataImporter,
    IFraewDataImporter fraewDataImporter,
    IDataIngestionRepository dataIngestionRepository) : IRequestHandler<CreateImportRequest>
{
    private readonly JsonDataIngestMapperIValidator _validator = validator;
    private readonly IDataIngestionLookupService _dataIngestionLookupService = dataIngestionLookupService;
    private readonly IDataIngestionRepository _dataIngestionRepository = dataIngestionRepository;
    private readonly IBuildingDetailsDataImporter _buildingDetailsDataImporter = buildingDetailsDataImporter;
    private readonly IResponsibleEntityDataImporter _responsibleEntityDataImporter = responsibleEntityDataImporter;
    private readonly IFraDataImporter _fraDataImporter = fraDataImporter;
    private readonly IFraewDataImporter _fraewDataImporter = fraewDataImporter;

    public async ValueTask<Unit> Handle(CreateImportRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        // MAPPING

        ImportedDataRow importedDataRow;
        try
        {
            importedDataRow = new ImportedDataRow(request.ImportData);
        }
        catch (Exception ex)
        {
            await SaveErrorToUnprocessedRow(request.UnProcessedRowId, "Mapping", ex.Message);
            throw new DataImportException(ex.Message);
        }

        // VALIDATION

        var validation = await _validator.ValidateAsync(importedDataRow, cancellationToken);
        if (!validation.IsValid)
        {
            var validationErrorMessages = string.Join("; ", validation.Errors.Select(e => e.ErrorMessage));
            await SaveErrorToUnprocessedRow(request.UnProcessedRowId, "Validation", validationErrorMessages);
            throw new DataImportException(validationErrorMessages);
        }

        // LOOKUPS (applicant, applicant company, local authority etc)

        DataIngestionLookupService.LookupData lookups;
        try
        {
            lookups = await _dataIngestionLookupService.GetLookups(importedDataRow);
        }
        catch (Exception ex)
        {
            await SaveErrorToUnprocessedRow(request.UnProcessedRowId, "Lookups", ex.Message);
            throw;
        }

        // IMPORT

        var applicationScheme = request.TargetScheme;

        try
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var applicationId = await _buildingDetailsDataImporter.Process(importedDataRow, applicationScheme, lookups.ApplicantUserId, lookups);
            await _responsibleEntityDataImporter.Process(importedDataRow, applicationScheme, applicationId);
            await _fraDataImporter.Process(importedDataRow, applicationScheme, applicationId, lookups);
            await _fraewDataImporter.Process(importedDataRow, applicationScheme, applicationId, lookups);

            await CompleteSuccessfulImport(request.DataIngestionId, request.UnProcessedRowId, applicationId, lookups.ApplicantCompanyRegistration);
            transaction.Complete();
        }
        catch (Exception ex)
        {
            string columnName = ex is DataImportException dex ? dex.ColumnName : "GeneralError";
            await SaveErrorToUnprocessedRow(request.UnProcessedRowId, columnName, ex.Message);
            throw;
        }
        
        return Unit.Value;
    }

    private async Task SaveErrorToUnprocessedRow(Guid rowId, string columnName, string errorMessage)
    {
        await _dataIngestionRepository.UpdateUnProcessedRowWithErrorDetails(rowId, new Dictionary<string, string>() { { columnName, errorMessage } });
    }

    private async Task CompleteSuccessfulImport(Guid dataIngestionId, Guid unprocessedRowId, Guid applicationId, string organisationRegistrationNumber)
    {
        await _dataIngestionRepository.DeleteUnProcessedRow(unprocessedRowId);
        await _dataIngestionRepository.LinkImportToApplication(dataIngestionId, applicationId, organisationRegistrationNumber);
    }
}