using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageCostsScheduling.CostsTemplate.Get;

public class GetCostsTemplateRequest : IRequest<GetCostsTemplateResponse>
{
    private GetCostsTemplateRequest()
    {
    }

    public static GetCostsTemplateRequest Request => new();
}
