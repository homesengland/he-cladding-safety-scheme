using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageGrantCertifyingOfficer.IsCorrectPerson.Get;

public class GetIsCorrectPersonRequest : IRequest<GetIsCorrectPersonResponse>
{
    private GetIsCorrectPersonRequest()
    {
    }

    public static readonly GetIsCorrectPersonRequest Request = new();
}