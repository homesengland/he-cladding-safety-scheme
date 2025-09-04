using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.AlternativeFundingRoutes.PursuedSourcesFunding.SetPursuedSourcesFunding
{
    public class SetPursuedSourcesFundingRequest: IRequest<SetPursuedSourcesFundingResponse>
    {
        public EPursuedSourcesFundingType? PursuedSourcesFunding { get; set; }
    }
}
