namespace WebApi.Models;

public class UserRatingModel
{
    public string Url { get; set; }
    
    public int UserId { get; set; }
        
    public string TConst { get; set; }
        
    public float Rating { get; set; }
}