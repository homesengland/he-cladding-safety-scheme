using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Interface;
using Microsoft.Extensions.Caching.Memory;

namespace HE.Remediation.Core.Providers.ApplicationDetailsProvider;

public class ApplicationDetailsProvider : IApplicationDetailsProvider
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IApplicationRepository _applicationRepository;
    private readonly IBuildingDetailsRepository _buildingDetailsRepository;
    private readonly IMemoryCache _memoryCache;

    private const int SlidingCacheMins = 10;
    private const int AbsoluteCacheMins = 30;


    public ApplicationDetailsProvider(
        IApplicationDataProvider applicationDataProvider, 
        IApplicationRepository applicationRepository, 
        IBuildingDetailsRepository buildingDetailsRepository, 
        IMemoryCache memoryCache)
    {
        _applicationDataProvider = applicationDataProvider;
        _applicationRepository = applicationRepository;
        _buildingDetailsRepository = buildingDetailsRepository;
        _memoryCache = memoryCache;
    }

    public async Task<ApplicationDetailsModel> GetApplicationDetails()
    {
        var applicationId = _applicationDataProvider.GetApplicationId();

        var value = await _memoryCache.GetOrCreateAsync<ApplicationDetailsModel>(applicationId, async cacheEntry =>
        {
            cacheEntry.SlidingExpiration = TimeSpan.FromMinutes(SlidingCacheMins);
            cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(AbsoluteCacheMins);

            var reference = await _applicationRepository.GetApplicationReferenceNumber(applicationId);
            var buildingName = await _buildingDetailsRepository.GetBuildingUniqueName(applicationId);

            var applicationDetails = new ApplicationDetailsModel
            {
                ApplicationId = applicationId,
                ApplicationReferenceNumber = reference,
                BuildingName = buildingName
            };
            return applicationDetails;
        });

        return value;
    }

}