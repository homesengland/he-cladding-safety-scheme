using MediatR;
namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.WorksToCladdingSystems.SetCladdingSystem;

public class SetCladdingSystemRequest : IRequest<Unit>
{
    public SetCladdingSystemRequest()
    {}

    public Guid? FireRiskCladdingSystemsId { get; set; }
    public int? InsulationTypeId { get; set; }
    public int? CladdingSystemTypeId { get; set; }
}