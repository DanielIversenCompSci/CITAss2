namespace DataAccessLayer;

public class TitlePrincipals
{
    public string TConst  { get; set; }
    
    public int Ordering { get; set; }
    
    public string NConst  { get; set; }
    
    public string Category { get; set; }
    
    public string Job { get; set; }
    
    public string Characters { get; set; }
    
    //NAV
    public TitleBasics TitleBasic { get; set; }
    
    public NameBasics NameBasic { get; set; }
    
    
}