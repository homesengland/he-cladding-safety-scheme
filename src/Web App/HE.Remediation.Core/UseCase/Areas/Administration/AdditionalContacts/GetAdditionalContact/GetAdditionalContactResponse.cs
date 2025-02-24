using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HE.Remediation.Core.UseCase.Areas.Administration.AdditionalContacts.GetAdditionalContact;

public class GetAdditionalContactResponse
{
    public Guid Id { get; set; }

    public string Name { get;set; }

    public string ContactNumber { get;set; }

    public string EmailAddress { get;set; }
}
