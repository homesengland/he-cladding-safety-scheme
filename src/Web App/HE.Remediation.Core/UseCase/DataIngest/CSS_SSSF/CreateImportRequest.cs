using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.DataIngest.CSS_SSSF;

public class CreateImportRequest(Dictionary<string, string> importData, Guid unProcessedRowId, Guid dataIngestionId, EApplicationScheme targetScheme) : IRequest<Unit>
{
    public Dictionary<string, string> ImportData { get; } = importData;
    public Guid UnProcessedRowId { get; } = unProcessedRowId;
    public Guid DataIngestionId { get; } = dataIngestionId;
    public EApplicationScheme TargetScheme { get; } = targetScheme;
}