using HE.Remediation.WebApp.ViewModels;

namespace HE.Remediation.WebApp.Helpers;

public static class PaginationHelper
{
    public static PagnationRangeValuesViewModel ObtainPageHandlingDetails(int CurrentPage, int NoOfRecords, int EntriesPerPage, bool ShowAllRecords)
    {
        var pagnationRangeValuesModel = new PagnationRangeValuesViewModel
        {
            NoOfRecords = NoOfRecords
        };

        var NoOfPages = (int)Math.Ceiling(NoOfRecords / (double)EntriesPerPage);   
        if (CurrentPage > NoOfPages)
        {
            CurrentPage = 1;
        }
        var StartRecordValue = (CurrentPage - 1) * EntriesPerPage;

        var MaxPageDisplayed = CurrentPage + 9;
        if ((CurrentPage + 9) > NoOfPages)
        {
            MaxPageDisplayed = NoOfPages;
        }

        var EndRecordValue = (StartRecordValue + EntriesPerPage) - 1;
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