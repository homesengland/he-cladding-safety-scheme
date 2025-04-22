using Microsoft.AspNetCore.Razor.TagHelpers;
using HE.Remediation.WebApp.TagHelpers;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.Tests.TagHelpers
{
    public class ApplicationStagesListTagHelperTests
    {
        [Fact]
        public void RenderStagesList_ShouldGenerateCorrectHtmlContent_WhenGivenCurrentStage()
        {
            // Arrange
            var tagHelper = new ApplicationStagesListTagHelper();
            var currentStage = EApplicationStage.WorksDelivery;

            // Act
            var result = tagHelper.RenderStagesList(currentStage);

            // Assert
            Assert.Contains("govuk-stage-list-item-current", result);
            Assert.Contains("<span class=\"govuk-stage-list-item-text\">Works delivery</span>", result);
        }

        [Theory]
        [InlineData(EApplicationScheme.CladdingSafetyScheme)]
        [InlineData(EApplicationScheme.SocialSector)]
        [InlineData(EApplicationScheme.ResponsibleActorsScheme)]
        [InlineData(EApplicationScheme.SelfRemediating)]
        public void RenderStagesList_ShouldExcludeStates_WhenStatesAreExcluded(EApplicationScheme applicationScheme)
        {
            // Arrange
            var tagHelper = new ApplicationStagesListTagHelper();
            tagHelper.ApplicationScheme = applicationScheme;
            var currentStage = EApplicationStage.WorksCompleted;

            // Act
            var result = tagHelper.RenderStagesList(currentStage);

            // Assert
            Assert.DoesNotContain("Building complete", result);
            Assert.DoesNotContain("Counter sign in progress", result);

            if (applicationScheme != EApplicationScheme.CladdingSafetyScheme) {
                Assert.DoesNotContain("Grant funding agreement", result);
            }
        }

        [Theory]
        [InlineData(EApplicationScheme.CladdingSafetyScheme)]
        [InlineData(EApplicationScheme.SocialSector)]
        [InlineData(EApplicationScheme.ResponsibleActorsScheme)]
        [InlineData(EApplicationScheme.SelfRemediating)]
        public void RenderStagesList_ShouldContainSpecificText_WhenSchemesAreSet(EApplicationScheme applicationScheme)
        {
            // Arrange
            var tagHelper = new ApplicationStagesListTagHelper();
            tagHelper.ApplicationScheme = applicationScheme;
            var currentStage = EApplicationStage.WorksCompleted;

            // Act
            var result = tagHelper.RenderStagesList(currentStage);

            // Assert
            if (applicationScheme == EApplicationScheme.CladdingSafetyScheme)
            {
                Assert.Contains("Apply for grant", result);
                Assert.DoesNotContain("Create building record", result);
            }
            else
            {
                Assert.Contains("Create building record", result);
                Assert.DoesNotContain("Apply for grant", result);
            }
        }


        [Fact]
        public void Process_ShouldSetCorrectAttributesOnTagHelperOutput()
        {
            // Arrange
            var tagHelper = new ApplicationStagesListTagHelper
            {
                ApplicationStage = EApplicationStage.WorksDelivery
            };

            var context = new TagHelperContext(
                new TagHelperAttributeList(),
                new Dictionary<object, object>(),
                "test");

            var output = new TagHelperOutput(
                "application-stages-list",
                new TagHelperAttributeList(),
                (useCachedResult, encoder) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));

            // Act
            tagHelper.Process(context, output);

            // Assert
            Assert.Equal("ul", output.TagName);
            Assert.Equal(TagMode.StartTagAndEndTag, output.TagMode);
            Assert.Equal("govuk-stage-list", output.Attributes["class"].Value);
            Assert.NotNull(output.Content.GetContent());
        }
    }
}