using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Location.PostCode;

public class GetPostCodeRequest : IRequest<GetPostCodeResponse>
{
    public string PostCode { get; set; }

    private GetPostCodeRequest() { }

    public static readonly GetPostCodeRequest Request = new();
}