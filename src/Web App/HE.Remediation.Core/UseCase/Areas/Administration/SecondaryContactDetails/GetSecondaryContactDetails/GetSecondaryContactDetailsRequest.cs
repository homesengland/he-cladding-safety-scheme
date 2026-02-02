using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Administration.SecondaryContactDetails.GetSecondaryContactDetails
{
    public class GetSecondaryContactDetailsRequest : IRequest<GetSecondaryContactDetailsResponse>
    {        
        public Guid? Id { get; set; }            
    }
}
