using System.Transactions;
using Azure.Core;
using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.FileService;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.FireRiskAssessment;

public class SetHasFraHandler : IRequestHandler<SetHasFraRequest, SetHasFraResponse>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IFireRiskAssessmentRepository _fireRiskAssessmentRepository;
    private readonly IFileRepository _fileRepository;
    private readonly IFileService _fileService;

    public SetHasFraHandler(
        IApplicationDataProvider applicationDataProvider, 
        IFireRiskAssessmentRepository fireRiskAssessmentRepository, 
        IFileRepository fileRepository, 
        IFileService fileService)
    {
        _applicationDataProvider = applicationDataProvider;
        _fireRiskAssessmentRepository = fireRiskAssessmentRepository;
        _fileRepository = fileRepository;
        _fileService = fileService;
    }

    public async Task<SetHasFraResponse> Handle(SetHasFraRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        var currentHasFra = await _fireRiskAssessmentRepository.GetHasFra(applicationId);
        var visitedCheckYourAnswers = await _fireRiskAssessmentRepository.GetFraVisitedCheckYourAnswers(applicationId);

        var response = new SetHasFraResponse
        {
            VisitedCheckYourAnwers = visitedCheckYourAnswers
        };

        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        if (currentHasFra == true && request.HasFra == false)
        {
            await ClearAnswers(applicationId);
        }

        await _fireRiskAssessmentRepository.SetHasFra(new SetHasFraParameters
        {
            ApplicationId = applicationId,
            HasFra = request.HasFra!.Value
        });

        await _fireRiskAssessmentRepository.SetFraTaskStatus(new SetFraTaskStatusParameters
        {
            ApplicationId = applicationId,
            TaskStatusId = (int)ETaskStatus.InProgress
        });

        if (currentHasFra == false && request.HasFra == true)
        {
            await _fireRiskAssessmentRepository.SetFraVisitedCheckYourAnswers(
                new SetFraVisitedCheckYourAnswersParameters
                {
                    ApplicationId = applicationId,
                    VisitedCheckYourAnswers = false
                });

            response.VisitedCheckYourAnwers = false;
        }

        scope.Complete();

        return response;
    }

    private async Task ClearAnswers(Guid applicationId)
    {
        await _fireRiskAssessmentRepository.ClearFraAnswers(applicationId);
        var file = await _fireRiskAssessmentRepository.GetFireRiskAssessmentForApplication(applicationId);
        if (file.AddedFra?.Id is not null)
        {
            await _fireRiskAssessmentRepository.DeleteFraForApplication(new DeleteFraForApplicationParameters
            {
                ApplicationId = applicationId,
                FileId = file.AddedFra.Id
            });

            var result = await _fileRepository.DeleteFile(file.AddedFra.Id);

            await _fileService.DeleteFile($"{file.AddedFra.Id}{result.Extension}");

        }
    }
}

public class SetHasFraRequest : IRequest<SetHasFraResponse>
{
    public bool? HasFra { get; set; }
}

public class SetHasFraResponse
{
    public bool VisitedCheckYourAnwers { get; set; }
}