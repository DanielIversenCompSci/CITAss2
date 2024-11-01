using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

public class TitleRatingsContext : DbContext
{
    public void MapTitleRatings(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TitleRatings>().ToTable("title_ratings").HasKey(t => t.TConst);
        modelBuilder.Entity<TitleRatings>().Property(x => x.TConst).HasColumnName("tconst");
        modelBuilder.Entity<TitleRatings>().Property(x => x.AverageRating).HasColumnName("averagerating");
        modelBuilder.Entity<TitleRatings>().Property(x => x.NumVotes).HasColumnName("numvotes");
    }
}