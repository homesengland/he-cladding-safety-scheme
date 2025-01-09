namespace HE.Remediation.Core.Data.StoredProcedureParameters
{
    public class GetTaskTypeParameters
    {
        public GetTaskTypeParameters(string parentType, string childType)
        {
            ParentType = parentType;
            ChildType = childType;
        }
        public string ParentType { get; set; }
        public string ChildType { get; set; }
    }
}
