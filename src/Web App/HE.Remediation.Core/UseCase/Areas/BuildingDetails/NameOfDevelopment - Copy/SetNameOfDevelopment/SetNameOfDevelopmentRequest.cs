using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.BuildingDetails.NameOfDevelopment.SetNameOfDevelopment
{
    public class SetNameOfDevelopmentRequest : IRequest<Unit>
    {
        public string NameOfDevelopment { get; set; }
    }
}
