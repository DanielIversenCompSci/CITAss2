using System.Collections;
using System.Runtime.CompilerServices;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer;

public class DataService : IDataService
{
    private readonly ImdbContext _context;

    // Constructor with dependency injection for ImdbContext
    public DataService(ImdbContext context)
    {
        _context = context;
    }

    // TitleBasics
    public IList<TitleBasics> GetTitleBasicsList()
    {
        return _context.TitleBasics.ToList();
    }

    public TitleBasics GetTitleBasicsById(string tConst)
    {
        return _context.TitleBasics.Find(tConst);
    }

    
    public TitleBasics AddTitleBasics(TitleBasics newTitle)
    {
        _context.TitleBasics.Add(newTitle);
        _context.SaveChanges();
        return newTitle;
    }

    
    public bool UpdateTitleBasics(string tConst, TitleBasics updatedTitle)
    {
        var existingTitle = _context.TitleBasics.Find(tConst);
        if (existingTitle == null)
        {
            return false; // Entry not found
        }


        // Update properties
        existingTitle.PrimaryTitle = updatedTitle.PrimaryTitle;
        existingTitle.OriginalTitle = updatedTitle.OriginalTitle;
        existingTitle.IsAdult = updatedTitle.IsAdult;
        existingTitle.StartYear = updatedTitle.StartYear;
        existingTitle.EndYear = updatedTitle.EndYear;
        existingTitle.RuntimeMinutes = updatedTitle.RuntimeMinutes;
        existingTitle.Plot = updatedTitle.Plot;
        existingTitle.Poster = updatedTitle.Poster;

        _context.SaveChanges();
        return true;
    }


    public bool DeleteTitleBasics(string tConst)
    {
        var title = _context.TitleBasics.Find(tConst);

        // Check if the entry exists
        if (title == null)
        {
            Console.WriteLine($"No TitleBasics entry found with TConst '{tConst}' to delete.");
            return false; // Entry not found, nothing to delete
        }

        _context.TitleBasics.Remove(title);
        _context.SaveChanges();
        return true;
    }

    // TitlePrincipals Done
    public IList<TitlePrincipals> GetTitlePrincipalsList()
    {
        return _context.TitlePrincipals.ToList();
    }
    
    public TitlePrincipals GetTitlePrincipalsById(string tConst)
    {
        return _context.TitlePrincipals.FirstOrDefault(tp => tp.TConst == tConst);
    }
    
    public TitlePrincipals AddTitlePrincipals(TitlePrincipals newTitle)
    {
        _context.TitlePrincipals.Add(newTitle);
        _context.SaveChanges();
        return newTitle;
    }
    
    public bool UpdateTitlePrincipals(string tConst, TitlePrincipals updatedTitle)
    {
        var existingTitle = _context.TitlePrincipals.FirstOrDefault(tp => tp.TConst == tConst);
        
        if (existingTitle == null)
        {
            return false; // Entry not found
        }

        // Update properties
        existingTitle.Ordering = updatedTitle.Ordering;
        existingTitle.Category = updatedTitle.Category;
        existingTitle.Job = updatedTitle.Job;
        existingTitle.Characters = updatedTitle.Characters;
        

        _context.SaveChanges();
        return true;
    }
    
    public bool DeleteTitlePrincipals(string tConst)
    {
        var title = _context.TitlePrincipals.FirstOrDefault(tp => tp.TConst == tConst);

        // Check if the entry exists
        if (title == null)
        {
            Console.WriteLine($"No TitlePrincipals entry found with TConst '{tConst}' to delete.");
            return false; // Entry not found, nothing to delete
        }

        _context.TitlePrincipals.Remove(title);
        _context.SaveChanges();
        return true;
    }

    // TitleAkas done
    public IList<TitleAkas> GetTitleAkasList()
    {
        return _context.TitleAkas.ToList();
    }

    // NameBasics done
    public IList<NameBasics> GetNameBasicsList()
    {
        return _context.NameBasics.ToList();
    }
    
    public NameBasics GetNameBasicsById(string nConst)
    {
        return _context.NameBasics.FirstOrDefault(tp => tp.Nconst == nConst);
    }

    public NameBasics AddNameBasics(NameBasics newTitle)
    {
        _context.NameBasics.Add(newTitle);
        _context.SaveChanges();
        return newTitle;
    }
    
    public bool UpdateNameBasics(string nConst, NameBasics updatedTitle)
    {
        var existingTitle = _context.NameBasics.FirstOrDefault(tp => tp.Nconst == nConst);
        
        if (existingTitle == null)
        {
            return false; // Entry not found
        }

        // Update properties
        existingTitle.PrimaryName = updatedTitle.PrimaryName;
        existingTitle.BirthYear = updatedTitle.BirthYear;
        existingTitle.DeathYear = updatedTitle.DeathYear;
        

        _context.SaveChanges();
        return true;
    }
    
    public bool DeleteNameBasics(string nConst)
    {
        var title = _context.NameBasics.FirstOrDefault(tp => tp.Nconst == nConst);

        // Check if the entry exists
        if (title == null)
        {
            Console.WriteLine($"No NameBasics entry found with NConst '{nConst}' to delete.");
            return false; // Entry not found, nothing to delete
        }

        _context.NameBasics.Remove(title);
        _context.SaveChanges();
        return true;
    }
    
    // Users done
    public IList<Users> GetUsersList()
    {
        return _context.Users.ToList();
    }

    // TitlePersonnel Done ?
    public IList<TitlePersonnel> GetTitlePersonnelList()
    {
        return _context.TitlePersonnel.ToList();
    }

    // KnownForTitle
    public IList<KnownForTitle> GetKnownForTitleList()
    {
        return _context.KnownForTitle.ToList();
    }

    public KnownForTitle GetKnownForTitleById(string nConst, string tConst)
    {
        return _context.KnownForTitle.FirstOrDefault(k => k.TConst == tConst && k.NConst == nConst);
    }

    public KnownForTitle AddKnownForTitle(KnownForTitle newTitle)
    {
        _context.KnownForTitle.Add(newTitle);
        _context.SaveChanges();
        return newTitle;
    }

    public bool UpdateKnownForTitle(string tConst, string nConst, KnownForTitle updatedTitle) 
    {
        var existingTitle = _context.KnownForTitle.FirstOrDefault(k => k.TConst == tConst && k.NConst == nConst);
        if (existingTitle == null) 
        {
            return false;
        }

        existingTitle.TConst = updatedTitle.TConst;
        existingTitle.NConst = updatedTitle.NConst;

        _context.SaveChanges();
        return true;
    }

    public bool DeleteKnownForTitle(string tConst, string nConst)
    {
        var title = _context.KnownForTitle.FirstOrDefault(k => k.TConst == tConst && k.NConst == nConst);
        if (title == null)
        {
            Console.WriteLine($"No KnownForTitle entry found to delete.");
            return false;
        }
        _context.KnownForTitle.Remove (title);
        _context.SaveChanges();
        return true;
    }

    // PrimaryProfession
    public IList<PrimaryProfession> GetPrimaryProfessionList()
    {
        return _context.PrimaryProfession.ToList();
    }

    // ActorRating
    
    public IList<ActorRating> GetActorRatingList(int limit = 100)
    {
        return _context.ActorRating.Take(limit).ToList();
    }

    
    public ActorRating GetActorRatingById(string nConst)
    {
        return _context.ActorRating.FirstOrDefault(a => a.NConst == nConst);
    }

    
    public ActorRating AddActorRating(ActorRating newActorRating)
    {
        _context.ActorRating.Add(newActorRating);
        _context.SaveChanges();
        return newActorRating;
    }

    
    public bool UpdateActorRating(string nConst, ActorRating updatedActorRating)
    {
        var existingActorRating = _context.ActorRating.Find(nConst);
        if (existingActorRating == null)
            return false;

        existingActorRating.ARating = updatedActorRating.ARating; 
        _context.SaveChanges();
        return true;
    }

    
    public bool DeleteActorRating(string nConst)
    {
        var actorRating = _context.ActorRating.Find(nConst);
        if (actorRating == null)
            return false;

        _context.ActorRating.Remove(actorRating);
        _context.SaveChanges();
        return true;
    }

    // TitleGenre

    public IList<TitleGenre> GetTitleGenreList(int limit = 100)
    {
        return _context.TitleGenre.Take(limit).ToList();
    }


    // Maybe this should insted return a list of genre for a given tconst or have a metohde for it
    public TitleGenre GetTitleGenreById(string tConst, string genre)
    {
        return _context.TitleGenre.FirstOrDefault(t => t.TConst == tConst && t.Genre == genre);
    }

    
    public TitleGenre AddTitleGenre(TitleGenre newTitleGenre)
    {
        _context.TitleGenre.Add(newTitleGenre);
        _context.SaveChanges();
        return newTitleGenre;
    }

    
    public bool UpdateTitleGenre(string tConst, string genre, TitleGenre updatedTitleGenre)
    {
        var existingTitleGenre = _context.TitleGenre.FirstOrDefault(t => t.TConst == tConst && t.Genre == genre);

        if (existingTitleGenre == null)
        {
            return false;
        }

        existingTitleGenre.Genre = updatedTitleGenre.Genre;

        _context.SaveChanges();
        return true;
    }

    
    public bool DeleteTitleGenre(string tConst, string genre)
    {
        var titleGenre = _context.TitleGenre.FirstOrDefault(t => t.TConst == tConst && t.Genre == genre);

        if (titleGenre == null)
        {
            return false;
        }

        _context.TitleGenre.Remove(titleGenre);
        _context.SaveChanges();
        return true;
    }


    // TitleRatings
    public IList<TitleRatings> GetTitleRatingsList(int limit = 100)
    {
        return _context.TitleRatings.Take(limit).ToList();
    }

    public TitleRatings GetTitleRatingById(string tConst)
    {
        return _context.TitleRatings.FirstOrDefault(tr => tr.TConst == tConst);
    }

    public TitleRatings AddTitleRating(TitleRatings newTitleRating)
    {
        _context.TitleRatings.Add(newTitleRating);
        _context.SaveChanges();
        return newTitleRating;
    }

    public bool UpdateTitleRating(string tConst, TitleRatings updatedTitleRating)
    {
        var existingRating = _context.TitleRatings.FirstOrDefault(tr => tr.TConst == tConst);

        if (existingRating == null)
        {
            return false;
        }

        existingRating.AverageRating = updatedTitleRating.AverageRating;
        existingRating.NumVotes = updatedTitleRating.NumVotes;
        _context.SaveChanges();
        return true;
    }

    public bool DeleteTitleRating(string tConst)
    {
        var existingRating = _context.TitleRatings.FirstOrDefault(tr => tr.TConst == tConst);

        if (existingRating == null)
        {
            return false;
        }

        _context.TitleRatings.Remove(existingRating);
        _context.SaveChanges();
        return true;
    }


// SearchHis
public IList<SearchHis> GetSearchHisList()
    {
        return _context.SearchHis.ToList();
    }
    
    public SearchHis GetSearchHistoryById(string userId, DateTime timestamp)
    {
        return _context.SearchHis.FirstOrDefault(s => s.UserId == userId && s.SearchTimeStamp == timestamp);
    }

    
    public SearchHis AddSearchHistory(SearchHis newSearch)
    {
        _context.SearchHis.Add(newSearch);
        _context.SaveChanges();
        return newSearch;
    }

    
    public bool UpdateSearchHistory(string userId, DateTime timestamp, SearchHis updatedSearch)
    {
        var existingSearch = _context.SearchHis.FirstOrDefault(s => s.UserId == userId && s.SearchTimeStamp == timestamp);

        if (existingSearch == null)
        {
            return false;
        }

        existingSearch.SearchQuery = updatedSearch.SearchQuery;
        existingSearch.SearchTimeStamp = updatedSearch.SearchTimeStamp;
        _context.SaveChanges();
        return true;
    }

    
    public bool DeleteSearchHistory(string userId, DateTime timestamp)
    {
        var existingSearch = _context.SearchHis.FirstOrDefault(s => s.UserId == userId && s.SearchTimeStamp == timestamp);

        if (existingSearch == null)
        {
            return false;
        }

        _context.SearchHis.Remove(existingSearch);
        _context.SaveChanges();
        return true;
    }

    // UserRatings
    public IList<UserRating> GetUserRatingsList()
    {
        return _context.UserRating.ToList();
    }
    
    public UserRating GetUserRatingById(string userId, string tConst)
    {
        return _context.UserRating.FirstOrDefault(r => r.UserId == userId && r.TConst == tConst);
    }

    public UserRating AddUserRating(UserRating newUserRating)
    {
        if (_context.UserRating.Any(r => r.UserId == newUserRating.UserId && r.TConst == newUserRating.TConst))
        {
            return null; // User rating for this movie already exists
        }

        _context.UserRating.Add(newUserRating);
        _context.SaveChanges();
        return newUserRating;
    }


    public bool UpdateUserRating(string userId, string tConst, UserRating updatedRating)
    {
        var existingRating = _context.UserRating.FirstOrDefault(r => r.UserId == userId && r.TConst == tConst);

        if (existingRating == null)
        {
            return false; // No existing rating found
        }

        existingRating.Rating = updatedRating.Rating;
        _context.SaveChanges();
        return true;
    }



    public bool DeleteUserRating(string userId, string tConst)
    {
        var existingRating = _context.UserRating.FirstOrDefault(r => r.UserId == userId && r.TConst == tConst);

        if (existingRating == null)
        {
            return false; // No existing rating found
        }

        _context.UserRating.Remove(existingRating);
        _context.SaveChanges();
        return true;
    }

}
