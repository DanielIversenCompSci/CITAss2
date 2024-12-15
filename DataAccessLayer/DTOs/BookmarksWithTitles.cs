using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccessLayer;

public class BookmarksWithTitles
{
    public int UserId { get; set; }
    public int UserBookmarksId { get; set; }
    public string Note {  get; set; }
    public string TConst  { get; set; }
    public string PrimaryTitle { get; set; }
    public string? Poster {  get; set; } 
}