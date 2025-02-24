using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.GrantFundingSignatories.DeleteGrantFundingSignatory
{
    public class DeleteGrantFundingSignatoryRequest : IRequest
    {
        public Guid GrantFundingSignatoryId { get; set; }
    }
}
