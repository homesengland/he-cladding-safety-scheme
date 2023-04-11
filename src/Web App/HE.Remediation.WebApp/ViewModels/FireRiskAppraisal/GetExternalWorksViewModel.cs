
namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal;

public class GetExternalWorksViewModel
{
    public List<ExternalWallWorks> WallWorks { get; set; }

    public class ExternalWallWorks
    {
        public Guid? Id { get; set; }  

        public string Description { get;set; }        
    }
}
