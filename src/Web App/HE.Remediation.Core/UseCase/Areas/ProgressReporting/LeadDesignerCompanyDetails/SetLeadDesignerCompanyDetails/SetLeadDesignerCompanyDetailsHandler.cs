
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.LeadDesignerCompanyDetails.SetLeadDesignerCompanyDetails;

public class SetLeadDesignerCompanyDetailsHandler : IRequestHandler<SetLeadDesignerCompanyDetailsRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IProgressReportingRepository _progressReportingRepository;

    public SetLeadDesignerCompanyDetailsHandler(IApplicationDataProvider applicationDataProvider,
                                                IProgressReportingRepository progressReportingRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<Unit> Handle(SetLeadDesignerCompanyDetailsRequest request, CancellationToken cancellationToken)
    {        
        var progressReportId = _applicationDataProvider.GetProgressReportId();

        TeamMemberDetails memberDetails = new TeamMemberDetails
        {
            ProgressReportId = progressReportId,
            CompanyName = request.CompanyName,
            CompanyRegistrationNumber = request.CompanyRegistrationNumber,
            Name = request.Name,
            EmailAddress = request.EmailAddress,
            PrimaryContactNumber = request.PrimaryContactNumber,
            //OtherRole = request.re
            ContractSigned = request.ContractSigned,
            IndemnityInsurance = request.IndemnityInsurance,
            InvolvedInOriginalInstallation = request.LeadDesignerInvolvedInOriginalInstallation,
            RoleId = (int)ETeamRole.LeadDesigner,
            IndemnityInsuranceReason = request.IndemnityInsuranceReason,
            InvolvedRoleReason = request.LeadDesignerInvolvedRoleReason
        };

        TeamMemberDetails existingTeamMemberDetails = await _progressReportingRepository.GetLeadDesignerCompanyDetails();
        if (existingTeamMemberDetails is not null)
        {
            memberDetails.Id = existingTeamMemberDetails.Id;
            await _progressReportingRepository.UpdateTeamMember(memberDetails);        
            return Unit.Value;
        }
                
        await _progressReportingRepository.InsertTeamMember(memberDetails);        
        
        return Unit.Value;
    }
}
