using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.FireRiskAssessorListPdf;

public class FireRiskAssessorListPdfRequest : IRequest<byte[]>
{
    private FireRiskAssessorListPdfRequest()
    {
    }

    public static readonly FireRiskAssessorListPdfRequest Request = new();
}