using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.AuthorisedSignatories.Get;

public class GetAuthorisedSignatoriesHandler : IRequestHandler<GetAuthorisedSignatoriesRequest, GetAuthorisedSignatoriesResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageRepository _workPackageRepository;

    public GetAuthorisedSignatoriesHandler(IApplicationDataProvider applicationDataProvider,
                            IBuildingDetailsRepository buildingDetailsRepository,
                            IApplicationRepository applicationRepository,
                            IWorkPackageRepository workPackageRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<GetAuthorisedSignatoriesResponse> Handle(GetAuthorisedSignatoriesRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var authorisedSignatories = await _workPackageRepository.GetGrantCertifyingOfficerAuthorisedSignatories();
        var isSubmitted = await _workPackageRepository.IsWorkPackageSubmitted();

        return new GetAuthorisedSignatoriesResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            AuthorisedSignatory1 = authorisedSignatories.AuthorisedSignatory1,
            AuthorisedSignatory1EmailAddress = authorisedSignatories.AuthorisedSignatory1EmailAddress,
            CompaniesDateOfAppointmentDay = authorisedSignatories.CompaniesDateOfAppointment?.Day,
            CompaniesDateOfAppointmentMonth = authorisedSignatories.CompaniesDateOfAppointment?.Month,
            CompaniesDateOfAppointmentYear = authorisedSignatories.CompaniesDateOfAppointment?.Year,
            IsSubmitted = isSubmitted
        };
    }
}
