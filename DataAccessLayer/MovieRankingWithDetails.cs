namespace DataAccessLayer;
using Microsoft.EntityFrameworkCore;

[Keyless]
public class MovieRankingWithDetails
{
    public string Tconst { get; set; }
    public decimal WeightedRating { get; set; }
    public double? AverageRating { get; set; }
    public int NumVotes { get; set; }
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
