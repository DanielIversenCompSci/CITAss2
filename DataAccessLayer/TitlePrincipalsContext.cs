using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

public class TitlePrincipalsContext : DbContext
{
    public void MapTitlePrincipals(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TitlePrincipals>().ToTable("title_principals").HasKey(t => t.TConst);
        modelBuilder.Entity<TitlePrincipals>().ToTable("title_principals").HasKey(t => t.NConst);
        modelBuilder.Entity<TitlePrincipals>().Property(x => x.TConst).HasColumnName("tconst");
        modelBuilder.Entity<TitlePrincipals>().Property(x => x.Ordering).HasColumnName("ordering");
        modelBuilder.Entity<TitlePrincipals>().Property(x => x.NConst).HasColumnName("nconst");
        modelBuilder.Entity<TitlePrincipals>().Property(x => x.Category).HasColumnName("category");
        modelBuilder.Entity<TitlePrincipals>().Property(x => x.Job).HasColumnName("job");
        modelBuilder.Entity<TitlePrincipals>().Property(x => x.Characters).HasColumnName("characters");
    }
}