using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.Shared
{
    public abstract class FileUploadViewModel
    {
	    private List<File> _addedFiles;

        public IFormFile File { get; set; }

        public List<File> AddedFiles
        {
	        get => _addedFiles;
	        set
	        {
		        _addedFiles = value.OrderBy(x => x.Name).ToList();
	        }
        }

        public abstract string DeleteEndpoint { get; }
        public abstract string[] AcceptedFileTypes { get; }
        public ESubmitAction SubmitAction { get; set; }
        public abstract int NumberOfFilesAllowed { get; }
        public Dictionary<string,string> DeleteParameters { get; set; }
    }
}
