using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Xunit;

public class WebServiceTests
{
    // Api URLS for the different domains
    private const string TitleBasicsApiUrl = "http://localhost:5255/api/TitleBasics";
    private const string NameBasicsApiUrl = "http://localhost:5255/api/NameBasics";
    
    // /api/TitleBaics TESTS
    [Fact]
    public async Task ApiTitleBasics_GetWithNoArguments_ReturnsOkAndAllTitleBasics()
    {
        var (data, statusCode) = await GetArray(TitleBasicsApiUrl);
        
        Assert.Equal(HttpStatusCode.OK, statusCode);
        Assert.NotNull(data);
        Assert.NotEmpty(data);
        Assert.Equal(10, data?.Count); // Count is set to 10, since thats the limit we put per page for now
        Assert.Equal("tt24563474", data?.FirstElement("tConst"));
        Assert.Equal("Ek Tak", data?.LastElement("primaryTitle"));
    }
    
    [Fact]
    public async Task ApiTitleBasics_DeleteWithValidId_ReturnsNoContent()
    {
        var data = new
        {
            TConst = "tt99999999",  // TConst that dosent already exist in the db
            PrimaryTitle = "Created Title",
            TitleType = "Movie",
            IsAdult = false
        };

        var (title, postStatusCode) = await PostData($"{TitleBasicsApiUrl}", data);

        Assert.Equal(HttpStatusCode.Created, postStatusCode);
        Assert.NotNull(title);

        // select id for deletion
        string id = title?.Value("tConst") ?? title?.Value("url")?.Split('/').Last();

        Assert.False(string.IsNullOrEmpty(id), "ID should not be null or empty");

        // delete it
        var deleteStatusCode = await DeleteData($"{TitleBasicsApiUrl}/{id}");
    
        // nocontent means that the deletion was succesful, so we accept it in place of msg "Ok"
        Assert.Equal(HttpStatusCode.NoContent, deleteStatusCode);
    }

    // NameBasics TESTS
    [Fact]
    public async Task ApiNameBaics_ValidNConst_CompleteRecord()
    {
        var (data, statusCode) = await GetObject($"{NameBasicsApiUrl}/nm0062028 ");
        Assert.Equal(HttpStatusCode.OK, statusCode);
        Assert.Equal("Jana Bauke", data?.Value("primaryName"));
        Assert.Equal("1968", data?.Value("birthYear"));
    }
    
    [Fact]
    public async Task ApiNameBaics_InvalidNConst_CompleteRecord()
    {
        var (_, statusCode) = await GetObject($"{NameBasicsApiUrl}/nm-11");
        
        Assert.Equal(HttpStatusCode.NotFound, statusCode);
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
    
    async Task<(JsonObject?, HttpStatusCode)> GetObject(string url)
    {
        var client = new HttpClient();
        var response = client.GetAsync(url).Result;
        var data = await response.Content.ReadAsStringAsync();
        return (JsonSerializer.Deserialize<JsonObject>(data), response.StatusCode);
    }

    async Task<(JsonObject?, HttpStatusCode)> PostData(string url, object content)
    {
        var client = new HttpClient();
        var requestContent = new StringContent(
            JsonSerializer.Serialize(content),
            Encoding.UTF8,
            "application/json");

        var response = await client.PostAsync(url, requestContent);
        var data = await response.Content.ReadAsStringAsync();

        Console.WriteLine($"Post Response Data: {data}");

        return (response.IsSuccessStatusCode ? JsonSerializer.Deserialize<JsonObject>(data) : null, response.StatusCode);
    }

    async Task<HttpStatusCode> DeleteData(string url)
    {
        var client = new HttpClient();
        var response = await client.DeleteAsync(url);
        return response.StatusCode;
    }
}

// ************
// Helper class
// ************
public static class HelperExtensions
{
    public static string? Value(this JsonNode node, string name)
    {
        var value = node[name];
        return value?.ToString();
    }
    
    public static string? FirstElement(this JsonArray node, string name)
    {
        return node.First()?.Value(name);
    }
    
    public static string? LastElement(this JsonArray node, string name)
    {
        return node.Last()?.Value(name);
    }
}