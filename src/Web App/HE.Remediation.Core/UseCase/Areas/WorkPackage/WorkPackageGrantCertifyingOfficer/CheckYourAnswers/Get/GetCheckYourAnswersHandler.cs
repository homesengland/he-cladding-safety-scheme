using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.CheckYourAnswers.Get;

public class GetCheckYourAnswersHandler : IRequestHandler<GetCheckYourAnswersRequest, GetCheckYourAnswersResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageRepository _workPackageRepository;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public GetCheckYourAnswersHandler(IApplicationDataProvider applicationDataProvider,
                                           IBuildingDetailsRepository buildingDetailsRepository,
                                           IApplicationRepository applicationRepository,
                                           IWorkPackageRepository workPackageRepository,
                                           IProgressReportingRepository progressReportingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _workPackageRepository = workPackageRepository;
        _progressReportingRepository = progressReportingRepository;
    }

    public async ValueTask<GetCheckYourAnswersResponse> Handle(GetCheckYourAnswersRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var checkMyAnswersResult = await _workPackageRepository.GetGrantCertifyingOfficerAnswers();
        var isSubmitted = await _workPackageRepository.IsWorkPackageSubmitted();
        var isProgressReportGrantCertifyingOfficerComplete = await _progressReportingRepository.IsGrantCertifyingOfficerComplete();

        return new GetCheckYourAnswersResponse
        {
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber,
            Name = checkMyAnswersResult.Name,
            RoleName = checkMyAnswersResult.RoleName,
            CompanyName = checkMyAnswersResult.CompanyName,
            CompanyRegistrationNumber = checkMyAnswersResult.CompanyRegistrationNumber,
            EmailAddress = checkMyAnswersResult.EmailAddress,
            PrimaryContactNumber = checkMyAnswersResult.PrimaryContactNumber,
            CompanyNameNumber = checkMyAnswersResult.CompanyNameNumber,
            CompanyAddressLine1 = checkMyAnswersResult.CompanyAddressLine1,
            CompanyAddressLine2 = checkMyAnswersResult.CompanyAddressLine2,
            CompanyCity = checkMyAnswersResult.CompanyCity,
            CompanyCounty = checkMyAnswersResult.CompanyCounty,
            CompanyPostcode = checkMyAnswersResult.CompanyPostcode,
            DateAppointed = checkMyAnswersResult.DateAppointed,
            ContractSigned = checkMyAnswersResult.ContractSigned,
            IndemnityInsurance = checkMyAnswersResult.IndemnityInsurance,
            InvolvedInOriginalInstallation = checkMyAnswersResult.InvolvedInOriginalInstallation,
            AuthorisedSignatory1 = checkMyAnswersResult.AuthorisedSignatory1,
            AuthorisedSignatory1EmailAddress = checkMyAnswersResult.AuthorisedSignatory1EmailAddress,
            CompaniesDateOfAppointment = checkMyAnswersResult.CompaniesDateOfAppointment,
            IsSubmitted = isSubmitted,
            IsProgressReportGrantCertifyingOfficerComplete = isProgressReportGrantCertifyingOfficerComplete
        };
    }
}
