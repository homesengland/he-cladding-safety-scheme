using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.GrantCertifyingOfficer;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.AuthorisedSignatories.Set;

public class SetAuthorisedSignatoriesHandler : IRequestHandler<SetAuthorisedSignatoriesRequest>
{
    private readonly IWorkPackageRepository _workPackageRepository;

    public SetAuthorisedSignatoriesHandler(IWorkPackageRepository workPackageRepository)
    {
        _workPackageRepository = workPackageRepository;
    }

    public async ValueTask<Unit> Handle(SetAuthorisedSignatoriesRequest request, CancellationToken cancellationToken)
    {
        await _workPackageRepository.UpdateGrantCertifyingOfficerAuthorisedSignatories(new UpdateGrantCertifyingOfficerAuthorisedSignatoriesParameters
        {
            AuthorisedSignatory1 = request.AuthorisedSignatory1,
            AuthorisedSignatory1EmailAddress = request.AuthorisedSignatory1EmailAddress,
            CompaniesDateOfAppointment = request.CompaniesDateOfAppointmentYear.HasValue && request.CompaniesDateOfAppointmentMonth.HasValue && request.CompaniesDateOfAppointmentDay.HasValue ? new DateTime(request.CompaniesDateOfAppointmentYear.Value, request.CompaniesDateOfAppointmentMonth.Value, request.CompaniesDateOfAppointmentDay.Value) : null,
        });

        return Unit.Value;
    }
}