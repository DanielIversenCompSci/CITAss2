namespace WebApi.Models;

public class SearchHisModel
{
    public string Url { get; set; }
    
    public string UserId { get; set; }
    
    public string SearchQuery { get; set; }
    
    public DateTime SearchTimeStamp { get; set; }
}