using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

public class TitleGenreContext : DbContext
{
    public void MapTitleGenre(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TitleGenre>().ToTable("title_genre").HasNoKey();
        modelBuilder.Entity<TitleGenre>().Property(x => x.TConst).HasColumnName("tconst");
        modelBuilder.Entity<TitleGenre>().Property(x => x.Genre).HasColumnName("genre");
    }
}