using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Administration.SecondaryContactDetails.SetSecondaryContactDetails;

public class SetSecondaryContactDetailsRequest : IRequest<Unit>
{
    public string Name { get; set; }        

    public string ContactNumber { get; set; }

    public string EmailAddress { get; set; }
}