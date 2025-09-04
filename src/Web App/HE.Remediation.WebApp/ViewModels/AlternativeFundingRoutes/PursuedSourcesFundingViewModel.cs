using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.AlternativeFundingRoutes
{
    public class PursuedSourcesFundingViewModel
    {
        public EPursuedSourcesFundingType? PursuedSourcesFunding { get; set; }
        public bool VisitedCheckYourAnswers { get; set; }

        public ESubmitAction SubmitAction { get; set; }
    }
}
