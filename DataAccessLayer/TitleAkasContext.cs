using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

public class TitleAkasContext : DbContext
{
    public void MapTitleAkas(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TitleAkas>().ToTable("title_akas").HasNoKey();
        modelBuilder.Entity<TitleAkas>().Property(x => x.TitleId).HasColumnName("titleid");
        modelBuilder.Entity<TitleAkas>().Property(x => x.Ordering).HasColumnName("ordering");
        modelBuilder.Entity<TitleAkas>().Property(x => x.Title).HasColumnName("title");
        modelBuilder.Entity<TitleAkas>().Property(x => x.Region).HasColumnName("region");
        modelBuilder.Entity<TitleAkas>().Property(x => x.Language).HasColumnName("language");
        modelBuilder.Entity<TitleAkas>().Property(x => x.Types).HasColumnName("types");
        modelBuilder.Entity<TitleAkas>().Property(x => x.Attributes).HasColumnName("attributes");
        modelBuilder.Entity<TitleAkas>().Property(x => x.IsOriginalTitle).HasColumnName("isoriginaltitle");
    }
}