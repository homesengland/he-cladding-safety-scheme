using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting;

public class SetGrantCertifyingOfficerAddressHandler : IRequestHandler<SetGrantCertifyingOfficerAddressRequest>
{
    private readonly IProgressReportingRepository _progressReportingRepository;

    public SetGrantCertifyingOfficerAddressHandler(IProgressReportingRepository progressReportingRepository)
    {
        _progressReportingRepository = progressReportingRepository;
    }

    public async ValueTask<Unit> Handle(SetGrantCertifyingOfficerAddressRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await _progressReportingRepository.UpdateGrantCertifyingOfficerAddress(
            new UpdateGrantCertifyingOfficerAddressParameters
            {
                NameNumber = request.NameNumber,
                AddressLine1 = request.AddressLine1,
                AddressLine2 = request.AddressLine2,
                City = request.City,
                County = request.County,
                Postcode = request.Postcode
            });

        return Unit.Value;
    }
}

public class SetGrantCertifyingOfficerAddressRequest : IRequest
{
    public string NameNumber { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string County { get; set; }
    public string Postcode { get; set; }
}