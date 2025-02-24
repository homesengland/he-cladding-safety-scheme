namespace HE.Remediation.Core.Data.StoredProcedureResults
{
    public class GetTaskResult
    {
        public Guid TaskId { get; set; }
        public string ApplicationReference { get; set; }
        public int TaskTypeId { get; set; }
        public int TaskSubTypeId { get; set; }
        public int? TopicId { get; set; }
        public string Description { get; set; }
        public DateOnly DueDate { get; set; }
        public Guid? AssigneeUserId { get; set; }
        public string AssigneeFirstName { get; set; }
        public string AssigneeLastName { get; set; }
        public string TaskStatus { get; set; }
        public int? TaskOutcomeId { get; set; }
        public string Notes { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? AssignedToTeamId { get; set; }
        public string AssignedToTeamName { get; set; }
    }
}
