using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.CompanyAddress.GetCompanyAddress;
using Mediator;
using System.Transactions;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.CompanyAddress.SetCompanyAddress
{
    public class SetCompanyAddressHandler : IRequestHandler<SetCompanyAddressRequest>
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IResponsibleEntityRepository _responsibleEntityRepository;

        public SetCompanyAddressHandler(IDbConnectionWrapper dbConnectionWrapper, 
                                        IApplicationDataProvider applicationDataProvider,
                                        IResponsibleEntityRepository responsibleEntityRepository)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _applicationDataProvider = applicationDataProvider;
            _responsibleEntityRepository = responsibleEntityRepository;
        }

        public async ValueTask<Unit> Handle(SetCompanyAddressRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var address = await _responsibleEntityRepository.GetCompanyAddress(applicationId);
            if (address is not null)
            {
                await UpdateCompanyAddress(applicationId, request);
                return Unit.Value;
            }

            await InsertCompanyAddress(applicationId, request);

            return Unit.Value;
        }

        private async Task InsertCompanyAddress(Guid applicationId, SetCompanyAddressRequest request)
        {
            var addressId = Guid.NewGuid();

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _dbConnectionWrapper.ExecuteAsync("InsertResponsibleEntityCompanyAddress",
                    new
                    {
                        addressId,
                        request.NameNumber,
                        request.AddressLine1,
                        request.AddressLine2,
                        request.City,
                        request.Postcode,
                        request.County
                    }
                );

                await _dbConnectionWrapper.ExecuteAsync("UpdateResponsibleEntityCompanyAddressId", new { applicationId, addressId });

                scope.Complete();
            }
        }

        private async Task UpdateCompanyAddress(Guid applicationId, SetCompanyAddressRequest request)
        {
            await _dbConnectionWrapper.ExecuteAsync("UpdateResponsibleEntityCompanyAddress",
                new
                {
                    applicationId,
                    request.NameNumber,
                    request.AddressLine1,
                    request.AddressLine2,
                    request.City,
                    request.County,
                    request.Postcode
                });
        }
    }
}
