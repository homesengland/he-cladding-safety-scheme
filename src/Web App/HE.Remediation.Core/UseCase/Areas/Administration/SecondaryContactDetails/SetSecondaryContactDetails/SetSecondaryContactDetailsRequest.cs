using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Administration.SecondaryContactDetails.SetSecondaryContactDetails;

public class SetSecondaryContactDetailsRequest : IRequest<Unit>
{    
    public Guid? Id { get; set; }    

    public string Name { get; set; }        

    public string ContactNumber { get; set; }

    public string EmailAddress { get; set; }
}