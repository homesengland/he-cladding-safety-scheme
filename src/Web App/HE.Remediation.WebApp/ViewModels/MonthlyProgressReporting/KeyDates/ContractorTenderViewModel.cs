using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates;
public class ContractorTenderViewModel
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }
    public ESubmitAction SubmitAction { get; set; }

    public EContractorTenderType? ContractorTenderType { get; set; }
}