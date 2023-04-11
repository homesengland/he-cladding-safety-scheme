using AutoMapper;
using HE.Remediation.Core.UseCase.Areas.Application.StageDiagram.GetStageDiagram;

namespace HE.Remediation.WebApp.ViewModels.Application;

public class StageDiagramViewModelMapper : Profile
{
    public StageDiagramViewModelMapper()
    {
        CreateMap<GetStageDiagramResponse, StageDiagramViewModel>();
    }
}