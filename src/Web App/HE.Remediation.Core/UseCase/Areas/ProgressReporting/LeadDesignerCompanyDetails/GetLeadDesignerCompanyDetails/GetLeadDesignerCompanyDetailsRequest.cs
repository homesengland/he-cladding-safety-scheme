using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.LeadDesignerCompanyDetails.GetLeadDesignerCompanyDetails;

public class GetLeadDesignerCompanyDetailsRequest : IRequest<GetLeadDesignerCompanyDetailsResponse>
{
    private GetLeadDesignerCompanyDetailsRequest()
    {
    }

    public static readonly GetLeadDesignerCompanyDetailsRequest Request = new();
}
