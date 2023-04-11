using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.DeveloperContacted.SetDeveloperContacted;

public class SetDeveloperContactedRequest : IRequest
{
    public bool? HasDeveloperBeenContactedAboutRemediation { get; set; }
}