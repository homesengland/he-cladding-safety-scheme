using FileSignatures;
using System.Reflection;

namespace HE.Remediation.Core.Services.FileService
{
    public class CustomFileFormatInspector: FileFormatInspector, ICustomFileFormatInspector
    {
        private readonly IEnumerable<FileFormat> _formats;

        public CustomFileFormatInspector() : base()
        {
            _formats = FileFormatLocator.GetFormats(Assembly.GetExecutingAssembly(), true);
        }

        public FileFormat GetFileFormat(Stream stream, string extension)
        {
            var formats = _formats.Where(x => x.Extension.ToLower() == extension.ToLower());

            foreach (var format in formats)
            {
                var isMatch = format.IsMatch(stream);

                if (isMatch)
                    return format;
            }

            return null;
        }
    }
}
