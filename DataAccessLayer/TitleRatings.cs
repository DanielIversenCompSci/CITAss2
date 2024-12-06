namespace DataAccessLayer;

public class TitleRatings
{
    public string TConst  { get; set; }
    
    public float AverageRating { get; set; }
    
    public int NumVotes { get; set; }
    
    //NAV
    public TitleBasics TitleBasic { get; set; }
}