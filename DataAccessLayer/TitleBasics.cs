namespace DataAccessLayer;

public class TitleBasics
{
    public string TConst { get; set; }
    
    public string? TitleType { get; set; }
    
    public string? PrimaryTitle { get; set; }
    
    public string? OriginalTitle { get; set; }
    
    public bool IsAdult { get; set; }
    
    public string? StartYear { get; set; }
    
    public string? EndYear { get; set; }
    
    public int? RuntimeMinutes { get; set; }
    
    public string? Plot { get; set; }
    
    public string? Poster { get; set; }
    
    //NAV
    
    public ICollection<TitleGenre> TitleGenre { get; set; }
    
    public ICollection<UserBookmarks> UserBookmarks { get; set; }
    
    public ICollection<TitlePrincipals> TitlePrincipals { get; set; }
    
    public ICollection<TitleAkas> TitleAkas { get; set; }
    
    public ICollection<TitlePersonnel> TitlePersonnel { get; set; }
    
    public ICollection<KnownForTitle> KnownForTitle { get; set; }
    
    public TitleRatings TitleRating { get; set; }
}