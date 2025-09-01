using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.ClosingReport;

namespace HE.Remediation.WebApp.Tests.ViewModels
{
    public class TaskListViewModelTests
    {
        [Fact]
        public void DisplayStatus_Section1Task_ReturnsStatus()
        {
            // Arrange
            var task = EClosingReportTask.FireRiskAssessment;
            var vm = new TaskListViewModel
            {
                TasksWithStatuses = new List<TaskListViewModel.TaskWithStatus>
                {
                    new(task, ETaskStatus.Completed)
                }
            };

            // Act
            var result = vm.DisplayStatus(task);

            // Assert
            Assert.Equal(ETaskStatus.Completed, result);
        }

        [Fact]
        public void DisplayStatus_Section2_SubmitPaymentRequest_ReturnsNull_IfSection1NotComplete()
        {
            // Arrange
            var vm = new TaskListViewModel
            {
                TasksWithStatuses = new List<TaskListViewModel.TaskWithStatus>
                {
                    new(EClosingReportTask.FireRiskAssessment, ETaskStatus.InProgress)
                }
            };

            // Act
            var result = vm.DisplayStatus(EClosingReportTask.SubmitPaymentRequest);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void DisplayStatus_Section2_SubmitPaymentRequest_ReturnsStatus_IfSection1Complete()
        {
            // Arrange
            var vm = new TaskListViewModel
            {
                TasksWithStatuses = new List<TaskListViewModel.TaskWithStatus>
                {
                    new(EClosingReportTask.FireRiskAssessment, ETaskStatus.Completed),
                    new(EClosingReportTask.PracticalCompletionCertificate, ETaskStatus.Completed),
                    new(EClosingReportTask.BuildingControlEvidence, ETaskStatus.Completed),
                    new(EClosingReportTask.CommunicationWithLeaseholders, ETaskStatus.Completed),
                    new(EClosingReportTask.BuildingInsuranceInformation, ETaskStatus.Completed),
                    new(EClosingReportTask.EvidenceOfThirdPartyContribution, ETaskStatus.Completed),
                    new(EClosingReportTask.RatingsForContractors, ETaskStatus.Completed),
                    new(EClosingReportTask.SubmitPaymentRequest, ETaskStatus.InProgress)
                }
            };

            // Act
            var result = vm.DisplayStatus(EClosingReportTask.SubmitPaymentRequest);

            // Assert
            Assert.Equal(ETaskStatus.InProgress, result);
        }

        [Fact]
        public void DisplayStatus_Section2_UploadFinalCostReport_ReturnsNull_IfPreviousTaskNotComplete()
        {
            // Arrange
            var vm = new TaskListViewModel
            {
                TasksWithStatuses = new List<TaskListViewModel.TaskWithStatus>
                {
                    // All section 1 complete
                    new(EClosingReportTask.FireRiskAssessment, ETaskStatus.Completed),
                    new(EClosingReportTask.PracticalCompletionCertificate, ETaskStatus.Completed),
                    new(EClosingReportTask.BuildingControlEvidence, ETaskStatus.Completed),
                    new(EClosingReportTask.CommunicationWithLeaseholders, ETaskStatus.Completed),
                    new(EClosingReportTask.BuildingInsuranceInformation, ETaskStatus.Completed),
                    new(EClosingReportTask.EvidenceOfThirdPartyContribution, ETaskStatus.Completed),
                    new(EClosingReportTask.RatingsForContractors, ETaskStatus.Completed),
                    // Previous section 2 task not complete
                    new(EClosingReportTask.SubmitPaymentRequest, ETaskStatus.InProgress)
                }
            };

            // Act
            var result = vm.DisplayStatus(EClosingReportTask.UploadFinalCostReport);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void DisplayStatus_Section3_FinalPaymentDeclaration_ReturnsStatus_IfSection2Complete()
        {
            // Arrange
            var vm = new TaskListViewModel
            {
                TasksWithStatuses = new List<TaskListViewModel.TaskWithStatus>
                {
                    // All section 1 complete
                    new(EClosingReportTask.FireRiskAssessment, ETaskStatus.Completed),
                    new(EClosingReportTask.PracticalCompletionCertificate, ETaskStatus.Completed),
                    new(EClosingReportTask.BuildingControlEvidence, ETaskStatus.Completed),
                    new(EClosingReportTask.CommunicationWithLeaseholders, ETaskStatus.Completed),
                    new(EClosingReportTask.BuildingInsuranceInformation, ETaskStatus.Completed),
                    new(EClosingReportTask.EvidenceOfThirdPartyContribution, ETaskStatus.Completed),
                    new(EClosingReportTask.RatingsForContractors, ETaskStatus.Completed),
                    // All section 2 complete
                    new(EClosingReportTask.SubmitPaymentRequest, ETaskStatus.Completed),
                    new(EClosingReportTask.UploadFinalCostReport, ETaskStatus.Completed),
                    // Section 3 task
                    new(EClosingReportTask.FinalPaymentDeclaration, ETaskStatus.NotStarted)
                }
            };

            // Act
            var result = vm.DisplayStatus(EClosingReportTask.FinalPaymentDeclaration);

            // Assert
            Assert.Equal(ETaskStatus.NotStarted, result);
        }

        [Fact]
        public void DisplayStatus_Section3_FinalPaymentDeclaration_ReturnsNull_IfSection2NotComplete()
        {
            // Arrange
            var vm = new TaskListViewModel
            {
                TasksWithStatuses = new List<TaskListViewModel.TaskWithStatus>
                {
                    // All section 1 complete
                    new(EClosingReportTask.FireRiskAssessment, ETaskStatus.Completed),
                    new(EClosingReportTask.PracticalCompletionCertificate, ETaskStatus.Completed),
                    new(EClosingReportTask.BuildingControlEvidence, ETaskStatus.Completed),
                    new(EClosingReportTask.CommunicationWithLeaseholders, ETaskStatus.Completed),
                    new(EClosingReportTask.BuildingInsuranceInformation, ETaskStatus.Completed),
                    new(EClosingReportTask.EvidenceOfThirdPartyContribution, ETaskStatus.Completed),
                    new(EClosingReportTask.RatingsForContractors, ETaskStatus.Completed),
                    // Section 2 not complete
                    new(EClosingReportTask.SubmitPaymentRequest, ETaskStatus.Completed),
                    new(EClosingReportTask.UploadFinalCostReport, ETaskStatus.InProgress)
                }
            };

            // Act
            var result = vm.DisplayStatus(EClosingReportTask.FinalPaymentDeclaration);

            // Assert
            Assert.Null(result);
        }
    }
}
