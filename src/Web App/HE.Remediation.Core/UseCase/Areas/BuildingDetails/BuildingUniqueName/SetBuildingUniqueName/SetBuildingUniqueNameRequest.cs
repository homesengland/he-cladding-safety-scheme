using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.BuildingUniqueName.SetBuildingUniqueName
{
    public class SetBuildingUniqueNameRequest : IRequest<Unit>
    {
        public string UniqueName { get; set; }
    }
}