using HE.Remediation.Core.Enums;
using HE.Remediation.WebApp.ViewModels.Application;

namespace HE.Remediation.WebApp.Tests.ViewModels
{
    public class StageDiagramViewModelTests
    {
        [Theory]
        [InlineData(EApplicationScheme.CladdingSafetyScheme, false, false)]
        [InlineData(EApplicationScheme.CladdingSafetyScheme, true, true)]
        [InlineData(EApplicationScheme.SocialSector, false, true)]
        [InlineData(EApplicationScheme.SocialSector, true, true)]
        public void DisplayWorksPackage_HasProgressReports_ReturnsExpected(EApplicationScheme applicationScheme, bool hasProgressReports, bool expected)
        {
            // Arrange
            var vm = new StageDiagramViewModel
            {
                Stage = EApplicationStage.WorksPackage,
                HasWorkPackage = true,
                IsWorkPackageSubmitted = false,
                ApplicationScheme = applicationScheme,
                HasProgressReports = hasProgressReports
            };

            // Act
            var result = vm.DisplayWorksPackage();

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(EApplicationStage.ApplyForGrant, EApplicationStatus.ApplicationInReview, false)]
        [InlineData(EApplicationStage.ApplyForGrant, EApplicationStatus.ApplicationSubmitted, true)]
        [InlineData(EApplicationStage.WorksPackage, EApplicationStatus.ProgressReporting, true)]
        [InlineData(EApplicationStage.WorksPackage, EApplicationStatus.WorksPackageInProgress, true)]
        public void DisplayWorksPackage_StageForSssf_ReturnsExpected(EApplicationStage applicationStage, EApplicationStatus applicationStatus, bool expected)
        {
            // Arrange
            var vm = new StageDiagramViewModel
            {
                Stage = applicationStage,
                Status = applicationStatus,
                HasWorkPackage = true,
                IsWorkPackageSubmitted = false,
                ApplicationScheme = EApplicationScheme.SocialSector,
                HasProgressReports = false
            };

            // Act
            var result = vm.DisplayWorksPackage();

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(EApplicationScheme.CladdingSafetyScheme, true, true)]
        [InlineData(EApplicationScheme.CladdingSafetyScheme, false, false)]
        [InlineData(EApplicationScheme.SocialSector, true, true)]
        [InlineData(EApplicationScheme.SocialSector, false, false)]
        public void DisplayWorksPackage_ActiveWorksPackage_ReturnsExpected(EApplicationScheme scheme, bool hasWorksPackage, bool expected)
        {
            // Arrange
            var vm = new StageDiagramViewModel
            {
                Stage = EApplicationStage.WorksPackage,
                Status = EApplicationStatus.WorksPackageInProgress,
                HasWorkPackage = hasWorksPackage,
                IsWorkPackageSubmitted = false,
                ApplicationScheme = scheme,
                HasProgressReports = true
            };

            // Act
            var result = vm.DisplayWorksPackage();

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
