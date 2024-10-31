using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

public class PrimaryProfessionContext : DbContext
{
    public void MapPrimaryProfession(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PrimaryProfession>().ToTable("primary_profession").HasNoKey();
        modelBuilder.Entity<PrimaryProfession>().Property(x => x.NConst).HasColumnName("nconst");
        modelBuilder.Entity<PrimaryProfession>().Property(x => x.Role).HasColumnName("profession");
    }
}