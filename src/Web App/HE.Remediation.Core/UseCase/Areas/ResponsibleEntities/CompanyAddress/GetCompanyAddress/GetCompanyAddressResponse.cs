namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.CompanyAddress.GetCompanyAddress
{
    public class GetCompanyAddressResponse
    {
        public string NameNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }

        public bool UkRegistered { get; set; }
    }
}
