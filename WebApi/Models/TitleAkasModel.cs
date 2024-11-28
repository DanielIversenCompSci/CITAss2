namespace WebApi.Models;

public class TitleAkasModel
{
    public string Url { get; set; }
    
    public string TitleId { get; set; }
    
    public int Ordering { get; set; }
    
    public string Title { get; set; }
    
    public string Region { get; set; }
    
    public string Language { get; set; }
    
    public string Types { get; set; }
    
    public string Attributes { get; set; }
    
    public bool IsOriginalTitle { get; set; }
}