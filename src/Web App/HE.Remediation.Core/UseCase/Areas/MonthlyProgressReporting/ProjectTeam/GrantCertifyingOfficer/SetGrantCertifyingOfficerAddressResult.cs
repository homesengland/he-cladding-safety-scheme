using HE.Remediation.Core.Data.Repositories.MonthlyProgressReporting;
using HE.Remediation.Core.Data.StoredProcedureParameters.MonthlyProgressReport.ProjectTeam;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Location.PostCode;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.MonthlyProgressReporting.ProjectTeam.GrantCertifyingOfficer;

public class SetGrantCertifyingOfficerAddressResultHandler : IRequestHandler<SetGrantCertifyingOfficerAddressResultRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingProjectTeamRepository _progressReportingProjectTeamRepository;

    public SetGrantCertifyingOfficerAddressResultHandler(
        IApplicationDataProvider applicationDataProvider,
        IProgressReportingProjectTeamRepository progressReportingProjectTeamRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _progressReportingProjectTeamRepository = progressReportingProjectTeamRepository;
    }

    public async Task<Unit> Handle(SetGrantCertifyingOfficerAddressResultRequest request, CancellationToken cancellationToken)
    {
        var progressReportId = _applicationDataProvider.GetProgressReportId();
        var parsedAddress = PostCodeUtility.ParseAddressJson(request.SelectedAddressId);
        if (parsedAddress != null)
        {
            await _progressReportingProjectTeamRepository.UpdateGrantCertifyingOfficerAddress(
                new UpdateGrantCertifyingOfficerAddressParameters
                {
                    ProgressReportId = progressReportId,
                    NameNumber = parsedAddress.NameNumber ?? "",
                    AddressLine1 = parsedAddress.AddressLine1 ?? "",
                    AddressLine2 = parsedAddress.AddressLine2 ?? "",
                    City = parsedAddress.City ?? "",
                    LocalAuthority = parsedAddress.LocalAuthority,
                    County = parsedAddress.County ?? "",
                    Postcode = parsedAddress.Postcode ?? "",
                    SubBuildingName = parsedAddress.SubBuildingName,
                    BuildingName = parsedAddress.BuildingName,
                    BuildingNumber = parsedAddress.BuildingNumber,
                    Street = parsedAddress.Street,
                    Town = parsedAddress.Town,
                    AdminArea = parsedAddress.AdminArea,
                    UPRN = parsedAddress.UPRN,
                    AddressLines = parsedAddress.AddressLines,
                    XCoordinate = parsedAddress.XCoordinate,
                    YCoordinate = parsedAddress.YCoordinate,
                    Toid = parsedAddress.Toid,
                    BuildingType = parsedAddress.BuildingType
                });
        }

        return Unit.Value;
    }
}

public class SetGrantCertifyingOfficerAddressResultRequest : IRequest
{
    public string SelectedAddressId { get; set; }
}