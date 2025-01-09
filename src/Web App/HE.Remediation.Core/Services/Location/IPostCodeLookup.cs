
namespace HE.Remediation.Core.Services.Location;

public interface IPostCodeLookup
{
    Task<PostCodeResults> SearchPostCode(string postCode);
    Task<PostCodeResults> SearchBuildings(string postcode);
}
