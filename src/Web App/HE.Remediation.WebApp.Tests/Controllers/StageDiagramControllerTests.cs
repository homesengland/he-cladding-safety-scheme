using Moq;
using Mediator;
using HE.Remediation.WebApp.Areas.Application.Controllers;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Application.StageDiagram.GetStageDiagram;
using HE.Remediation.WebApp.ViewModels.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using HE.Remediation.Core.Enums;

namespace HE.Remediation.WebApp.Tests.Controllers
{
    public class StageDiagramControllerTests
    {
        [Fact]
        public async Task Index_ShouldReturnViewWithCorrectViewModel()
        {
            // Arrange
            var mockSender = new Mock<ISender>();
            var mockMapper = new Mock<IMapper>();
            var mockApplicationDataProvider = new Mock<IApplicationDataProvider>();
            var mockLogger = new Mock<ILogger<StageDiagramController>>();

            var stageDiagramResponse = new GetStageDiagramResponse();
            var stageDiagramViewModel = new StageDiagramViewModel();

            mockSender
                .Setup(sender => sender.Send(It.IsAny<GetStageDiagramRequest>(), default))
                .ReturnsAsync(stageDiagramResponse);

            mockMapper
                .Setup(mapper => mapper.Map<StageDiagramViewModel>(stageDiagramResponse))
                .Returns(stageDiagramViewModel);

            mockApplicationDataProvider
                .Setup(provider => provider.GetApplicationScheme())
                .Returns(EApplicationScheme.CladdingSafetyScheme);

            var controller = new StageDiagramController(
                mockSender.Object,
                mockMapper.Object,
                mockApplicationDataProvider.Object,
                mockLogger.Object
            );

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var viewModel = Assert.IsType<StageDiagramViewModel>(viewResult.Model);

            Assert.Equal(EApplicationScheme.CladdingSafetyScheme, viewModel.ApplicationScheme);
            mockSender.Verify(sender => sender.Send(It.IsAny<GetStageDiagramRequest>(), default), Times.Once);
            mockMapper.Verify(mapper => mapper.Map<StageDiagramViewModel>(stageDiagramResponse), Times.Once);
            mockApplicationDataProvider.Verify(provider => provider.GetApplicationScheme(), Times.Once);
        }
    }
}