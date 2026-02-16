using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.Application.NewApplication.SetExistingApplication
{
    public class SetExistingApplicationRequest : IRequest<Unit>
    {
        public Guid ApplicationId { get; set; }
        public string ApplicationNumber { get; set; }
    }
}
