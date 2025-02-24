﻿using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetAddRole;

public class GetAddRoleHandler : IRequestHandler<GetAddRoleRequest, GetAddRoleResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IWorkPackageRepository _workPackageRepository;
    private readonly IPaymentRequestRepository _paymentRequestRepository;

    public GetAddRoleHandler(IApplicationDataProvider applicationDataProvider,
                             IBuildingDetailsRepository buildingDetailsRepository,
                             IApplicationRepository applicationRepository,
                             IWorkPackageRepository workPackageRepository,
                             IPaymentRequestRepository paymentRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _buildingDetailsRepository = buildingDetailsRepository;
        _applicationRepository = applicationRepository;
        _workPackageRepository = workPackageRepository;
        _paymentRequestRepository = paymentRequestRepository;   
    }

    public async Task<GetAddRoleResponse> Handle(GetAddRoleRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var paymentRequestId = _applicationDataProvider.GetPaymentRequestId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var teamMembers = await _workPackageRepository.GetTeamMembers();

        var allOptions = Enum.GetValues<ETeamRole>().ToList();
        var consumedOptions = teamMembers.Select(tm => (ETeamRole)tm.Role);
        var availableOptions = allOptions.Except(consumedOptions).ToList();

        // Other should always be available.
        if (!availableOptions.Contains(ETeamRole.Other))
        {
            availableOptions.Add(ETeamRole.Other);
        }

        var isSubmitted = await _paymentRequestRepository.IsPaymentRequestSubmitted(paymentRequestId);
        var isExpired = await _paymentRequestRepository.IsPaymentRequestExpired(paymentRequestId);

        return new GetAddRoleResponse
        {
            IsSubmitted = isSubmitted,
            IsExpired = isExpired,
            AvailableTeamRoles = availableOptions,
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber            
        };
    }
}
