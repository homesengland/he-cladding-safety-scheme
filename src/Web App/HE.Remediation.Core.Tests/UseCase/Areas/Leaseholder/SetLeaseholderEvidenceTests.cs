using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using HE.Remediation.Core.Settings;
using HE.Remediation.Core.UseCase.Areas.Leaseholder.SetLeaseholderEvidence;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;

namespace HE.Remediation.Core.Tests.UseCase.Areas.Leaseholder;

public class SetLeaseholderEvidenceTests
{
    private readonly Mock<IApplicationDataProvider> _applicationDataProvider;
    private readonly Mock<IDbConnectionWrapper> _connection;
    private readonly Mock<IFileService> _fileService;
    private readonly Mock<IFileRepository> _fileRepository;
    private readonly Mock<IOptions<FileServiceSettings>> _fileServiceSettings;

    private readonly SetLeaseholderEvidenceHandler _handler;
        
    public SetLeaseholderEvidenceTests()
    {       
        _connection = new Mock<IDbConnectionWrapper>(MockBehavior.Strict);
        _applicationDataProvider = new Mock<IApplicationDataProvider>(MockBehavior.Strict);
        _fileService = new Mock<IFileService>(MockBehavior.Strict);
        _fileRepository = new Mock<IFileRepository>(MockBehavior.Strict);
        _fileServiceSettings = new Mock<IOptions<FileServiceSettings>>(MockBehavior.Strict);

        _fileServiceSettings.Setup(x => x.Value).Returns(new FileServiceSettings());
                
        _handler = new SetLeaseholderEvidenceHandler(_connection.Object,
                                                     _applicationDataProvider.Object,
                                                     _fileService.Object,
                                                     _fileRepository.Object,
                                                     _fileServiceSettings.Object);
    }

    [Fact]
    public async Task Handler_Sets_Lease_Holder_Evidence_Completed_To_False()
    {
        //Arrange        
        Guid engagementId = Guid.NewGuid();
        //_connection.Setup(x => x.QuerySingleOrDefaultAsync<bool>("CheckCanCompleteLeaseHolderEngagement", It.IsAny<object>()))                  
        //                        .ReturnsAsync(true)
        //                        .Verifiable();

        _connection.Setup(x => x.QuerySingleOrDefaultAsync<Guid?>("GetLeaseHolderEngagementIdForApplication", It.IsAny<object>()))                  
                                .ReturnsAsync(engagementId)
                                .Verifiable();

        _connection.Setup(x => x.ExecuteAsync("InsertLeaseHolderEngagementFile", It.IsAny<object>()))                  
                                .Returns(Task.CompletedTask)
                                .Verifiable();

        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();

        _fileRepository.Setup(x => x.InsertFile(It.IsAny<InsertFileParameters>()))
                       .Returns(Task.CompletedTask)
                       .Verifiable();  

        ProcessFileResult fileResult = new ProcessFileResult();

        
        _fileService.Setup(x => x.ProcessFile(It.IsAny<IFormFile>(), It.IsAny<UploadSectionSettings>()))
                    .ReturnsAsync(fileResult)
                    .Verifiable();

        using var stream = new MemoryStream(new byte[] 
        { 
            0x41, 
            0x42, 
            0x43
        });
        var formFile = new FormFile(stream, 0, stream.Length, "streamFile", "test");

        //// Act
        var result = await _handler.Handle(new SetLeaseHolderEvidenceRequest
        {
            Completed = false,
            File = formFile
        }, CancellationToken.None);

        // Assert
        _connection.Verify();
        _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);
        _connection.Verify(x => x.ExecuteAsync(It.IsAny<string>(), 
                                               It.IsAny<object>()), 
                                               Times.Once);
    }

    [Fact]
    public async Task Handler_Sets_Lease_Holder_Evidence_Completed_To_True()
    {
        //Arrange        
        Guid engagementId = Guid.NewGuid();
        _connection.Setup(x => x.QuerySingleOrDefaultAsync<bool>("CheckCanCompleteLeaseHolderEngagement", It.IsAny<object>()))
                                .ReturnsAsync(true)
                                .Verifiable();
        
        _applicationDataProvider.Setup(x => x.GetApplicationId())
                                .Returns(Guid.NewGuid())
                                .Verifiable();

        using var stream = new MemoryStream(new byte[] 
        { 
            0x41, 
            0x42, 
            0x43
        });
        var formFile = new FormFile(stream, 0, stream.Length, "streamFile", "test");

        //// Act
        var result = await _handler.Handle(new SetLeaseHolderEvidenceRequest
        {
            Completed = true,
            File = formFile
        }, CancellationToken.None);

        // Assert
        _connection.Verify();
        _applicationDataProvider.Verify(x => x.GetApplicationId(), Times.Once);
        _connection.Verify(x => x.QuerySingleOrDefaultAsync<bool>(It.IsAny<string>(), 
                                                            It.IsAny<object>()), 
                                                            Times.Once);
    }
}
