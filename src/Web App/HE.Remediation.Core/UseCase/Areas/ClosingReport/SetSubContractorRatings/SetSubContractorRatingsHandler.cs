using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureParameters.ClosingReport;
using HE.Remediation.Core.Data.StoredProcedureParameters.SubContractorRatings;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using Mediator;

namespace HE.Remediation.Core.UseCase.Areas.ClosingReport.SetSubContractorRatings;

public class SetSubContractorRatingsHandler : IRequestHandler<SetSubContractorRatingsRequest>
{
    private readonly IApplicationDataProvider _adp;
    private readonly ISubContractorSurveyRepository _subContractorSurveyRepository;

    public SetSubContractorRatingsHandler(IApplicationDataProvider adp,
        ISubContractorSurveyRepository subContractorSurveyRepository)
    {
        _adp = adp;
        _subContractorSurveyRepository = subContractorSurveyRepository;
    }

    public async ValueTask<Unit> Handle(SetSubContractorRatingsRequest request, CancellationToken cancellationToken)
    {
        await _subContractorSurveyRepository.UpdateRating(request.Ratings.Id, new UpdateSubcontractorRatingParameters
        {
            QualityOfWork = request.Ratings.QualityOfWork,
            ValueForMoney = request.Ratings.ValueForMoney,
            Reliability = request.Ratings.Reliability,
            ConsiderationOfResidents = request.Ratings.ConsiderationOfResidents,
            OverallSatisfaction = request.Ratings.OverallSatisfaction,
            Status = request.Complete ? ETaskStatus.Completed : ETaskStatus.InProgress
        });

        return Unit.Value;
    }
}