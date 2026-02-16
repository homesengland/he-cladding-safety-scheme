using HE.Remediation.Core.UseCase.Areas.Administration.SecondaryContactDetails.GetSecondaryContactDetails;
using Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HE.Remediation.Core.UseCase.Areas.Administration.AdditionalContacts.GetAdditionalContact;

public class GetAdditionalContactRequest : IRequest<IReadOnlyCollection<GetAdditionalContactResponse>>
{
    public string FirstName { get;set; }

    public string LastName { get;set; }

    public string ContactNumber { get;set; }

    private GetAdditionalContactRequest()
    {
    }

    public static readonly GetAdditionalContactRequest Request = new();
}
