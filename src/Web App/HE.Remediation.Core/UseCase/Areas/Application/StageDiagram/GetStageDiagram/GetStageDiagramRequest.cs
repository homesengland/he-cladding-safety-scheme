using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.Application.StageDiagram.GetStageDiagram
{
    public class GetStageDiagramRequest : IRequest<GetStageDiagramResponse>
    {
        private GetStageDiagramRequest()
        {
        }

        public static GetStageDiagramRequest Request = new();
    }
}
