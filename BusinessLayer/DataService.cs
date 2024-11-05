using System.Collections;
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

    // PrimaryProfession
    public IList<PrimaryProfession> GetPrimaryProfessionList()
    {
        return _context.PrimaryProfession.ToList();
    }

    // ActorRating
    public IList<ActorRating> GetActorRatingList()
    {
        return _context.ActorRating.ToList();
    }

    // TitleGenre
    public IList<TitleGenre> GetTitleGenreList()
    {
        return _context.TitleGenre.ToList();
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
