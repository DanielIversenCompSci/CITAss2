namespace WebApi.Models;

public class UserBookmarksModel
{
    public int UserBookmarksId { get; set; }
    public string Url { get; set; }
    public int UserId { get; set; }
    
    public string TConst  { get; set; }
    
    public string Note { get; set; }
}