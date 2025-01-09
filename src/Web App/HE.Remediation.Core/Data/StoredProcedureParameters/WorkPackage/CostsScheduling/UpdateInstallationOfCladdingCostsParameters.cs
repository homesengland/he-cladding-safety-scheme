namespace HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.CostsScheduling;

public class UpdateInstallationOfCladdingCostsParameters
{
    public decimal? NewCladdingAmount { get; set; }
    public string NewCladdingDescription { get; set; }
    public decimal? ExternalWorksAmount { get; set; }
    public string ExternalWorksDescription { get; set; }
    public decimal? InternalWorksAmount { get; set; }
    public string InternalWorksDescription { get; set; }
}