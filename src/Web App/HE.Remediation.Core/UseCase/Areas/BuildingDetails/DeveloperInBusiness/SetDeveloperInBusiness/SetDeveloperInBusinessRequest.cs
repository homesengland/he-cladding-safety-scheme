using HE.Remediation.Core.Enums;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.DeveloperInBusiness.SetDeveloperInBusiness;

public class SetDeveloperInBusinessRequest : IRequest
{
    public EApplicationDeveloperInBusinessType? IsOriginalDeveloperStillInBusiness { get; set; }
}