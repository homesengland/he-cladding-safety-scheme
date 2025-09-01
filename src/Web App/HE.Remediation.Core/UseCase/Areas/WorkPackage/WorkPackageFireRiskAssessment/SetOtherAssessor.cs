using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters;
using HE.Remediation.Core.Interface;
using MediatR;

namespace HE.Remediation.Core.UseCase.Areas.WorkPackage.WorkPackageFireRiskAssessment;

public class SetOtherAssessorHandler : IRequestHandler<SetOtherAssessorRequest>
{
    private readonly IApplicationDataProvider _applicationDataProvider;
    private readonly IWorkPackageFireRiskAssessmentRepository _fireRiskAssessmentRepository;

    public SetOtherAssessorHandler(
        IApplicationDataProvider applicationDataProvider, 
        IWorkPackageFireRiskAssessmentRepository fireRiskAssessmentRepository)
    {
        _applicationDataProvider = applicationDataProvider;
        _fireRiskAssessmentRepository = fireRiskAssessmentRepository;
    }

    public async Task<Unit> Handle(SetOtherAssessorRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var applicationId = _applicationDataProvider.GetApplicationId();

        await _fireRiskAssessmentRepository.SetWorkPackageFraOtherAssessor(new SetWorkPackageFraOtherAssessorParameters
        {
            ApplicationId = applicationId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            CompanyName = request.CompanyName,
            CompanyNumber = request.CompanyNumber,
            EmailAddress = request.EmailAddress,
            Telephone = request.Telephone
        });

        return Unit.Value;
    }
}

public class SetOtherAssessorRequest : IRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string CompanyName { get; set; }
    public string CompanyNumber { get; set; }
    public string EmailAddress { get; set; }
    public string Telephone { get; set; }
}