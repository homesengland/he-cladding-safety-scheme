﻿using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PaymentRequest.GetChangeCladdingRemovedDate;

public class GetChangeCladdingRemovedDateHandler : IRequestHandler<GetChangeCladdingRemovedDateRequest, GetChangeCladdingRemovedDateResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IPaymentRequestRepository _paymentRequestRepository;

    public GetChangeCladdingRemovedDateHandler(IApplicationDataProvider applicationDataProvider,
                                               IApplicationRepository applicationRepository,
                                               IBuildingDetailsRepository buildingDetailsRepository,
                                               IPaymentRequestRepository paymentRequestRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _paymentRequestRepository = paymentRequestRepository;
    }

    public async Task<GetChangeCladdingRemovedDateResponse> Handle(GetChangeCladdingRemovedDateRequest request, CancellationToken cancellationToken)
    {
        var applicationId = _applicationDataProvider.GetApplicationId();
        var paymentRequestId = _applicationDataProvider.GetPaymentRequestId();

        var applicationReferenceNumber = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
        var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

        var projectDetails = await _paymentRequestRepository.GetPaymentRequestProjectDetails(paymentRequestId);

        var isSubmitted = await _paymentRequestRepository.IsPaymentRequestSubmitted(paymentRequestId);
        var isExpired = await _paymentRequestRepository.IsPaymentRequestExpired(paymentRequestId);
        
        return new GetChangeCladdingRemovedDateResponse
        {
            IsSubmitted = isSubmitted,
            IsExpired = isExpired,
            DateRemovedMonth = projectDetails?.UnsafeCladdingRemovedDate?.Month,
            DateRemovedYear = projectDetails?.UnsafeCladdingRemovedDate?.Year,
            BuildingName = buildingName,
            ApplicationReferenceNumber = applicationReferenceNumber            
        };        
    }
}
