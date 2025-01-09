using FileSignatures;
using HE.Remediation.Core.Settings;

namespace HE.Remediation.Core.Services.FileService
{
    public interface ICustomFileFormatInspector
    {
        FileFormat GetFileFormat(Stream stream, string extension, UploadSectionSettings settings);
    }
}
