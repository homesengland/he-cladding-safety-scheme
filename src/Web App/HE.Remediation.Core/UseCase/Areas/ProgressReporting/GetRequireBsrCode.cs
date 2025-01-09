using HE.Remediation.Core.Data.Repositories;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting;

public class GetRequireBsrCodeHandler : IRequestHandler<GetRequireBsrCodeRequest, GetRequireBsrCodeResponse>
{
    private readonly IProgressReportingRepository _progressReportingRepository;

    public GetRequireBsrCodeHandler(IProgressReportingRepository progressReportingRepository)
    {
        _progressReportingRepository = progressReportingRepository;
    }

    public async Task<GetRequireBsrCodeResponse> Handle(GetRequireBsrCodeRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var showBsr = await _progressReportingRepository.GetProgressReportShowBuildingSafetyRegulatorRegistrationCode();
        var version = await _progressReportingRepository.GetProgressReportVersion();

        return new GetRequireBsrCodeResponse
        {
            ShowBsrCode = showBsr,
            Version = version
        };
    }
}

public class GetRequireBsrCodeRequest : IRequest<GetRequireBsrCodeResponse>
{
    private GetRequireBsrCodeRequest()
    {
    }

    public static readonly GetRequireBsrCodeRequest Request = new();
}

public class GetRequireBsrCodeResponse
{
    public bool ShowBsrCode { get; set; }
    public int Version { get; set; }
}