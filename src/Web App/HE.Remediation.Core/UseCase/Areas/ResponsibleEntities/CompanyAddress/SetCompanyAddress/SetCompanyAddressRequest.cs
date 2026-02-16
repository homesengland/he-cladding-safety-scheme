using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.CompanyAddress.SetCompanyAddress
{
    public class SetCompanyAddressRequest : IRequest
    {
        public string NameNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
    }
}
