namespace WebApi.Models;

public class UserRatingCreateModel
{
    public string UserId { get; set; }
        
    public string TConst { get; set; }
    
    public float Rating { get; set; }
}