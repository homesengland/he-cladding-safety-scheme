using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PreTenderSupport.ConfirmContactDetails.SetConfirmContactDetails;

public class SetEmailContactDetailsRequest : IRequest
{
    public Guid? Id { get; set; }
    public string EmailAddress { get; set; }
}
