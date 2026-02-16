using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.NonResidentialUnits.SetNonResidentialUnits
{
    public class SetNonResidentialUnitsRequest : IRequest<Unit>
    {
        public int? NonResidentialUnitsCount { get; set; }
    }
}