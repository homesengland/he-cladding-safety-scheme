using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.CompanyAddress.GetCompanyAddress
{
    public class GetCompanyAddressRequest : IRequest<GetCompanyAddressResponse>
    {
        private GetCompanyAddressRequest() { }

        public static readonly GetCompanyAddressRequest Request = new();

    }
}
