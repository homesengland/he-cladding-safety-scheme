using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.Leaseholder.GetLeaseHolderEvidence;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.CompanyAddress.GetCompanyAddress;
using Moq;
using System.Net;

namespace HE.Remediation.Core.Tests.UseCase.Areas.ResponsibleEntities;

public class GetCompanyAddressTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;
    private readonly Mock<IResponsibleEntityRepository> _responsibleEntityRepository;

    private readonly GetCompanyAddressHandler _handler;
        
    public GetCompanyAddressTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _responsibleEntityRepository = new Mock<IResponsibleEntityRepository>(MockBehavior.Strict);

        _handler = new GetCompanyAddressHandler(_connection.Object, 
                                                _applicationDataProvider.Object,
                                                _responsibleEntityRepository.Object);
    }

    [Fact]
    public async Task Handler_Returns_Data_From_DB()
    {
        //Arrange        
        CompanyAddressManualDetails addressResponse = new CompanyAddressManualDetails
        {
            NameNumber = "12",
            AddressLine1 = "10 High Road",
            AddressLine2 = null,
            City = "Chelmsford",   
            County = "Essex",
            Postcode = "CM1 2RT"
        };

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable(); 

        _responsibleEntityRepository.Setup(x => x.GetCompanyAddress(It.IsAny<Guid>()))
                                .ReturnsAsync(addressResponse)
                                .Verifiable();

        //// Act
        var result = await _handler.Handle(GetCompanyAddressRequest.Request, CancellationToken.None);

        // Assert        
        Assert.NotNull(result);

        var resultValid = ((result != null) &&
                           (result.City == "Chelmsford"));
        Assert.True(resultValid);
        _connection.Verify();
        _applicationDataProvider.Verify();
    }
}
