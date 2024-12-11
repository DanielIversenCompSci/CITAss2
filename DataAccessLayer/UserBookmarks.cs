namespace DataAccessLayer;

public class UserBookmarks
{
    public int UserBookmarksId { get; set; }
    public int UserId { get; set; }
    
    public string TConst  { get; set; }
    
    public string Note { get; set; }
    
    
    
    //NAV
    public Users User { get; set; }
    
    public TitleBasics TitleBasic { get; set; }
    
    
    
}