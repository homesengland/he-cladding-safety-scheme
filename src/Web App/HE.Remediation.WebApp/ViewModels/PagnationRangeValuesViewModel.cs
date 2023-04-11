namespace HE.Remediation.WebApp.ViewModels
{
    public class PagnationRangeValuesViewModel
    {
        public int StartRecordValue { get; set; }
        public int EndRecordValue { get; set; }
        public int NoOfPages { get; set; }
        public int NoOfRecords { get; set; }

        public int CurrentPage { get; set; }

        public bool UseEllipses { get; set; }
        
        public PagnationRangeValuesViewModel()
        {
            StartRecordValue = 0;
            EndRecordValue = 0;
            NoOfPages = 0;
            NoOfRecords = 0;
            CurrentPage = 1;
            UseEllipses = false;            
        }
    }
}
