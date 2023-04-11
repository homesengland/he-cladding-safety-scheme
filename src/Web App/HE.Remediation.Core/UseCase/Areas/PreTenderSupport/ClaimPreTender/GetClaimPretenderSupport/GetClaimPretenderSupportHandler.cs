using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.PreTenderSupport.ClaimPreTender.GetClaimPretenderSupport;

public class GetClaimPretenderSupportHandler: IRequestHandler<GetClaimPretenderSupportRequest, GetClaimPretenderSupportResponse>
{
    private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;

        public GetClaimPretenderSupportHandler(IDbConnectionWrapper dbConnectionWrapper, IApplicationDataProvider applicationDataProvider)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
        }

        public async Task<GetClaimPretenderSupportResponse> Handle(GetClaimPretenderSupportRequest request, CancellationToken cancellationToken)
        {           
            return new GetClaimPretenderSupportResponse
            {
                EligibleClaimAmount = 50000.00m
            };
        }
}
