using HE.Remediation.Core.Enums;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.DeveloperInBusiness.SetDeveloperInBusiness;

public class SetDeveloperInBusinessRequest : IRequest
{
    public EApplicationDeveloperInBusinessType? IsOriginalDeveloperStillInBusiness { get; set; }
}