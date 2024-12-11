namespace WebApi.Models;

public class UserBookmarksCreateModel
{
    public int UserId { get; set; }
    
    public string TConst  { get; set; }
    
    public string Note { get; set; }
}