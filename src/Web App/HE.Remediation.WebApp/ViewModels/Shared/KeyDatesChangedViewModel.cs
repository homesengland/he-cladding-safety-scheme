using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.Shared;

public abstract class KeyDatesChangedViewModel
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public int? ChangeTypeId { get; set; }
    public string ChangeReason { get; set; }
    public IList<ChangeTypeViewModel> ChangeTypes { get; set; } = new List<ChangeTypeViewModel>();

    public ESubmitAction SubmitAction { get; set; }

    public class ChangeTypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}