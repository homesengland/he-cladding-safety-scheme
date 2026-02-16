using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.ExternalWallWorks;

public class SetDeleteExternalWallWorksRequest: IRequest
{
        public Guid? Id { get; set; }
}
