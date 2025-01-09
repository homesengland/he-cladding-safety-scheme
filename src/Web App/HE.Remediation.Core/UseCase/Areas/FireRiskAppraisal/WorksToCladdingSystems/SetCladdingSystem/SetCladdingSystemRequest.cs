using MediatR;
namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.WorksToCladdingSystems.SetCladdingSystem;

public class SetCladdingSystemRequest : IRequest<Unit>
{
    public SetCladdingSystemRequest()
    { }

    public Guid? FireRiskCladdingSystemsId { get; set; }
    public int? InsulationTypeId { get; set; }
    public int? CladdingSystemTypeId { get; set; }
    public int? CladdingManufacturerId { get; set; }
    public int? InsulationManufacturerId { get; set; }
    public string OtherInsulationManufacturer { get; set; }
    public string OtherCladdingManufacturer { get; set; }
    public string OtherCladdingType { get; set; }
    public string OtherInsulationType { get; set; }
}