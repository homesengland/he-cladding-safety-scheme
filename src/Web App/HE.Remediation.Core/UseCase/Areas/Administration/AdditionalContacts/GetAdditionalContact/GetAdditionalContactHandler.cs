using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Administration.AddExtraContact.GetExtraContact;
using HE.Remediation.Core.UseCase.Areas.Administration.SecondaryContactDetails.GetSecondaryContactDetails;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HE.Remediation.Core.UseCase.Areas.Administration.AdditionalContacts.GetAdditionalContact;

public class GetAdditionalContactHandler: IRequestHandler<GetAdditionalContactRequest, IReadOnlyCollection<GetAdditionalContactResponse>>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IDbConnectionWrapper _db;

    public GetAdditionalContactHandler(IApplicationDataProvider applicationDataProvider, IDbConnectionWrapper db)
    {
        _applicationDataProvider = applicationDataProvider;
        _db = db;
    }

    public async ValueTask<IReadOnlyCollection<GetAdditionalContactResponse>> Handle(GetAdditionalContactRequest request, CancellationToken cancellationToken)
    {
        var userId = _applicationDataProvider.GetUserId();            
        return await GetAdditionalContactDetails(request);
    }

    private async ValueTask<IReadOnlyCollection<GetAdditionalContactResponse>> GetAdditionalContactDetails(GetAdditionalContactRequest request)
    {
        var userId = _applicationDataProvider.GetUserId();
        var results = await _db.QueryAsync<GetAdditionalContactResponse>("GetUserSecondaryContactDetails", new
        {
            userId
        });

        return results;
    }
}
