namespace HE.Remediation.Core.Services.Alert.Models;

public class AlertViewModel
{
    public Guid AlertId { get; set; }
    public string Title { get; set; }
    public bool IsAcknowledged { get; set; }
    public DateTime DateCreated { get; set; }
}