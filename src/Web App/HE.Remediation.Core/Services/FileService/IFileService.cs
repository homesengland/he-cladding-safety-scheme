using HE.Remediation.Core.Settings;
using Microsoft.AspNetCore.Http;

namespace HE.Remediation.Core.Services.FileService
{
    public interface IFileService
    {
        Task<ProcessFileResult> ProcessFile(IFormFile file, UploadSectionSettings settings);
        Task<ProcessFileResult> ProcessPdfFile(MemoryStream fileStream, string fileName);
        Task DeleteFile(string fileName);
        Task<GetFileResult> GetFile(Guid fileId, string extension);
    }
}
