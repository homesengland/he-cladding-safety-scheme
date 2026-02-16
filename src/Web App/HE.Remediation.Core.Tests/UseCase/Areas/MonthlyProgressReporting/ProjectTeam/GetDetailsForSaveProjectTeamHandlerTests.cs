using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectTeam;
using HE.Remediation.Core.Data.StoredProcedureResults.MonthlyProgressReport.ProjectTeam;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Providers.ApplicationDetailsProvider;
using HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.MonthlyProgressReporting.ProjectTeam;

public class GetDetailsForSaveProjectTeamHandlerTests
{
    private readonly Mock<IApplicationDetailsProvider> _detailsProvider;
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IProgressReportingProjectTeamRepository> _progressReportingProjectTeamRepository;

    private readonly GetDetailsForSaveProjectTeamHandler _handler;

    public GetDetailsForSaveProjectTeamHandlerTests()
    {
        _detailsProvider = new Mock<IApplicationDetailsProvider>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _progressReportingProjectTeamRepository = new Mock<IProgressReportingProjectTeamRepository>(MockBehavior.Strict);

        _handler = new GetDetailsForSaveProjectTeamHandler(
            _detailsProvider.Object,
            _applicationDataProvider.Object,
            _progressReportingProjectTeamRepository.Object);
    }

    [Theory]
    [InlineData("Some reason", true)]
    [InlineData(null, false)]
    public async Task Handle_ReasonNoTeamScenarios_ReturnsExpectedHasReasonNoTeam(string? reasonNoTeam, bool expectedHasReasonNoTeam)
    {
        // Arrange
        var applicationId = Guid.NewGuid();
        var progressReportId = Guid.NewGuid();

        SetupApplicationDetails(applicationId);
        SetupProgressReportId(progressReportId);
        SetupGcoDetails(null);
        SetupTeamMembers(applicationId, progressReportId, new List<GetTeamMembersResult>());
        SetupReasonNoTeam(applicationId, progressReportId, reasonNoTeam);

        // Act
        var result = await _handler.Handle(GetDetailsForSaveProjectTeamRequest.Request, CancellationToken.None);

        // Assert
        Assert.Equal(expectedHasReasonNoTeam, result.HasReasonNoTeam);
        VerifyAllMocks();
    }

    [Theory]
    [InlineData(true, true)]
    [InlineData(false, false)]
    [InlineData(null, false)]
    public async Task Handle_GcoCompleteStatusScenarios_ReturnsExpectedIsGcoComplete(bool? isGcoComplete, bool expectedIsGcoComplete)
    {
        // Arrange
        var applicationId = Guid.NewGuid();
        var progressReportId = Guid.NewGuid();

        var gcoDetails = isGcoComplete.HasValue
            ? new GetGrantCertifyingOfficerResult { IsGcoComplete = isGcoComplete.Value }
            : null;

        SetupApplicationDetails(applicationId);
        SetupProgressReportId(progressReportId);
        SetupGcoDetails(gcoDetails);
        SetupTeamMembers(applicationId, progressReportId, new List<GetTeamMembersResult>());
        SetupReasonNoTeam(applicationId, progressReportId, null);

        // Act
        var result = await _handler.Handle(GetDetailsForSaveProjectTeamRequest.Request, CancellationToken.None);

        // Assert
        Assert.Equal(expectedIsGcoComplete, result.IsGcoComplete);
        VerifyAllMocks();
    }

    [Theory]
    [MemberData(nameof(TeamMemberScenarios))]
    public async Task Handle_TeamMemberScenarios_ReturnsExpectedTeamMemberFlags(
        List<GetTeamMembersResult> teamMembers,
        bool expectedHasZeroTeamMembers,
        bool expectedHasTeamMembersButNoGcoRoles)
    {
        // Arrange
        var applicationId = Guid.NewGuid();
        var progressReportId = Guid.NewGuid();

        SetupApplicationDetails(applicationId);
        SetupProgressReportId(progressReportId);
        SetupGcoDetails(null);
        SetupTeamMembers(applicationId, progressReportId, teamMembers);
        SetupReasonNoTeam(applicationId, progressReportId, null);

        // Act
        var result = await _handler.Handle(GetDetailsForSaveProjectTeamRequest.Request, CancellationToken.None);

        // Assert
        Assert.Equal(expectedHasZeroTeamMembers, result.HasZeroTeamMembers);
        Assert.Equal(expectedHasTeamMembersButNoGcoRoles, result.HasTeamMembersButNoGcoRoles);
        VerifyAllMocks();
    }

    public static IEnumerable<object[]> TeamMemberScenarios()
    {
        // No team members
        yield return new object[]
        {
            new List<GetTeamMembersResult>(),
            true,  // HasZeroTeamMembers
            false  // HasTeamMembersButNoGcoRoles
        };

        // Team members with non-GCO roles
        yield return new object[]
        {
            new List<GetTeamMembersResult>
            {
                new GetTeamMembersResult
                {
                    Id = Guid.NewGuid(),
                    RoleId = (int)ETeamRole.FireSafetyEngineer,
                    Name = "John Doe",
                    RoleName = "Fire Safety Engineer"
                }
            },
            false, // HasZeroTeamMembers
            true   // HasTeamMembersButNoGcoRoles
        };

        // Only Project Manager
        yield return new object[]
        {
            new List<GetTeamMembersResult>
            {
                new GetTeamMembersResult
                {
                    Id = Guid.NewGuid(),
                    RoleId = (int)ETeamRole.ProjectManager,
                    Name = "Project Manager",
                    RoleName = "Project Manager"
                }
            },
            false, // HasZeroTeamMembers
            true   // HasTeamMembersButNoGcoRoles
        };

        // Only Quantity Surveyor
        yield return new object[]
        {
            new List<GetTeamMembersResult>
            {
                new GetTeamMembersResult
                {
                    Id = Guid.NewGuid(),
                    RoleId = (int)ETeamRole.QuantitySurveyor,
                    Name = "Quantity Surveyor",
                    RoleName = "Quantity Surveyor"
                }
            },
            false, // HasZeroTeamMembers
            true   // HasTeamMembersButNoGcoRoles
        };

        // Both GCO roles (Project Manager and Quantity Surveyor)
        yield return new object[]
        {
            new List<GetTeamMembersResult>
            {
                new GetTeamMembersResult
                {
                    Id = Guid.NewGuid(),
                    RoleId = (int)ETeamRole.ProjectManager,
                    Name = "Project Manager",
                    RoleName = "Project Manager"
                },
                new GetTeamMembersResult
                {
                    Id = Guid.NewGuid(),
                    RoleId = (int)ETeamRole.QuantitySurveyor,
                    Name = "Quantity Surveyor",
                    RoleName = "Quantity Surveyor"
                }
            },
            false, // HasZeroTeamMembers
            false  // HasTeamMembersButNoGcoRoles
        };
    }

    [Fact]
    public async Task Handle_CompleteScenario_ReturnsCorrectResponse()
    {
        // Arrange
        var applicationId = Guid.NewGuid();
        var progressReportId = Guid.NewGuid();

        var teamMembers = new List<GetTeamMembersResult>
        {
            new GetTeamMembersResult
            {
                Id = Guid.NewGuid(),
                RoleId = (int)ETeamRole.ProjectManager,
                Name = "PM Name",
                RoleName = "Project Manager"
            },
            new GetTeamMembersResult
            {
                Id = Guid.NewGuid(),
                RoleId = (int)ETeamRole.QuantitySurveyor,
                Name = "QS Name",
                RoleName = "Quantity Surveyor"
            },
            new GetTeamMembersResult
            {
                Id = Guid.NewGuid(),
                RoleId = (int)ETeamRole.FireSafetyEngineer,
                Name = "FSE Name",
                RoleName = "Fire Safety Engineer"
            }
        };

        var gcoDetails = new GetGrantCertifyingOfficerResult
        {
            IsGcoComplete = true
        };

        SetupApplicationDetails(applicationId);
        SetupProgressReportId(progressReportId);
        SetupGcoDetails(gcoDetails);
        SetupTeamMembers(applicationId, progressReportId, teamMembers);
        SetupReasonNoTeam(applicationId, progressReportId, null);

        // Act
        var result = await _handler.Handle(GetDetailsForSaveProjectTeamRequest.Request, CancellationToken.None);

        // Assert
        Assert.False(result.HasReasonNoTeam);
        Assert.False(result.HasZeroTeamMembers);
        Assert.False(result.HasTeamMembersButNoGcoRoles);
        Assert.True(result.IsGcoComplete);
        VerifyAllMocks();
    }

    private void SetupApplicationDetails(Guid applicationId)
    {
        _detailsProvider.Setup(x => x.GetApplicationDetails())
            .ReturnsAsync(new ApplicationDetailsModel
            {
                ApplicationId = applicationId,
                ApplicationReferenceNumber = "TEST-REF-001",
                BuildingName = "Test Building"
            })
            .Verifiable();
    }

    private void SetupProgressReportId(Guid progressReportId)
    {
        _applicationDataProvider.Setup(x => x.GetProgressReportId())
            .Returns(progressReportId)
            .Verifiable();
        
        _applicationDataProvider.Setup(x => x.GetApplicationScheme())
            .Returns(EApplicationScheme.CladdingSafetyScheme)
            .Verifiable();
    }

    private void SetupGcoDetails(GetGrantCertifyingOfficerResult? gcoDetails)
    {
        _progressReportingProjectTeamRepository.Setup(x => x.GetGrantCertifyingOfficer(It.IsAny<Guid>()))
            .ReturnsAsync(gcoDetails)
            .Verifiable();
    }

    private void SetupTeamMembers(Guid applicationId, Guid progressReportId, List<GetTeamMembersResult> teamMembers)
    {
        _progressReportingProjectTeamRepository.Setup(x => x.GetProjectTeamMembers(
                It.Is<GetTeamMembersParameters>(p =>
                    p.ApplicationId == applicationId &&
                    p.ProgressReportId == progressReportId)))
            .ReturnsAsync(teamMembers)
            .Verifiable();
    }

    private void SetupReasonNoTeam(Guid applicationId, Guid progressReportId, string? reasonNoTeam)
    {
        var result = reasonNoTeam != null
            ? new GetProjectTeamNoTeamResult { ReasonNoTeam = reasonNoTeam }
            : null;

        _progressReportingProjectTeamRepository.Setup(x => x.GetProjectTeamNoTeam(
                It.Is<GetProjectTeamNoTeamParameters>(p =>
                    p.ApplicationId == applicationId &&
                    p.ProgressReportId == progressReportId)))
            .ReturnsAsync(result)
            .Verifiable();
    }

    private void VerifyAllMocks()
    {
        _detailsProvider.Verify();
        _applicationDataProvider.Verify();
        _progressReportingProjectTeamRepository.Verify();
    }
}
