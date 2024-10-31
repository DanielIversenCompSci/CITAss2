using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

public class KnownForTitleContext : DbContext
{
    public void MapKnownForTitle(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KnownForTitle>().ToTable("known_for_title").HasNoKey();
        modelBuilder.Entity<KnownForTitle>().Property(x => x.TConst).HasColumnName("tconst");
        modelBuilder.Entity<KnownForTitle>().Property(x => x.NConst).HasColumnName("nconst");
    }
}