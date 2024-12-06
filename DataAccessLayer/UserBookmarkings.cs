namespace DataAccessLayer;

public class UserBookmarkings
{
    public int UserId { get; set; }
    
    public string TConst  { get; set; }
    
    public string Note { get; set; }
    
    public int UserBookmarkingsId { get; set; }
    
    
    //NAV
    public Users User { get; set; }
    
    public TitleBasics TitleBasic { get; set; }
    
    
    
}