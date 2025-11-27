using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.ViewModels.MonthlyProgressReporting.KeyDates;

public class WorksPlanningViewModel
{
    public string ApplicationReferenceNumber { get; set; }
    public string BuildingName { get; set; }

    public int? ExpectedTenderMonth { get; set; }
    public int? ExpectedTenderYear { get; set; }
    public DateTime? ExpectedTenderDate => ExpectedTenderMonth is >= 1 and <= 12 
                                           && ExpectedTenderYear is >= 2000 and <= 3000
        ? new DateTime(ExpectedTenderYear.Value, ExpectedTenderMonth.Value, 1)
        : null;

    public int? ActualTenderMonth { get; set; }
    public int? ActualTenderYear { get; set; }
    public DateTime? ActualTenderDate => ActualTenderMonth is >= 1 and <= 12
                                         && ActualTenderYear is >= 2000 and <= 3000
        ? new DateTime(ActualTenderYear.Value, ActualTenderMonth.Value, 1)
        : null;

    public int? ExpectedLeadContractorAppointmentMonth { get; set; }
    public int? ExpectedLeadContractorAppointmentYear { get; set; }
    public DateTime? ExpectedLeadContractorAppointmentDate => ExpectedLeadContractorAppointmentMonth is >= 1 and <= 12
                                                              && ExpectedLeadContractorAppointmentYear is >= 2000 and <= 3000
        ? new DateTime(ExpectedLeadContractorAppointmentYear.Value, ExpectedLeadContractorAppointmentMonth.Value, 1)
        : null;

    public int? ActualLeadContractorAppointmentMonth { get; set; }
    public int? ActualLeadContractorAppointmentYear { get; set; }
    public DateTime? ActualLeadContractorAppointmentDate => ActualLeadContractorAppointmentMonth is >= 1 and <= 12
                                                            && ActualLeadContractorAppointmentYear is >= 2000 and <= 3000
        ? new DateTime(ActualLeadContractorAppointmentYear.Value, ActualLeadContractorAppointmentMonth.Value, 1)
        : null;

    public int? ExpectedWorksPackageSubmissionMonth { get; set; }
    public int? ExpectedWorksPackageSubmissionYear { get; set; }

    public DateTime? ExpectedWorksPackageSubmissionDate => ExpectedWorksPackageSubmissionMonth is >= 1 and <= 12
                                                           && ExpectedWorksPackageSubmissionYear is >= 2000 and <= 3000
        ? new DateTime(ExpectedWorksPackageSubmissionYear.Value, ExpectedWorksPackageSubmissionMonth.Value, 1)
        : null;

    public DateTime? PreviousExpectedTenderDate { get; set; }
    public DateTime? PreviousExpectedLeadContractorAppointmentDate { get; set; }
    public DateTime? PreviousActualTenderDate { get; set; }
    public DateTime? PreviousActualLeadContractorAppointmentDate { get; set; }
    public DateTime? PreviousExpectedWorksPackageSubmissionDate { get; set; }

    public ESubmitAction SubmitAction { get; set; }
}