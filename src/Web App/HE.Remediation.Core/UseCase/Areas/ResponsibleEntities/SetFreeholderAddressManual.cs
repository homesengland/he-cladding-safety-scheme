using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities;

public class SetFreeholderAddressManual: IRequestHandler<SetFreeholderAddressManualRequest>
{
    private readonly IDbConnectionWrapper _connection;
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IResponsibleEntityRepository _responsibleEntityRepository;

    public SetFreeholderAddressManual(IDbConnectionWrapper connection,
                                      IApplicationDataProvider applicationDataProvider,
                                      IResponsibleEntityRepository responsibleEntityRepository)
    {
        _connection = connection;
        _applicationDataProvider = applicationDataProvider;
        _responsibleEntityRepository = responsibleEntityRepository;
    }

    public async Task<Unit> Handle(SetFreeholderAddressManualRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        FreeholderAddressManualDetails addressDetails = new FreeholderAddressManualDetails
        {
            NameNumber = request.NameNumber,
            AddressLine1 = request.AddressLine1,
            AddressLine2 = request.AddressLine2,
            City = request.City,
            County = request.County,
            Postcode = request.Postcode
        };

        var response = await _responsibleEntityRepository.GetFreeholderAddress(applicationId);        
        if (response is not null)
        {            
            await _responsibleEntityRepository.UpdateFreeholderAddress(applicationId, addressDetails);
            return Unit.Value;
        }

        await _responsibleEntityRepository.InsertFreeholderAddress(applicationId, addressDetails);
        return Unit.Value;
    }
}

public class SetFreeholderAddressManualRequest : MediatR.IRequest
{
    public string NameNumber { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string County { get; set; }
    public string Postcode { get; set; }
}