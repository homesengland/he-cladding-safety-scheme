
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PreTenderSupport.EmailContactDetails;

public class GetEmailContactDetailsRequest : IRequest<GetEmailContactDetailsResponse>
{
    public Guid? Id { get; set; }    
}
