using MediatR;

namespace HE.Remediation.Core.UseCase.DataIngest
{
    public class CreateImportRequest: IRequest<Unit>
    {
        public Dictionary<string, string> ImportData { get; set; }
        public Guid UnProcessedRowId { get; set; }
        public Guid DataIngestionId { get; set; }
    }
}