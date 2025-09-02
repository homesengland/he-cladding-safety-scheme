using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Data.StoredProcedureParameters.WorkPackage.InternalDefects;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageInternalDefects.InternalDefectsCost.Set;

public class SetInternalDefectsCostHandler : IRequestHandler<SetInternalDefectsCostRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IWorkPackageRepository _workPackageRepository;
    private readonly IFileService _fileService;
    private readonly IFileRepository _fileRepository;

    public SetInternalDefectsCostHandler(
        IApplicationDataProvider applicationDataProvider,
        IWorkPackageRepository workPackageRepository,
        IFileService fileService,
        IFileRepository fileRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _workPackageRepository = workPackageRepository;
        _fileService = fileService;
        _fileRepository = fileRepository;
    }

    public async Task<Unit> Handle(SetInternalDefectsCostRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();


        await _workPackageRepository.SetInternalDefectsCost(new SetInternalDefectsParameters
        {
            ApplicationId = applicationId,
            InternalDefectsCosts = request.InternalDefectsCosts!.Value,
            Description = request.Description
        });


        return Unit.Value;
    }
}

public class SetInternalDefectsCostRequest : IRequest
{
    public decimal? InternalDefectsCosts { get; set; }
    public string Description { get; set; }
}