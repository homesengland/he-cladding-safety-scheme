using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HE.Remediation.Core.UseCase.Areas.Administration.DeleteExtraContact.SetDeleteExtraContact;

public class SetDeleteExtraContactRequest : IRequest
{
    public Guid? Id { get; set; }
}