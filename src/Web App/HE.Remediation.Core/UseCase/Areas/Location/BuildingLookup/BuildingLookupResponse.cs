using HE.Remediation.Core.Services.Location;
using System.Text.Json;

namespace HE.Remediation.Core.UseCase.Areas.Location.BuildingLookup
{
    public class BuildingLookupResponse
    {
        public string PostCode { get; set; }

        public bool HaveResults { get; set; }

        /// <summary>
        /// We directly bind to this list on the VM - it is preformatted to keep our view simple
        /// and for future object bindings
        /// </summary>
        public List<KeyValuePair<string, string>> OutputLocations { get; set; } = new List<KeyValuePair<string, string>>();

        public BuildingLookupResponse(PostCodeResults results)
        {
            if (results?.Locations != null)
            {
                // Generate the text in the drop down (VM) based on the fields we want, comma separated
                foreach (PostCodeResult postCodeResult in results.Locations)
                {
                    var value = JsonSerializer.Serialize(postCodeResult);

                    OutputLocations.Add(new KeyValuePair<string, string>(postCodeResult.Address, value));
                }

                // If we have a post code entry, then extract out the post code and use this instead of our entry            
                if (results.Locations.Count > 0)
                {
                    PostCode = results.Locations[0].Postcode;
                }
                HaveResults = (OutputLocations.Count > 0);
            }
        }
    }
}
