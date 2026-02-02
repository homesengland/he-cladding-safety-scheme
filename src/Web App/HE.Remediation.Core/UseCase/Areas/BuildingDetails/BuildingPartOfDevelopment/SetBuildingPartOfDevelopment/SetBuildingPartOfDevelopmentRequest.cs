using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingPartOfDevelopment.SetBuildingPartOfDevelopment
{
    public class SetBuildingPartOfDevelopmentRequest : IRequest<Unit>
    {
        public ENoYes PartOfDevelopment { get; set; }
    }
}
