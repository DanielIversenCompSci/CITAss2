using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

public class TitlePersonnelContext : DbContext
{
    public void MapTitlePersonnel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TitlePersonnel>().ToTable("title_personnel").HasNoKey();
        modelBuilder.Entity<TitlePersonnel>().Property(x => x.TConst).HasColumnName("tconst");
        modelBuilder.Entity<TitlePersonnel>().Property(x => x.NConst).HasColumnName("nconst");
        modelBuilder.Entity<TitlePersonnel>().Property(x => x.Role).HasColumnName("role");
    }
}