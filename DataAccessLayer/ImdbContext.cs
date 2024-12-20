using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DTOs;
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
    public DbSet<UserBookmarks> UserBookmarks { get; set; }
    
    // Combined DTOs
    public DbSet<NameWithRating> NameWithRatings { get; set; }

    public DbSet<MovieRankingWithDetails> MovieRankingWithDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        optionsBuilder.UseNpgsql("host=cit.ruc.dk;db=cit04;uid=cit04;pwd=1paLIXo0SHSs");
    }
    
    // ********** // **********
    // SQL Stored functions realted to 'Names'
    // ********** // **********
    // **********
    // Map SQL function for top100 Actors
    // **********
    public IQueryable<NameWithRating> GetTopRatedNames()
    {
        // This is a marker for the stored SQL function
        return FromExpression(() => GetTopRatedNames());
    }
    
    // **********
    // Map SQL function for top 100 actors search w substring
    // **********
    public IQueryable<NameWithRating> GetTopRatedNamesSub(string substring_filter)
    {
        // This is a marker for the stored SQL function
        return FromExpression(() => GetTopRatedNamesSub(substring_filter));
    }
    
    // **********
    // Map SQL function for get actor pr nconst
    // **********
    public IQueryable<NameWithRating> GetNameByNConstSQL(string nconst_param)
    {
        return FromExpression(() => GetNameByNConstSQL(nconst_param));
    }
    
    // **********
    // Map SQL function for getting the cast for a movie
    // **********
    public IQueryable<MovieCast> GetMovieCast(string tconst)
    {
        return FromExpression(() => GetMovieCast(tconst));
    }
    
    // **********
    // Map SQL stored function for getting CoPlayers for a actor
    // **********
    public IQueryable<CoPlayer> GetCoPlayers(string actor_name)
    {
        return FromExpression(() => GetCoPlayers(actor_name));
    }
    
    
    // ********** // **********
    // SQL Stored functions realted to 'Titles'
    // ********** // **********
    // **********
    // Map SQL function for top-rated movies
    // **********
    public IQueryable<MovieRankingWithDetails> GetTopRatedMovies(string titleType)
    {
        // This is a marker for the stored SQL function
        return FromExpression(() => GetTopRatedMovies(titleType));
    }
    
    // **********
    // Map SQL function for get similar movies
    // **********
    public IQueryable<SimilarMovie> GetSimilarMovies(string tconst)
    {
        return FromExpression(() => GetSimilarMovies(tconst));
    }
    
    // **********
    // Map SQL function for get bookmarks w titles
    // **********
    public IQueryable<BookmarksWithTitles> GetBookmarksWithTitles(int user_id)
    {
        return FromExpression(() => GetBookmarksWithTitles(user_id));
    }
    
    // **********
    // Map SQL function for top 5 movies by genre
    // **********
    public IQueryable<MovieRankingByGenre> GetMovieRankingByGenre(string genre_param)
    {
        return FromExpression(() => GetMovieRankingByGenre(genre_param));
    }
    
    // **********
    // Map SQL function for top 20 rated movies search with substring
    // **********
    public IQueryable<MovieRankingByGenre> GetTopRatedMoviesSub(string search_text)
    {
        // This is a marker for the stored SQL function
        return FromExpression(() => GetTopRatedMoviesSub(search_text));
    }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // ********** // **********
        // SQL Stored functions realted to 'Names'
        // ********** // **********
        // **********
        // Map SQL function for top100 Actors
        // **********
        modelBuilder.Entity<NameWithRating>().HasNoKey();
        modelBuilder
            .HasDbFunction(() => GetTopRatedNames())
            .HasName("gettopratednames") // Name of the function in the database
            .HasSchema("public");
        
        // **********
        // Map SQL function for top 100 actors search w substring
        // **********
        modelBuilder
            .HasDbFunction(() => GetTopRatedNamesSub(default))
            .HasName("gettopratednames_sub")
            .HasSchema("public")
            .HasParameter("substring_filter"); // Match the C# parameter name to the SQL parameter name
        
        // **********
        // Map SQL function for get actor pr nconst
        // **********
        modelBuilder
            .HasDbFunction(() => GetNameByNConstSQL(default))
            .HasName("get_person_details")
            .HasSchema("public")
            .HasParameter("nconst_param", ParameterBuilder => { });
        
        // **********
        // Map SQL function for get MovieCast for a Title
        // **********
        modelBuilder.Entity<MovieCast>().HasNoKey(); // This result set has no primary key
        modelBuilder
            .HasDbFunction(() => GetMovieCast(default))
            .HasName("get_movie_cast") // Name of the stored function
            .HasSchema("public")       // Schema where the function resides
            .HasParameter("tconst", parameterBuilder => { });
        
        // **********
        // Map SQL function for get CoPlayers for a Actor
        // **********
        modelBuilder.Entity<CoPlayer>().HasNoKey();
        modelBuilder
            .HasDbFunction(() => GetCoPlayers(default))
            .HasName("find_co_players")
            .HasSchema("public")
            .HasParameter("actor_name", p => { }); // Match the parameter in the stored function
        
        
        // ********** // **********
        // SQL Stored functions realted to 'Titles'
        // ********** // **********
        // **********
        // Map SQL function for top-rated movies
        // **********
        modelBuilder.Entity<MovieRankingWithDetails>().HasNoKey();
        modelBuilder
            .HasDbFunction(() => GetTopRatedMovies(default))
            .HasName("get_top_weighted_movies_with_details")
            .HasSchema("public")
            .HasParameter("titleType", ParameterBuilder => { });
        
        // **********
        // Map SQL function for Get Similar Titles
        // **********
        modelBuilder.Entity<SimilarMovie>().HasNoKey();
        modelBuilder
            .HasDbFunction(() => GetSimilarMovies(default))
            .HasName("find_similar_movies_by_genre")
            .HasSchema("public")
            .HasParameter("tconst", parameterBuilder => { });
        
        // **********
        // Map SQL function for Get Bookmarks with more info
        // **********
        modelBuilder.Entity<BookmarksWithTitles>().HasNoKey();
        modelBuilder
            .HasDbFunction(() => GetBookmarksWithTitles(default))
            .HasName("get_user_bookmarks_with_titles")
            .HasSchema("public")
            .HasParameter("user_id", parameterBuilder => { });
        
        // **********
        // Map SQL function for top 5 movies by genre
        // **********
        modelBuilder.Entity<MovieRankingByGenre>().HasNoKey();
        modelBuilder
            .HasDbFunction(() => GetMovieRankingByGenre(default))
            .HasName("get_top_5_highest_avg_ratings")
            .HasSchema("public")
            .HasParameter("genre_param", ParameterBuilder => { });

        // **********
        // Map SQL function for get top 20 rated movies
        // **********
        modelBuilder
            .HasDbFunction(() => GetTopRatedMoviesSub(default))
            .HasName("get_top_20_rated_movies")
            .HasSchema("public")
            .HasParameter("search_text", ParameterBuilder => { });

        // **********
        // Calling our mapping of DB tables, defined below
        // **********
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
        MapUserBookmarks(modelBuilder);
    }

    private static void MapActorRating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActorRating>().ToTable("actor_rating").HasKey(x => x.NConst);
        modelBuilder.Entity<ActorRating>().Property(x => x.NConst).HasColumnName("nconst");
        modelBuilder.Entity<ActorRating>().Property(x => x.ARating).HasColumnName("arating");
        
        modelBuilder.Entity<ActorRating>()
            .HasOne(tr => tr.NameBasic)
            .WithOne(nb => nb.ActorRating)
            .HasForeignKey<ActorRating>(tr => tr.NConst)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static void MapKnownForTitle(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KnownForTitle>().ToTable("known_for_title").HasKey(k => new {k.KnownForTitleId});
        modelBuilder.Entity<KnownForTitle>().Property(x => x.KnownForTitleId).HasColumnName("knownfortitle_id");
        modelBuilder.Entity<KnownForTitle>().Property(x => x.TConst).HasColumnName("tconst");
        modelBuilder.Entity<KnownForTitle>().Property(x => x.NConst).HasColumnName("nconst");
        
        modelBuilder.Entity<KnownForTitle>()
            .HasOne(p => p.NameBasic)
            .WithMany(p => p.KnownForTitle)
            .HasForeignKey(p => p.NConst)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<KnownForTitle>()
            .HasOne(p => p.TitleBasic)
            .WithMany(p => p.KnownForTitle)
            .HasForeignKey(p => p.TConst)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static void MapNameBasics(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NameBasics>().ToTable("name_basics").HasKey(i => i.NConst);
        modelBuilder.Entity<NameBasics>().Property(x => x.NConst).HasColumnName("nconst");
        modelBuilder.Entity<NameBasics>().Property(x => x.PrimaryName).HasColumnName("primaryname");
        modelBuilder.Entity<NameBasics>().Property(x => x.BirthYear).HasColumnName("birthyear");
        modelBuilder.Entity<NameBasics>().Property(x => x.DeathYear).HasColumnName("deathyear");
        
        // Foreign Kyes
        modelBuilder.Entity<NameBasics>()
            .HasOne(nb => nb.ActorRating) // Navigation property in NameBasics
            .WithOne(ar => ar.NameBasic) // Navigation property in ActorRating
            .HasForeignKey<NameBasics>(ar => ar.NConst) // FK in ActorRating
            .OnDelete(DeleteBehavior.Cascade); // Reflect ON DELETE CASCADE
        
        modelBuilder.Entity<NameBasics>()
            .HasMany(nb => nb.KnownForTitle)
            .WithOne(kft => kft.NameBasic)
            .HasForeignKey(kft => kft.NConst)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<NameBasics>()
            .HasMany(nb => nb.PrimaryProfession)
            .WithOne(pp => pp.NameBasic)
            .HasForeignKey(pp => pp.NConst)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<NameBasics>()
            .HasMany(nb => nb.TitlePersonnel)
            .WithOne(tp => tp.NameBasic)
            .HasForeignKey(tp => tp.NConst)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<NameBasics>()
            .HasMany(nb => nb.TitlePrincipals)
            .WithOne(tp => tp.NameBasic)
            .HasForeignKey(tp => tp.NConst)
            .OnDelete(DeleteBehavior.Cascade);
    }
    private static void MapPrimaryProfession(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PrimaryProfession>().ToTable("primary_profession").HasKey(k => k.PrimaryProfessionId);
        modelBuilder.Entity<PrimaryProfession>().Property(x => x.PrimaryProfessionId).HasColumnName("primaryprofession_id");
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
            .HasOne(b => b.User)
            .WithMany(u => u.SearchHistory) // Nav property in Users (Check what its called there)
            .HasForeignKey(b => b.UserId)
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
        
        //FK User Bookmarks
        modelBuilder.Entity<TitleBasics>()
            .HasMany(u => u.UserBookmarks)
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
        modelBuilder.Entity<TitlePersonnel>().ToTable("title_personnel").HasKey(k => k.TitlePersonnelId);
        modelBuilder.Entity<TitlePersonnel>().Property(x => x.TitlePersonnelId).HasColumnName("titlepersonnel_id");
        modelBuilder.Entity<TitlePersonnel>().Property(x => x.TConst).HasColumnName("tconst");
        modelBuilder.Entity<TitlePersonnel>().Property(x => x.NConst).HasColumnName("nconst");
        modelBuilder.Entity<TitlePersonnel>().Property(x => x.Role).HasColumnName("role");
        
        modelBuilder.Entity<TitlePersonnel>()
            .HasOne(u => u.NameBasic)
            .WithMany(t => t.TitlePersonnel)
            .HasForeignKey(k => k.NConst)
            .OnDelete(DeleteBehavior.Cascade);
            
        modelBuilder.Entity<TitlePersonnel>()
            .HasOne(u => u.TitleBasic)
            .WithMany(t => t.TitlePersonnel)
            .HasForeignKey(k => k.TConst)
            .OnDelete(DeleteBehavior.Cascade);
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
        modelBuilder.Entity<Users>().Property(u => u.Username).HasColumnName("username").IsRequired();
        modelBuilder.Entity<Users>().Property(u => u.Password).HasColumnName("password").IsRequired();
        modelBuilder.Entity<Users>().Property(u => u.Salt).HasColumnName("salt").IsRequired();

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

        // Configure one-to-many relationship with UserBookmarks
        modelBuilder.Entity<Users>()
            .HasMany(u => u.UserBookmarks)      // A User has many UserBookmarks
            .WithOne(b => b.User)                  // Each UserBookmark references one User
            .HasForeignKey(b => b.UserId)          // Foreign key in UserBookmarks
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
            .HasOne(b => b.User)
            .WithMany(u => u.UserRatings)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public void MapUserBookmarks(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserBookmarks>().ToTable("user_bookmarkings").HasKey(u => u.UserBookmarksId);
        modelBuilder.Entity<UserBookmarks>().Property(x => x.UserId).HasColumnName("userid");
        modelBuilder.Entity<UserBookmarks>().Property(x => x.TConst).HasColumnName("tconst");
        modelBuilder.Entity<UserBookmarks>().Property(x => x.Note).HasColumnName("note");
        modelBuilder.Entity<UserBookmarks>().Property(x => x.UserBookmarksId).HasColumnName("userbookmarkings_id");
        
        // PK/FK and cascade behavior
        modelBuilder.Entity<UserBookmarks>()
            .HasOne(b => b.User)
            .WithMany(u => u.UserBookmarks)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }


}