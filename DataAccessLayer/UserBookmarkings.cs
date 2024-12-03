namespace DataAccessLayer;

public class UserBookmarkings
{
    public int UserId { get; set; }
    
    public string TConst  { get; set; }
    
    public string Note { get; set; }
    
    public int UserBookmarkingsId { get; set; }
    
    public Users User { get; set; }
    
}