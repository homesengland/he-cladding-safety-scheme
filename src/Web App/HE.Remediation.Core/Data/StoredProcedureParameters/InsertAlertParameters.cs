namespace HE.Remediation.Core.Data.StoredProcedureParameters
{
    public class InsertAlertParameters
    {
        public Guid ApplicationId { get; set; }
        public int AlertTypeId { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
