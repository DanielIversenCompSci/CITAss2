using DataLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Db context
namespace PostgresDataService;
internal class ImdbContext : DbContext
{
    public DbSet<TitleBasics> TitleBasics { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        optionsBuilder.UseNpgsql("host=cit.ruc.dk;db=cit04;uid=cit04;pwd=1paLIXo0SHSs");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // title_baics
        MapTitleBasics(modelBuilder);
        
    }

    protected void MapTitleBasics(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TitleBasics>().ToTable("title_basics").HasKey(t => t.TConst);
        modelBuilder.Entity<TitleBasics>().Property(x => x.TConst).HasColumnName("tconst");
        modelBuilder.Entity<TitleBasics>().Property(x => x.TitleType).HasColumnName("titletype");
        modelBuilder.Entity<TitleBasics>().Property(x => x.PrimaryTitle).HasColumnName("primarytitle");
    }
}