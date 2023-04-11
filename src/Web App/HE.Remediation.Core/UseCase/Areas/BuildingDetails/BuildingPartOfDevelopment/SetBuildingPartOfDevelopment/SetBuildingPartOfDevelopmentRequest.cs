using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingPartOfDevelopment.SetBuildingPartOfDevelopment
{
    public class SetBuildingPartOfDevelopmentRequest : IRequest<Unit>
    {
        public ENoYes PartOfDevelopment { get; set; }
    }
}
