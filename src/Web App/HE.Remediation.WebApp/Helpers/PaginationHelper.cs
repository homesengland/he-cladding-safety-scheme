using HE.Remediation.WebApp.ViewModels;

namespace HE.Remediation.WebApp.Helpers
{
    public static class PaginationHelper
    {
        public static PagnationRangeValuesViewModel ObtainPageHandlingDetails(int CurrentPage, int NoOfRecords,
                                                                              int EntriesPerPage, bool ShowAllRecords)
        {
            PagnationRangeValuesViewModel pagnationRangeValuesModel = new PagnationRangeValuesViewModel();

            pagnationRangeValuesModel.NoOfRecords = NoOfRecords;
            
            int NoOfPages = (int)Math.Ceiling((double)NoOfRecords / (double)EntriesPerPage);   
            if (CurrentPage > NoOfPages)
            {
                CurrentPage = 1;
            }
            int StartRecordValue = (CurrentPage - 1) * EntriesPerPage;

            int MaxPageDisplayed = CurrentPage + 9;
            if ((CurrentPage + 9) > NoOfPages)
            {
                MaxPageDisplayed = NoOfPages;
            }

            int EndRecordValue = (StartRecordValue + EntriesPerPage) - 1;
            if (CurrentPage == MaxPageDisplayed)
            {
                EndRecordValue = NoOfRecords - 1;
            }

            if (ShowAllRecords)
            {
                EndRecordValue = NoOfRecords;
            }

            pagnationRangeValuesModel.CurrentPage = CurrentPage;
            pagnationRangeValuesModel.StartRecordValue = StartRecordValue;
            pagnationRangeValuesModel.EndRecordValue = EndRecordValue;
            pagnationRangeValuesModel.NoOfPages = NoOfPages;
            pagnationRangeValuesModel.UseEllipses = (NoOfPages > 9);
                       
            return pagnationRangeValuesModel;
        }
    }
    
}
