namespace DataAccessLayer;

public class TitlePersonnel
{
    public string TConst  { get; set; }
    
    public string NConst  { get; set; }
    
    public string Role  { get; set; }
    
    //NAV
    public TitleBasics TitleBasic { get; set; }
    
    public NameBasics NameBasic { get; set; }
}