using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetProjectTeam;

public class GetProjectTeamHandler : IRequestHandler<GetProjectTeamRequest, GetProjectTeamResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IPaymentRequestRepository _paymentRequestRepository;

    public GetProjectTeamHandler(IApplicationDataProvider applicationDataProvider,
                                 IApplicationRepository applicationRepository,
                                 IBuildingDetailsRepository buildingDetailsRepository,
                                 IPaymentRequestRepository paymentRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;    
        _paymentRequestRepository = paymentRequestRepository;
    }

    public async ValueTask<GetProjectTeamResponse> Handle(GetProjectTeamRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var paymentRequestId = _applicationDataProvider.GetPaymentRequestId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var teamMembers = await _paymentRequestRepository.GetPaymentRequestTeamMembers();                

        var projectDetails = await _paymentRequestRepository.GetPaymentRequestProjectDetails(paymentRequestId);

        var noOfMonthsSlippage = 2;
        var endDateSlipped = projectDetails.ProjectDatesChanged == true && await EndDateHasSlipped(applicationId, noOfMonthsSlippage);

        var isSubmitted = await _paymentRequestRepository.IsPaymentRequestSubmitted(paymentRequestId);
        var isExpired = await _paymentRequestRepository.IsPaymentRequestExpired(paymentRequestId);

        var allOptions = Enum.GetValues<ETeamRole>().ToList();
        var consumedOptions = teamMembers.Select(tm => (ETeamRole)tm.RoleId).ToList();

        consumedOptions.Add(ETeamRole.Other);
        var missingRoles = allOptions.Except(consumedOptions).ToList();

        return new GetProjectTeamResponse
        {
            IsSubmitted = isSubmitted,
            IsExpired = isExpired,
            ThirdPartyContributionsChanged = projectDetails?.ThirdPartyContributionsChanged,
            CostsChanged = projectDetails?.CostsChanged,
            EndDateSlipped = endDateSlipped,
            TeamMembers = teamMembers,
            MissingRoles = missingRoles,
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber
        };
    }

    public async ValueTask<bool> EndDateHasSlipped(Guid applicationId, int slippageInMonths)
    {
        var endVersionDates = await _paymentRequestRepository.GetPaymentRequestEndVersionDates(applicationId);
        if (endVersionDates == null)
        {
            return false;
        }
        if (endVersionDates.MostRecentVersion > 1)
        {
            if ((endVersionDates.MostRecentEndDate != null) &&
                (endVersionDates.PreviousEndDate != null))
            {
                var monthDifference = ((endVersionDates.MostRecentEndDate.Value.Month + endVersionDates.MostRecentEndDate.Value.Year * 12) -
                                       (endVersionDates.PreviousEndDate.Value.Month + endVersionDates.PreviousEndDate.Value.Year * 12));
                return (Math.Abs(monthDifference) >= slippageInMonths);
            }
        }

        return false;
    }
}
