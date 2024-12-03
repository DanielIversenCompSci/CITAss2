namespace WebApi.Models;

public class UserBookmarkingsCreateModel
{
    public int UserId { get; set; }
    
    public string TConst  { get; set; }
    
    public string Note { get; set; }
}