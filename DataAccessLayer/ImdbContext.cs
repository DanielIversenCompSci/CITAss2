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
    public ImdbContext(DbContextOptions<ImdbContext> options) : base(options) { }
    public DbSet<TitleBasics> TitleBasics { get; set; }    
    public DbSet<TitlePrincipals> TitlePrincipals { get; set; }
    public DbSet<TitleAkas> TitleAkas { get; set; }
    public DbSet<NameBasics> NameBasics { get; set; }
    public DbSet<Users> Users { get; set; }
    public DbSet<TitlePersonnel> TitlePersonnel { get; set; }
    public DbSet<KnownForTitle> KnownForTitle { get; set; }
    public DbSet<PrimaryProfession> PrimaryProfession { get; set; }    
    public DbSet<ActorRating> ActorRating { get; set; }    
    public DbSet<TitleGenre> TitleGenre { get; set; }
    public DbSet<TitleRatings> TitleRatings { get; set; }
    public DbSet<SearchHis> SearchHis { get; set; }
    public DbSet<UserRating> UserRating { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        optionsBuilder.UseNpgsql("host=cit.ruc.dk;db=cit04;uid=cit04;pwd=1paLIXo0SHSs");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        MapActorRating(modelBuilder);
        MapKnownForTitle(modelBuilder);
        MapNameBasics(modelBuilder);
        MapPrimaryProfession(modelBuilder);
        MapSearchHis(modelBuilder);
        MapTitleAkas(modelBuilder);
        MapTitleBasics(modelBuilder);
        MapTitleGenre(modelBuilder);
        MapTitlePersonnel(modelBuilder);
        MapTitlePrincipals(modelBuilder);
        MapTitleRatings(modelBuilder);
        MapUsers(modelBuilder);
        MapUserRating(modelBuilder);
    }

    private static void MapActorRating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActorRating>().ToTable("actor_rating").HasKey(x => x.NConst);
        modelBuilder.Entity<ActorRating>().Property(x => x.NConst).HasColumnName("nconst");
        modelBuilder.Entity<ActorRating>().Property(x => x.ARating).HasColumnName("arating");
    }

    private static void MapKnownForTitle(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KnownForTitle>().ToTable("known_for_title").HasKey(k => new {k.TConst, k.NConst});
        modelBuilder.Entity<KnownForTitle>().Property(x => x.TConst).HasColumnName("tconst");
        modelBuilder.Entity<KnownForTitle>().Property(x => x.NConst).HasColumnName("nconst");
    }

    private static void MapNameBasics(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NameBasics>().ToTable("name_basics").HasKey(i => i.Nconst);
        modelBuilder.Entity<NameBasics>().Property(x => x.Nconst).HasColumnName("nconst");
        modelBuilder.Entity<NameBasics>().Property(x => x.PrimaryName).HasColumnName("primaryname");
        modelBuilder.Entity<NameBasics>().Property(x => x.BirthYear).HasColumnName("birthyear");
        modelBuilder.Entity<NameBasics>().Property(x => x.DeathYear).HasColumnName("deathyear");

    }
    private static void MapPrimaryProfession(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PrimaryProfession>().ToTable("primary_profession").HasNoKey();
        modelBuilder.Entity<PrimaryProfession>().Property(x => x.NConst).HasColumnName("nconst");
        modelBuilder.Entity<PrimaryProfession>().Property(x => x.Role).HasColumnName("profession");
    }

    private static void MapSearchHis(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<SearchHis>().ToTable("search_his").HasKey(s => new { s.SearchId }); // Composite primary key
        modelBuilder.Entity<SearchHis>().Property(x => x.UserId).HasColumnName("userid");
        modelBuilder.Entity<SearchHis>().Property(x => x.SearchQuery).HasColumnName("searchquery");
        modelBuilder.Entity<SearchHis>().Property(x => x.SearchTimeStamp).HasColumnName("searchtimestamp");
        modelBuilder.Entity<SearchHis>().Property(x => x.SearchId).HasColumnName("search_id");
        
        // Define cascade behavior and foreignkey aswell as relation
        modelBuilder.Entity<SearchHis>()
            .HasOne<Users>()
            .WithMany(u => u.SearchHistory) // Nav property in Users (Check what its called there)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
    
    private static void MapTitleAkas(ModelBuilder modelBuilder)
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

    private static void MapTitleBasics(ModelBuilder modelBuilder)
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

    private static void MapTitleGenre(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TitleGenre>().ToTable("title_genre")
        .HasKey(t => new { t.TConst, t.Genre }); // Define composite primary key

        modelBuilder.Entity<TitleGenre>().Property(x => x.TConst).HasColumnName("tconst");
        modelBuilder.Entity<TitleGenre>().Property(x => x.Genre).HasColumnName("genre");
    }

    private static void MapTitlePersonnel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TitlePersonnel>().ToTable("title_personnel").HasNoKey();
        modelBuilder.Entity<TitlePersonnel>().Property(x => x.TConst).HasColumnName("tconst");
        modelBuilder.Entity<TitlePersonnel>().Property(x => x.NConst).HasColumnName("nconst");
        modelBuilder.Entity<TitlePersonnel>().Property(x => x.Role).HasColumnName("role");
    }

    private static void MapTitlePrincipals(ModelBuilder modelBuilder)
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

    private static void MapTitleRatings(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TitleRatings>().ToTable("title_ratings").HasKey(t => t.TConst);
        modelBuilder.Entity<TitleRatings>().Property(x => x.TConst).HasColumnName("tconst");
        modelBuilder.Entity<TitleRatings>().Property(x => x.AverageRating).HasColumnName("averagerating");
        modelBuilder.Entity<TitleRatings>().Property(x => x.NumVotes).HasColumnName("numvotes");
    }
    private static void MapUsers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Users>().ToTable("users").HasKey(i => i.UserId);
        modelBuilder.Entity<Users>().Property(x => x.UserId).HasColumnName("userid");
        modelBuilder.Entity<Users>().Property(x => x.Email).HasColumnName("email");
        modelBuilder.Entity<Users>().Property(x => x.Password).HasColumnName("password");
        modelBuilder.Entity<Users>().HasMany(u => u.SearchHistory).WithOne().HasForeignKey(s => s.UserId).OnDelete(DeleteBehavior.Cascade);

    }
    public void MapUserRating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<UserRating>().ToTable("user_rating").HasKey(s => new { s.UserRatingId }); // Composite primary key
        modelBuilder.Entity<UserRating>().Property(x => x.UserId).HasColumnName("userid");
        modelBuilder.Entity<UserRating>().Property(x => x.TConst).HasColumnName("tconst");
        modelBuilder.Entity<UserRating>().Property(x => x.Rating).HasColumnName("userrating");
        modelBuilder.Entity<UserRating>().Property(x => x.UserRatingId).HasColumnName("userrating_id");
        
        // Define cascade behavior and Fk and rel
        modelBuilder.Entity<UserRating>()
            .HasOne<Users>()
            .WithMany(u => u.UserRatings)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
      

    }

    
    
}