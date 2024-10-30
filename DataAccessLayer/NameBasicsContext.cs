using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class NameBasicsContext : DbContext
    {
        public void MapNameBasics(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NameBasics>().ToTable("name_basics").HasKey(i => i.Nconst);
            modelBuilder.Entity<NameBasics>().Property(x => x.Nconst).HasColumnName("nconst");
            modelBuilder.Entity<NameBasics>().Property(x => x.PrimaryName).HasColumnName("primaryname");
            modelBuilder.Entity<NameBasics>().Property(x => x.BirthYear).HasColumnName("birthyear");
            modelBuilder.Entity<NameBasics>().Property(x => x.DeathYear).HasColumnName("deathyear");

        }
    }
}