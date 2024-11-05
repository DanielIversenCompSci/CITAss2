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

    // TitlePrincipals
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

    // TitleAkas
    public IList<TitleAkas> GetTitleAkasList()
    {
        return _context.TitleAkas.ToList();
    }

    // NameBasics
    public IList<NameBasics> GetNameBasicsList()
    {
        return _context.NameBasics.ToList();
    }

    // Users
    public IList<Users> GetUsersList()
    {
        return _context.Users.ToList();
    }

    // TitlePersonnel
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
    public IList<TitleRatings> GetTitleRatingsList()
    {
        return _context.TitleRatings.ToList();
    }

    // SearchHis
    public IList<SearchHis> GetSearchHisList()
    {
        return _context.SearchHis.ToList();
    }

    // UserRatings
    public IList<UserRating> GetUserRatingsList()
    {
        return _context.UserRating.ToList();
    }
}
