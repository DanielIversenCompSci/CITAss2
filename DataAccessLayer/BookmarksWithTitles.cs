using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccessLayer;

public class BookmarksWithTitles
{
    public int userid { get; set; }
    public int userbookmarkingsid { get; set; }
    public string note {  get; set; }
    public string tconst  { get; set; }
    public string primarytitle { get; set; }
    public string? poster {  get; set; } 
    
    
    
    
    
    
}