using HE.Remediation.Core.Data.Repositories;
using HE.Remediation.Core.Data.StoredProcedureResults;
using HE.Remediation.Core.Enums;
using HE.Remediation.Core.Interface;
using HE.Remediation.Core.Services.PdfRendererService;
using HE.Remediation.Core.Services.RazorRenderer.Models;
using HE.Remediation.Core.UseCase.Areas.FireRiskAppraisal.FireRiskAssessorListPdf;
using MediatR;
using Syncfusion.Pdf.Graphics;


namespace HE.Remediation.Core.UseCase.Areas.CostSchedulePdf
{
    public class CostSchedulePdfHandler : IRequestHandler<CostSchedulePdfRequest, byte[]>
    {
        private readonly IApplicationDataProvider _applicationDataProvider;
        private readonly IApplicationRepository _applicationRepository;
        private readonly IBuildingDetailsRepository _buildingDetailsRepository;
        private readonly IWorkPackageRepository _workPackageRepository;
        private readonly IPdfRenderer _pdfRenderer;

        public CostSchedulePdfHandler(
        IApplicationDataProvider applicationDataProvider,
        IApplicationRepository applicationRepository,
        IBuildingDetailsRepository buildingDetailsRepository,
        IWorkPackageRepository workPackageRepository,
        IPdfRenderer pdfRenderer)
        {
            _applicationDataProvider = applicationDataProvider;
            _applicationRepository = applicationRepository;
            _buildingDetailsRepository = buildingDetailsRepository;
            _workPackageRepository = workPackageRepository;
            _pdfRenderer = pdfRenderer;
        }

        public async Task<byte[]> Handle(CostSchedulePdfRequest request, CancellationToken cancellationToken)
        {
            var applicationId = _applicationDataProvider.GetApplicationId();

            var costs = await _workPackageRepository.GetLatestCostSchedule(applicationId);

            var viewModels = new CostScheduleViewModel()
            {
                UnsafeCladdingRemovalAmount = costs.RemovalOfCladdingAmount,
                UnsafeCladdingRemovalDescription = costs.RemovalOfCladdingDescription,

                NewCladdingAmount = costs.NewCladdingAmount,
                NewCladdingDescription = costs.NewCladdingDescription,
                ExternalWorksAmount = costs.OtherEligibleWorkToExternalWallAmount,
                ExternalWorksDescription = costs.OtherEligibleWorkToExternalWallDescription,
                InternalWorksAmount = costs.InternalMitigationWorksAmount,
                InternalWorksDescription = costs.InternalMitigationWorksDescription,

                MainContractorPreliminariesAmount = costs.MainContractorPreliminariesAmount,
                MainContractorPreliminariesDescription = costs.MainContractorPreliminariesDescription,
                AccessAmount = costs.AccessAmount,
                AccessDescription = costs.AccessDescription,
                MainContractorOverheadAmount = costs.OverheadsAndProfitAmount,
                MainContractorOverheadDescription = costs.OverheadsAndProfitDescription,
                ContractorContingenciesAmount = costs.ContractorContingenciesAmount,
                ContractorContingenciesDescription = costs.ContractorContingenciesDescription,

                FraewSurveyAmount = costs.FraewSurveyAmount,
                FeasibilityStageAmount = costs.FeasibilityStageAmount,
                FeasibilityStageDescription = costs.FeasibilityStageDescription,
                PostTenderStageAmount = costs.PostTenderStageAmount,
                PostTenderStageDescription = costs.PostTenderStageDescription,
                PropertyManagerAmount = costs.PropertyManagerAmount,
                PropertyManagerDescription = costs.PropertyManagerDescription,
                IrrecoverableVatAmount = costs.IrrecoverableVatAmount,
                IrrecoverableVatDescription = costs.IrrecoverableVatDescription,
                IneligibleAmount = costs.IneligibleAmount
            };

            var pdf = await _pdfRenderer.RenderPdf(
                Path.Combine("Views", "Pdf", "CostSchedule.cshtml"),
                viewModels,
                new PdfRenderOptions
                {
                    Margins = new PdfMargins
                    {
                        All = 40f
                    }
                });

            return pdf;
        }
    }
}