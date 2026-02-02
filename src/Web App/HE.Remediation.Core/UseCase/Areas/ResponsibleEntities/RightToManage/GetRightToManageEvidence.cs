using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ResponsibleEntities.RightToManage;

public class GetRightToManageEvidenceHandler : IRequestHandler<GetRightToManageEvidenceRequest, GetRightToManageEvidenceResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IRightToManageRepository _rightToManageRepository;

    public GetRightToManageEvidenceHandler(IApplicationDataProvider applicationDataProvider, IRightToManageRepository rightToManageRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _rightToManageRepository = rightToManageRepository;
    }

    public async ValueTask<GetRightToManageEvidenceResponse> Handle(GetRightToManageEvidenceRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        var files = await _rightToManageRepository.GetRightToManageEvidence(_applicationDataProvider.GetApplicationId());

        return new GetRightToManageEvidenceResponse
        {
            AddedFiles = files.ToList()
        };
    }
}

public class GetRightToManageEvidenceRequest : IRequest<GetRightToManageEvidenceResponse>
{
    private GetRightToManageEvidenceRequest()
    {
    }

    public static readonly GetRightToManageEvidenceRequest Request = new ();
}

public class GetRightToManageEvidenceResponse
{
    public List<FileResult> AddedFiles { get; set; }
}