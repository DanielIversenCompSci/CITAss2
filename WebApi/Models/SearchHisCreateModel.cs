namespace WebApi.Models;

public class SearchHisCreateModel
{
    public string UserId { get; set; }
    
    public string SearchQuery { get; set; }

    public DateTime SearchTimeStamp { get; set; }
}