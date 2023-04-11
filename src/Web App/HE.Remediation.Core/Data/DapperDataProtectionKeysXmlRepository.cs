using System.Xml.Linq;
using HE.Remediation.Core.Extensions;
using HE.Remediation.Core.Interface;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HE.Remediation.Core.Data
{
    public class DapperDataProtectionKeysXmlRepository : IXmlRepository
    {
        private readonly ILogger _logger;
        private readonly IServiceProvider _services;

        public DapperDataProtectionKeysXmlRepository(IServiceProvider services, ILoggerFactory loggerFactory)
        {
            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            _logger = loggerFactory.CreateLogger<DapperDataProtectionKeysXmlRepository>();
            _services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public IReadOnlyCollection<XElement> GetAllElements()
        {
            return GetAllElementsCore().ToList().AsReadOnly();

            IEnumerable<XElement> GetAllElementsCore()
            {
                using var scope = _services.CreateScope();
                var dbWrapper = scope.ServiceProvider.GetRequiredService<IDbConnectionWrapper>();

                var dbQueryTask = Task.Run(async () =>
                    await dbWrapper.QueryAsync<DataProtectionKey>("GetDataProtectionKeys"));
                dbQueryTask.Wait();
                var dataProtectionKeys = dbQueryTask.Result;

                foreach (var dataProtectionKey in dataProtectionKeys)
                {
                    _logger.ReadingXmlFromKey(dataProtectionKey.FriendlyName!, dataProtectionKey.Xml);

                    if (!string.IsNullOrEmpty(dataProtectionKey.Xml))
                    {
                        yield return XElement.Parse(dataProtectionKey.Xml);
                    }
                }
            }

        }

        public void StoreElement(XElement element, string friendlyName)
        {
            using var scope = _services.CreateScope();
            var dbWrapper = scope.ServiceProvider.GetRequiredService<IDbConnectionWrapper>();
            var newKey = new DataProtectionKey
                { FriendlyName = friendlyName, Xml = element.ToString(SaveOptions.DisableFormatting) };

            _logger.LogSavingKeyToDatabase(friendlyName);

            var dbQuery = Task.Run(async () =>
                await dbWrapper.ExecuteAsync("InsertDataProtectionKey", new {newKey.FriendlyName, newKey.Xml}));

            dbQuery.Wait();
        }
    }
}
