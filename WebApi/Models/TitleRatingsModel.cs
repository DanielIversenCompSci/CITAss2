namespace WebApi.Models;

public class TitleRatingsModel
{
    public string Url { get; set; }
    
    public string TConst  { get; set; }
    
    public float AverageRating { get; set; }
    
    public int NumVotes { get; set; }
}