using Microsoft.AspNetCore.Razor.TagHelpers;
using HE.Remediation.WebApp.TagHelpers;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.Tests.TagHelpers
{
    public class ApplicationStageTagHelperTests
    {
        [Fact]
        public void GetStageName_ShouldReturnCorrectName_ForGivenStage()
        {
            // Arrange
            var tagHelper = new ApplicationStageTagHelper();

            // Act
            var stageName = tagHelper.GetStageName(EApplicationStage.WorksDelivery);

            // Assert
            Assert.Equal("Works delivery", stageName);
        }

        [Fact]
        public void Process_ShouldRenderOnlyText_WhenTextOnlyIsTrue()
        {
            // Arrange
            var tagHelper = new ApplicationStageTagHelper
            {
                ApplicationStage = EApplicationStage.WorksDelivery,
                TextOnly = true
            };

            var context = new TagHelperContext(
                new TagHelperAttributeList(),
                new Dictionary<object, object>(),
                "test");

            var output = new TagHelperOutput(
                "application-stage-text",
                new TagHelperAttributeList(),
                (useCachedResult, encoder) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));

            // Act
            tagHelper.Process(context, output);

            // Assert
            var content = output.PreContent.GetContent();
            Assert.Equal("Works delivery", content);
        }

        [Fact]
        public void Process_ShouldRenderWithHtml_WhenTextOnlyIsFalseOrNull()
        {
            // Arrange
            var tagHelper = new ApplicationStageTagHelper
            {
                ApplicationStage = EApplicationStage.WorksDelivery,
                TextOnly = false
            };

            var context = new TagHelperContext(
                new TagHelperAttributeList(),
                new Dictionary<object, object>(),
                "test");

            var output = new TagHelperOutput(
                "application-stage-text",
                new TagHelperAttributeList(),
                (useCachedResult, encoder) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));

            // Act
            tagHelper.Process(context, output);

            // Assert
            var content = output.PreContent.GetContent();
            Assert.Contains("<span class=\"govuk-body\">Works delivery</span>", content);
        }

        [Fact]
        public void Process_ShouldRenderCorrectAttributesOnTagHelperOutput()
        {
            // Arrange
            var tagHelper = new ApplicationStageTagHelper
            {
                ApplicationStage = EApplicationStage.GrantFundingAgreement,
                TextOnly = null
            };

            var context = new TagHelperContext(
                new TagHelperAttributeList(),
                new Dictionary<object, object>(),
                "test");

            var output = new TagHelperOutput(
                "application-stage-text",
                new TagHelperAttributeList(),
                (useCachedResult, encoder) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));

            // Act
            tagHelper.Process(context, output);

            // Assert
            Assert.Equal("span", output.TagName);
            Assert.Equal(TagMode.StartTagAndEndTag, output.TagMode);
            Assert.Contains("<span class=\"govuk-body\">Grant funding agreement</span>", output.PreContent.GetContent());
        }

        [Theory]
        [InlineData(EApplicationScheme.CladdingSafetyScheme)]
        [InlineData(EApplicationScheme.SocialSector)]
        [InlineData(EApplicationScheme.ResponsibleActorsScheme)]
        [InlineData(EApplicationScheme.SelfRemediating)]
        public void RenderStagesList_ShouldContainSpecificText_WhenSchemesAreSet(EApplicationScheme applicationScheme)
        {
            // Arrange
            var tagHelper = new ApplicationStageTagHelper
            {
                ApplicationStage = EApplicationStage.ApplyForGrant,
                ApplicationScheme = applicationScheme,
                TextOnly = false
            };

            var context = new TagHelperContext(
                new TagHelperAttributeList(),
                new Dictionary<object, object>(),
                "test");

            var output = new TagHelperOutput(
                "application-stage-text",
                new TagHelperAttributeList(),
                (useCachedResult, encoder) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));

            // Act
            tagHelper.Process(context, output);

            // Assert
            var content = output.PreContent.GetContent();

            if (applicationScheme == EApplicationScheme.CladdingSafetyScheme)
            {
                Assert.Contains("<span class=\"govuk-body\">Apply for grant</span>", content);
                Assert.DoesNotContain("<span class=\"govuk-body\">Create building record</span>", content);
            }
            else
            {
                Assert.Contains("<span class=\"govuk-body\">Create building record</span>", content);
                Assert.DoesNotContain("<span class=\"govuk-body\">Apply for grant</span>", content);
            }


        }

    }
}