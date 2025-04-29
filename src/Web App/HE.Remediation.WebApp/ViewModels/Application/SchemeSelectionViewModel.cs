using HE.Remediation.Core.UseCase.Areas.Application.Dashboard.SchemeSelection;

namespace HE.Remediation.WebApp.ViewModels.Application
{
    public class SchemeSelectionViewModel
    {
        public IList<ApplicationScheme> Schemes { get; set; }
        public int? SelectedSchemeId { get; set; }
        public string ReturnUrl { get; set; }
    }
}
