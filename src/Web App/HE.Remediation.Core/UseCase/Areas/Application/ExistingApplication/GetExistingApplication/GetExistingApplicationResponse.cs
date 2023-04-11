using HE.Remediation.Core.Enums;

namespace HE.Remediation.Core.UseCase.Areas.Application.ExistingApplication.GetExistingApplication
{     
    public class GetExistingApplicationResponse
    {
        public Guid ApplicationId { get; set; }

        public string ApplicationNumber { get; set; }

        public string UniqueBuildingName { get; set; }

        public DateTime DateCreated { get; set; }

        public EApplicationStage Stage { get; set; }

        public EApplicationStatus Status { get; set; }

        public bool OpenTasks { get; set; }
    }
}
