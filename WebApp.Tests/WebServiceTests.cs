using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Xunit;

namespace WebApp.Tests;

public class WebServiceTests
{
    // Api URLS for the different domains
    private const string TitleBasicsApiUrl = "http://localhost:5255/api/TitleBasics";
    
    // /api/TitleBaics TESTS

    [Fact]
    public async Task ApiTitleBasics_GetWithNoArguments_ReturnsOkAndAllTitleBasics()
    {
        var (data, statusCode) = await GetArray(TitleBasicsApiUrl);
        
        Assert.Equal(HttpStatusCode.OK, statusCode);
        Assert.Equal(10, data?.Count); // Count is set to 10, since thats the limit we put per page for now
    }
    
    
    
    // ****************
    // HELPER METHODS
    // ****************
    async Task<(JsonArray?, HttpStatusCode)> GetArray(string url)
    {
        var client = new HttpClient();
        var response = client.GetAsync(url).Result;
        var data = await response.Content.ReadAsStringAsync();
        return (JsonSerializer.Deserialize<JsonArray>(data), response.StatusCode);
    }

}