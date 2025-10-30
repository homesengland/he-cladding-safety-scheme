namespace HE.Remediation.Core.Data.Repositories;
public interface IWorksPackageMemorandumRepository
{
    Task CreateWorkPackageMemorandum(Guid applicationId);
}
