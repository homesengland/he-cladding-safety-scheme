namespace HE.Remediation.Core.Data.StoredProcedureParameters
{
    public class InsertTaskParameters
    {
        public Guid ReferenceId { get; set; }
        public string Description { get; set; }
        public int TaskTypeId { get; set; }
        public int? TopicId { get; set; }
        public DateOnly RequiredByDate { get; set; }
        public string TaskStatus { get; set; }
        public Guid? AssignedToUserId { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public int? AssignedToTeamId { get; set; }
        public string Notes { get; set; }
    }
}
