using FileSignatures;
using HE.Remediation.Core.Settings;

namespace HE.Remediation.Core.Services.FileService
{
    public class CustomFileFormatInspector : ICustomFileFormatInspector
    {
        private readonly IFileFormatInspector _fileFormatInspector;

        public CustomFileFormatInspector(IFileFormatInspector fileFormatInspector)
        {
            _fileFormatInspector = fileFormatInspector;
        }

        public FileFormat GetFileFormat(Stream stream, string extension, UploadSectionSettings settings)
        {
            var format = _fileFormatInspector.DetermineFileFormat(stream);

            var ext = $"{format?.Extension.ToLower()}";

            if (IsValidExtension(ext, settings))
            {
                return format;
            };

            return null;
        }

        public bool IsValidExtension(string ext, UploadSectionSettings settings)
        {
            return settings.AcceptedFileTypes.Contains(ext.ToLower()) ? true : false;
        }
    }
}
