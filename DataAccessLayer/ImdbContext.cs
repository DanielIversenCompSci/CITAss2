using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Db context
namespace DataAccessLayer;
public class ImdbContext : DbContext
{
    public DbSet<TitleBasics> TitleBasics { get; set; }
    
    public DbSet<TitlePrincipals> TitlePrincipals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        optionsBuilder.UseNpgsql("host=cit.ruc.dk;db=cit04;uid=cit04;pwd=1paLIXo0SHSs");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // title_baics
        MapTitleBasics(modelBuilder);
        
        TitlePrincipalsContext titlePrincipalsContext = new TitlePrincipalsContext();
        titlePrincipalsContext.MapTitlePrincipals(modelBuilder);
    }

    protected void MapTitleBasics(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TitleBasics>().ToTable("title_basics").HasKey(t => t.TConst);
        modelBuilder.Entity<TitleBasics>().Property(x => x.TConst).HasColumnName("tconst");
        modelBuilder.Entity<TitleBasics>().Property(x => x.TitleType).HasColumnName("titletype");
        modelBuilder.Entity<TitleBasics>().Property(x => x.PrimaryTitle).HasColumnName("primarytitle");
        modelBuilder.Entity<TitleBasics>().Property(x => x.OriginalTitle).HasColumnName("originaltitle");
        modelBuilder.Entity<TitleBasics>().Property(x => x.IsAdult).HasColumnName("isadult");
        modelBuilder.Entity<TitleBasics>().Property(x => x.StartYear).HasColumnName("startyear");
        modelBuilder.Entity<TitleBasics>().Property(x => x.EndYear).HasColumnName("endyear");
        modelBuilder.Entity<TitleBasics>().Property(x => x.RuntimeMinutes).HasColumnName("runtimeminutes");
        modelBuilder.Entity<TitleBasics>().Property(x => x.Plot).HasColumnName("plot");
        modelBuilder.Entity<TitleBasics>().Property(x => x.Poster).HasColumnName("poster");
    }
}