namespace WebApi.Models;

public class UserRatingModel
{
    public string Url { get; set; }
    
    public string UserId { get; set; }
        
    public string TConst { get; set; }
        
    public float Rating { get; set; }
}