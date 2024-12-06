namespace DataAccessLayer
{
    public class NameBasics
    {
        public string NConst { get; set; } 
        
        public string? PrimaryName { get; set; } 
        
        public string? BirthYear { get; set; } 
        
        public string? DeathYear { get; set; } 
        
        //NAV
        
        public ICollection<ActorRating> ActorRating { get; set; }
        
        public ICollection<KnownForTitle> KnownForTitle { get; set; }
        
        public ICollection<PrimaryProfession> PrimaryProfession { get; set; }
        
        public ICollection<TitlePersonnel> TitlePersonnel { get; set; }
        
        public ICollection<TitlePrincipals> TitlePrincipals { get; set; }
        
        
    }
}