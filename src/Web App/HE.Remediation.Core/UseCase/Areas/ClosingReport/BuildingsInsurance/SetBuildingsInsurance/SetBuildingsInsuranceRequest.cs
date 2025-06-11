using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.BuildingsInsurance.SetBuildingsInsurance;

public class SetBuildingsInsuranceRequest : IRequest<SetBuildingsInsuranceResponse>
{
        public Guid? Id { get; set; }
        public Guid ApplicationId { get; set; }
        public decimal? SumInsuredAmount {  get; set; }
        public decimal? CurrentBuildingInsurancePremiumAmount { get; set; }
        public List<int> SelectedInsuranceProviderIds { get; set; } = [];
        public string IfOtherInsuranceProviderName { get; set; }
        public string AdditionalInfo { get; set; }
}