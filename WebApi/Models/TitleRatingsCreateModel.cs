namespace WebApi.Models;

public class TitleRatingsCreateModel
{
    public string TConst  { get; set; }
    
    public float AverageRating { get; set; }
    
    public int NumVotes { get; set; }
}