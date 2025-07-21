using AutoMapper;
using MediatR;
using Moq;
using Microsoft.AspNetCore.Mvc;
using HE.Remediation.WebApp.Areas.Application.Controllers;
using HE.Remediation.WebApp.ViewModels.Application;
using HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.Get;
using HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.Invite;
using HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.InviteMember;
using HE.Remediation.Core.UseCase.Areas.Application.ThirdParty.RemoveAccess;
using HE.Remediation.Core.Interface;

namespace HE.Remediation.WebApp.Tests.Controllers
{

    public class ThirdPartyControllerTests
    {
        private readonly Mock<ISender> _senderMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<IApplicationDataProvider> _applicationDataProviderMock = new();

        private readonly ThirdPartyController _controller;

        public ThirdPartyControllerTests()
        {
            _controller = new ThirdPartyController(_senderMock.Object, _mapperMock.Object, _applicationDataProviderMock.Object);
        }

        [Fact]
        public async Task Index_ShouldReturnView_WithMappedViewModel()
        {
            // Arrange
            var response = new GetThirdPartyResponse();
            var viewModel = new ThirdPartyViewModel();

            _senderMock.Setup(s => s.Send(It.IsAny<GetThirdPartyRequest>(), default))
                .ReturnsAsync(response);

            _mapperMock.Setup(m => m.Map<ThirdPartyViewModel>(response))
                .Returns(viewModel);

            // Act
            var result = await _controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(viewModel, result.Model);
        }

        [Fact]
        public async Task InviteContributor_Get_ShouldReturnView_WithMappedViewModel()
        {
            // Arrange
            var teamMemberId = Guid.NewGuid();
            var response = new GetInviteResponse();
            var viewModel = new InviteViewModel();

            _senderMock.Setup(s => s.Send(It.IsAny<GetInviteRequest>(), default))
                .ReturnsAsync(response);

            _mapperMock.Setup(m => m.Map<InviteViewModel>(response))
                .Returns(viewModel);

            // Act
            var result = await _controller.InviteContributor(teamMemberId, Core.Enums.ETeamMemberSource.WorkPackage) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(viewModel, result.Model);
        }

        [Fact]
        public void InviteContributor_Post_ShouldRedirectToInvitationDeclaration()
        {
            // Arrange
            var viewModel = new InviteViewModel { TeamMemberId = Guid.NewGuid() };

            // Act
            var result = _controller.InviteContributor(viewModel) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("InvitationDeclaration", result.ActionName);
            Assert.Equal(viewModel.TeamMemberId, result.RouteValues["teamMemberId"]);
        }

        [Fact]
        public async Task InvitationDeclaration_Post_ShouldRedirectToInvitationSent_WhenValid()
        {
            // Arrange
            var viewModel = new InvitationDeclarationViewModel { TeamMemberId = Guid.NewGuid(), IsDeclarationConfimed = true  };
            var validator = new InvitationDeclarationViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            var request = new SetInviteMemberRequest(Guid.NewGuid(), string.Empty, Core.Enums.ETeamMemberSource.WorkPackage);
            _senderMock.Setup(s => s.Send(request, default))
                .ReturnsAsync(new SetInviteMemberResponse());

            // Act
            var result = await _controller.InvitationDeclaration(viewModel) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("InvitationSent", result.ActionName);
            Assert.Equal(viewModel.TeamMemberId, result.RouteValues["teamMemberId"]);
        }

        [Fact]
        public async Task RemoveAccess_Post_ShouldRedirectToIndex_WhenValid()
        {
            // Arrange
            var viewModel = new RemoveAccessViewModel { TeamMemberId = Guid.NewGuid() };
            var validator = new RemoveAccessViewModelValidator();
            var validationResult = await validator.ValidateAsync(viewModel);

            _senderMock.Setup(s => s.Send(It.IsAny<SetRemoveAccessRequest>(), default))
                .ReturnsAsync(new SetRemoveAccessResponse());

            // Act
            var result = await _controller.RemoveAccess(viewModel) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }
    }
}

