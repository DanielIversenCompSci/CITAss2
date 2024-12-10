namespace DataAccessLayer;

public class KnownForTitle
{
    public string TConst  { get; set; }
    
    public string NConst  { get; set; }
    
    public int KnownForTitleId { get; set; }
    
    //NAV
    public TitleBasics TitleBasic { get; set; }
    
    public NameBasics NameBasic { get; set; }
}