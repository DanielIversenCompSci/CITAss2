namespace WebApi.Models;

public class TitleBasicsModel
{
    public string Url { get; set; }  // URL property for navigation
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
}