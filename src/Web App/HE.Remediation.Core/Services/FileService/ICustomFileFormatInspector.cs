using FileSignatures;

namespace HE.Remediation.Core.Services.FileService
{
    public interface ICustomFileFormatInspector
    {
        FileFormat GetFileFormat(Stream stream, string extension);
    }
}
