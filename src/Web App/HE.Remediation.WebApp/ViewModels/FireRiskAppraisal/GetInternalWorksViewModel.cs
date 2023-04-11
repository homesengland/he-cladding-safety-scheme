namespace HE.Remediation.WebApp.ViewModels.FireRiskAppraisal;

public class GetInternalWorksViewModel
{
    public List<InternalWallWorks> WallWorks { get; set; }

    public class InternalWallWorks
    {
        public Guid Id { get; set; }  

        public string Description { get;set; }        
    }
}
