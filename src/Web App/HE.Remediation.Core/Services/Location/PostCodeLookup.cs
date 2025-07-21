using HE.Remediation.Core.Exceptions;
using System.Text;
using System.Text.Json;

namespace HE.Remediation.Core.Services.Location;

public class PostCodeLookup : IPostCodeLookup
{
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly IHttpClientFactory _httpClientFactory;

    public PostCodeLookup(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<PostCodeResults> SearchPostCode(string postCode)
    {           
        return await ObtainSearchResults(postCode);            
    }

    public async Task<PostCodeResults> SearchBuildings(string postcode)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient("ApimClient");
            var httpResponse = await httpClient.GetAsync($"{Environment.GetEnvironmentVariable("APIM_BUILDING_LOOKUP_URI")}?postcode={postcode}");

            await using var responseStream = await httpResponse.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<PostCodeResults>(responseStream, _jsonSerializerOptions);
        }
        catch (Exception ex)
        {
            throw new Exception("Bad call to web service. Message=" + ex.StackTrace);
        }
    }

    private async Task<PostCodeResults> ObtainSearchResults(string searchPostCode)
    {            
        var postcodeJSON = string.Empty;
        try
        {
            postcodeJSON = JsonSerializer.Serialize(new
            {
                postcode = searchPostCode
            });
        }
        catch (Exception ex)
        {
            throw new BadDataException("Error serialising post code. Message=" + ex.Message);
        }

        if (String.IsNullOrWhiteSpace(postcodeJSON))
        {
            return null;
        }

        return await PostToWebServiceAsync(postcodeJSON);        
    }

    private async Task<PostCodeResults> PostToWebServiceAsync(string payload)
    {
        try
        {
            var requestTxt = new StringContent(payload,
                                               Encoding.UTF8,
                                               "application/json");

            var httpClient = _httpClientFactory.CreateClient("ApimClient");
            var httpResponse = await httpClient.PostAsync(Environment.GetEnvironmentVariable("APIM_POST_CODE_URI"), 
                                                          requestTxt);

            await using var responseStream = await httpResponse.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<PostCodeResults>(responseStream, _jsonSerializerOptions);            
        }
        catch (Exception ex)
        {
            throw new Exception("Bad call to web service. Message=" + ex.StackTrace);                
        }
    }
}
