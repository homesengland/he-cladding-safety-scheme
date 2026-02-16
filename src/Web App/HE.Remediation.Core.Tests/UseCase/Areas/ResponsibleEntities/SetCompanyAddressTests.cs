using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.ReportDetails.SetReportDetails;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.CompanyAddress.GetCompanyAddress;
using HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.CompanyAddress.SetCompanyAddress;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HE.Remediation.Core.Tests.UseCase.Areas.ResponsibleEntities;

public class SetCompanyAddressTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;
    private readonly Mock<IResponsibleEntityRepository> _responsibleEntityRepository;

    private readonly SetCompanyAddressHandler _handler;
        
    public SetCompanyAddressTests()
    {
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _responsibleEntityRepository = new Mock<IResponsibleEntityRepository>(MockBehavior.Strict);

        _handler = new SetCompanyAddressHandler(_connection.Object, 
                                                _applicationDataProvider.Object,
                                                _responsibleEntityRepository.Object);
    }

    [Fact]
    public async Task Handler_Sets_Company_Address_Update()
    {
        //Arrange        
        //_connection.Setup(x => x.ExecuteAsync("InsertResponsibleEntityCompanyAddress", It.IsAny<object>()))                  
        //                        .Returns(Task.CompletedTask)
        //                        .Verifiable();

        _connection.Setup(x => x.ExecuteAsync("UpdateResponsibleEntityCompanyAddress", It.IsAny<object>()))                  
                                .Returns(Task.CompletedTask)
                                .Verifiable();

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();

        CompanyAddressManualDetails companyAddress = new CompanyAddressManualDetails
        {
            NameNumber = "12",
            AddressLine1 = "10 High Road",
            AddressLine2 = null,
            City = "Chelmsford",   
            County = "Essex",
            Postcode = "CM1 2RT",
            CountryId = 1
        };

        _responsibleEntityRepository.Setup(x => x.GetCompanyAddress(It.IsAny<Guid>()))
                                    .ReturnsAsync(companyAddress)
                                    .Verifiable();
                
        //// Act
        var result = await _handler.Handle(new SetCompanyAddressRequest(), CancellationToken.None);

        // Assert
        _connection.Verify();
        _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);
        _connection.Verify(x => x.ExecuteAsync(It.IsAny<string>(), 
                                               It.IsAny<object>()), 
                                               Times.Once);
    }

    [Fact]
    public async Task Handler_Sets_Company_Address_Insert()
    {
        //Arrange        
        _connection.Setup(x => x.ExecuteAsync("InsertResponsibleEntityCompanyAddress", It.IsAny<object>()))
                                .Returns(Task.CompletedTask)
                                .Verifiable();

        _connection.Setup(x => x.ExecuteAsync("UpdateResponsibleEntityCompanyAddressId", It.IsAny<object>()))                  
                                .Returns(Task.CompletedTask)
                                .Verifiable();

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();

        CompanyAddressManualDetails? companyAddress = null;
        _responsibleEntityRepository.Setup(x => x.GetCompanyAddress(It.IsAny<Guid>()))
                                    .ReturnsAsync(companyAddress!)
                                    .Verifiable();
                
        //// Act
        var result = await _handler.Handle(new SetCompanyAddressRequest(), CancellationToken.None);

        // Assert
        _connection.Verify();
        _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);
        _connection.Verify(x => x.ExecuteAsync(It.IsAny<string>(), 
                                               It.IsAny<object>()), 
                                               Times.Exactly(2));
    }
}
