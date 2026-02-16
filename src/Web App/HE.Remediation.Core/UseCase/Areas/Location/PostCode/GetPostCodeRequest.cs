using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Location.PostCode;

public class GetPostCodeRequest : IRequest<GetPostCodeResponse>
{
    public string PostCode { get; set; }
}