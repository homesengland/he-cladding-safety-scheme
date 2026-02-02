using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Administration.CorrespondenceAddress.SetCorrespondenceAddress;

public class SetCorrespondenceAddressRequest : IRequest<Unit>
{
    public string SelectedAddressId { get; set; }
}