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
    public DbSet<UserBookmarkings> UserBookmarkings { get; set; }



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
        MapUserBookmarkings(modelBuilder);
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
        modelBuilder.Entity<NameBasics>().ToTable("name_basics").HasKey(i => i.NConst);
        modelBuilder.Entity<NameBasics>().Property(x => x.NConst).HasColumnName("nconst");
        modelBuilder.Entity<NameBasics>().Property(x => x.PrimaryName).HasColumnName("primaryname");
        modelBuilder.Entity<NameBasics>().Property(x => x.BirthYear).HasColumnName("birthyear");
        modelBuilder.Entity<NameBasics>().Property(x => x.DeathYear).HasColumnName("deathyear");

        // Define relationships
        // 1. Relationship with ActorRating
        modelBuilder.Entity<NameBasics>()
            .HasMany(nb => nb.ActorRating) // Navigation property in NameBasics
            .WithOne(ar => ar.NameBasic) // Navigation property in ActorRating
            .HasForeignKey(ar => ar.NConst) // FK in ActorRating
            .OnDelete(DeleteBehavior.Cascade); // Reflect ON DELETE CASCADE

        // 2. Relationship with KnownForTitle
        modelBuilder.Entity<NameBasics>()
            .HasMany(nb => nb.KnownForTitle)
            .WithOne(kft => kft.NameBasic)
            .HasForeignKey(kft => kft.NConst)
            .OnDelete(DeleteBehavior.Cascade);

        // 3. Relationship with PrimaryProfession
        modelBuilder.Entity<NameBasics>()
            .HasMany(nb => nb.PrimaryProfession)
            .WithOne(pp => pp.NameBasic)
            .HasForeignKey(pp => pp.NConst)
            .OnDelete(DeleteBehavior.Cascade);

        // 4. Relationship with TitlePersonnel
        modelBuilder.Entity<NameBasics>()
            .HasMany(nb => nb.TitlePersonnel)
            .WithOne(tp => tp.NameBasic)
            .HasForeignKey(tp => tp.NConst)
            .OnDelete(DeleteBehavior.Cascade);

        // 5. Relationship with TitlePrincipals
        modelBuilder.Entity<NameBasics>()
            .HasMany(nb => nb.TitlePrincipals)
            .WithOne(tp => tp.NameBasic)
            .HasForeignKey(tp => tp.NConst)
            .OnDelete(DeleteBehavior.Cascade);
    }
    private static void MapPrimaryProfession(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PrimaryProfession>().ToTable("primary_profession").HasKey(k => new { k.NConst, k.Role });
        modelBuilder.Entity<PrimaryProfession>().Property(x => x.NConst).HasColumnName("nconst");
        modelBuilder.Entity<PrimaryProfession>().Property(x => x.Role).HasColumnName("profession");
        
        // Define foreign key relation
        modelBuilder.Entity<PrimaryProfession>()
            .HasOne(pp => pp.NameBasic)
            .WithMany(nb => nb.PrimaryProfession)
            .HasForeignKey(pp => pp.NConst)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static void MapSearchHis(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<SearchHis>().ToTable("search_his").HasKey(s => new { s.SearchId }); // Composite primary key
        modelBuilder.Entity<SearchHis>().Property(x => x.UserId).HasColumnName("userid");
        modelBuilder.Entity<SearchHis>().Property(x => x.SearchQuery).HasColumnName("searchquery");
        modelBuilder.Entity<SearchHis>().Property(x => x.SearchTimeStamp).HasColumnName("searchtimestamp");
        modelBuilder.Entity<SearchHis>().Property(x => x.SearchId).HasColumnName("searchhis_id");
        
        // Define cascade behavior and foreignkey aswell as relation
        modelBuilder.Entity<SearchHis>()
            .HasOne<Users>()
            .WithMany(u => u.SearchHistory) // Nav property in Users (Check what its called there)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
    
    private static void MapTitleAkas(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TitleAkas>().ToTable("title_akas").HasKey(k => new { k.TitleId, k.Ordering });
        modelBuilder.Entity<TitleAkas>().Property(x => x.TitleId).HasColumnName("titleid");
        modelBuilder.Entity<TitleAkas>().Property(x => x.Ordering).HasColumnName("ordering");
        modelBuilder.Entity<TitleAkas>().Property(x => x.Title).HasColumnName("title");
        modelBuilder.Entity<TitleAkas>().Property(x => x.Region).HasColumnName("region");
        modelBuilder.Entity<TitleAkas>().Property(x => x.Language).HasColumnName("language");
        modelBuilder.Entity<TitleAkas>().Property(x => x.Types).HasColumnName("types");
        modelBuilder.Entity<TitleAkas>().Property(x => x.Attributes).HasColumnName("attributes");
        modelBuilder.Entity<TitleAkas>().Property(x => x.IsOriginalTitle).HasColumnName("isoriginaltitle");
        
        modelBuilder.Entity<TitleAkas>()
            .HasOne<TitleBasics>()
            .WithMany(u => u.TitleAkas) // Nav property in Users (Check what its called there)
            .HasForeignKey(s => s.TitleId)
            .OnDelete(DeleteBehavior.Cascade);
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
        
        //FK Title Genre
        modelBuilder.Entity<TitleBasics>()
            .HasMany(u => u.TitleGenre)
            .WithOne(s => s.TitleBasic)
            .HasForeignKey(s => s.TConst)
            .OnDelete(DeleteBehavior.Cascade);
        
        //FK User Bookmarkings
        modelBuilder.Entity<TitleBasics>()
            .HasMany(u => u.UserBookmarkings)
            .WithOne(s => s.TitleBasic)
            .HasForeignKey(s => s.TConst)
            .OnDelete(DeleteBehavior.Cascade);
        
        //FK Title Principals
        modelBuilder.Entity<TitleBasics>()
            .HasMany(u => u.TitlePrincipals)
            .WithOne(s => s.TitleBasic)
            .HasForeignKey(s => s.TConst)
            .OnDelete(DeleteBehavior.Cascade);
        
        //FK Title Akas
        modelBuilder.Entity<TitleBasics>()
            .HasMany(u => u.TitleAkas)
            .WithOne(s => s.TitleBasic)
            .HasForeignKey(s => s.TitleId)
            .OnDelete(DeleteBehavior.Cascade);
        
        //FK Title Personnel
        modelBuilder.Entity<TitleBasics>()
            .HasMany(u => u.TitlePersonnel)
            .WithOne(s => s.TitleBasic)
            .HasForeignKey(s => s.TConst)
            .OnDelete(DeleteBehavior.Cascade);
        
        //FK Known For Title
        modelBuilder.Entity<TitleBasics>()
            .HasMany(u => u.KnownForTitle)
            .WithOne(s => s.TitleBasic)
            .HasForeignKey(s => s.TConst)
            .OnDelete(DeleteBehavior.Cascade);
        
        //FK Title Rating
        modelBuilder.Entity<TitleBasics>()
            .HasOne(u => u.TitleRating)
            .WithOne(s => s.TitleBasic)
            .HasForeignKey<TitleRatings>(s => s.TConst)
            .OnDelete(DeleteBehavior.Cascade);
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
        modelBuilder.Entity<TitlePersonnel>().ToTable("title_personnel").HasKey(k => new { k.TConst, k.NConst, k.Role });
        modelBuilder.Entity<TitlePersonnel>().Property(x => x.TConst).HasColumnName("tconst");
        modelBuilder.Entity<TitlePersonnel>().Property(x => x.NConst).HasColumnName("nconst");
        modelBuilder.Entity<TitlePersonnel>().Property(x => x.Role).HasColumnName("role");
    }

    private static void MapTitlePrincipals(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TitlePrincipals>().ToTable("title_principals").HasKey(t => new { t.NConst, t.Category } );
        modelBuilder.Entity<TitlePrincipals>().Property(x => x.TConst).HasColumnName("tconst");
        modelBuilder.Entity<TitlePrincipals>().Property(x => x.Ordering).HasColumnName("ordering");
        modelBuilder.Entity<TitlePrincipals>().Property(x => x.NConst).HasColumnName("nconst");
        modelBuilder.Entity<TitlePrincipals>().Property(x => x.Category).HasColumnName("category");
        modelBuilder.Entity<TitlePrincipals>().Property(x => x.Job).HasColumnName("job");
        modelBuilder.Entity<TitlePrincipals>().Property(x => x.Characters).HasColumnName("characters");
        
        modelBuilder.Entity<TitlePrincipals>()
            .HasOne(u => u.TitleBasic)
            .WithMany(s => s.TitlePrincipals)
            .HasForeignKey(u => u.TConst)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static void MapTitleRatings(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TitleRatings>().ToTable("title_ratings").HasKey(t => t.TConst);
        modelBuilder.Entity<TitleRatings>().Property(x => x.TConst).HasColumnName("tconst");
        modelBuilder.Entity<TitleRatings>().Property(x => x.AverageRating).HasColumnName("averagerating");
        modelBuilder.Entity<TitleRatings>().Property(x => x.NumVotes).HasColumnName("numvotes");
        
        modelBuilder.Entity<TitleRatings>()
            .HasOne(tr => tr.TitleBasic)
            .WithOne(nb => nb.TitleRating)
            .HasForeignKey<TitleRatings>(tr => tr.TConst)
            .OnDelete(DeleteBehavior.Cascade);
    }
    
    private static void MapUsers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Users>().ToTable("users").HasKey(u => u.UserId);
        modelBuilder.Entity<Users>().Property(u => u.UserId).HasColumnName("userid").IsRequired();
        modelBuilder.Entity<Users>().Property(u => u.Email).HasColumnName("email").IsRequired();
        modelBuilder.Entity<Users>().Property(u => u.Password).HasColumnName("password").IsRequired();

        // Configure one-to-many relationship with SearchHistory
        modelBuilder.Entity<Users>()
            .HasMany(u => u.SearchHistory)         // A User has many SearchHistory entries
            .WithOne(s => s.User)                  // Each SearchHis references one User
            .HasForeignKey(s => s.UserId)          // Foreign key in SearchHistory
            .OnDelete(DeleteBehavior.Cascade);     // Optional: Cascade delete

        // Configure one-to-many relationship with UserRatings
        modelBuilder.Entity<Users>()
            .HasMany(u => u.UserRatings)           // A User has many UserRatings
            .WithOne(r => r.User)                  // Each UserRating references one User
            .HasForeignKey(r => r.UserId)          // Foreign key in UserRatings
            .OnDelete(DeleteBehavior.Cascade);     // Optional: Cascade delete

        // Configure one-to-many relationship with UserBookmarkings
        modelBuilder.Entity<Users>()
            .HasMany(u => u.UserBookmarkings)      // A User has many UserBookmarkings
            .WithOne(b => b.User)                  // Each UserBookmarking references one User
            .HasForeignKey(b => b.UserId)          // Foreign key in UserBookmarkings
            .OnDelete(DeleteBehavior.Cascade);     // Optional: Cascade delete
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

    public void MapUserBookmarkings(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserBookmarkings>().ToTable("user_bookmarkings").HasKey(u => u.UserBookmarkingsId);
        modelBuilder.Entity<UserBookmarkings>().Property(x => x.UserId).HasColumnName("userid");
        modelBuilder.Entity<UserBookmarkings>().Property(x => x.TConst).HasColumnName("tconst");
        modelBuilder.Entity<UserBookmarkings>().Property(x => x.Note).HasColumnName("note");
        modelBuilder.Entity<UserBookmarkings>().Property(x => x.UserBookmarkingsId).HasColumnName("userbookmarkings_id");
        
        // PK/FK and cascade behavior
        modelBuilder.Entity<UserBookmarkings>()
            .HasOne<Users>()
            .WithMany(u => u.UserBookmarkings)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
    
    
}