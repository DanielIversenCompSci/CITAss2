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



    // The different entities af labelled like this for easier navigation:
    // ********************
    // EntityName
    // ********************



    // ********************
    // TitleBasics
    // ********************
    public async Task<IList<TitleBasics>> GetLimitedTitleBasicsAsync(int limit, int offset)
    {
        return await _context.TitleBasics
            .AsNoTracking() // Optimize for read-only queries
            .Skip(offset)   // Skip the specified number of records
            .Take(limit)    // Fetch only the required number of records
            .ToListAsync();
    }

    public async Task<int> GetTitleBasicsCountAsync()
    {
        return await _context.TitleBasics.CountAsync();
    }

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
        if (title == null) return false;

        _context.TitleBasics.Remove(title);
        _context.SaveChanges();
        return true;
    }

    public int GetTitleBasicsCount()
    {
        return _context.TitleBasics.Count();
    }
    
    
    
    // ********************
    // TitlePrincipals
    // ********************
    public IList<TitlePrincipals> GetTitlePrincipalsList()
    {
        return _context.TitlePrincipals.ToList();
    }
    
    public TitlePrincipals GetTitlePrincipalsById(string nConst, string category)
    {
        return _context.TitlePrincipals.FirstOrDefault(tp => tp.NConst == nConst && tp.Category == category);
    }
    
    public TitlePrincipals AddTitlePrincipals(TitlePrincipals newTitle)
    {
        _context.TitlePrincipals.Add(newTitle);
        _context.SaveChanges();
        return newTitle;
    }
    
    public bool UpdateTitlePrincipals(string tConst, string category, TitlePrincipals updatedTitle)
    {
        var existingTitle = _context.TitlePrincipals.FirstOrDefault(tp => tp.TConst == tConst && tp.Category == category);
        
        if (existingTitle == null)
        {
            return false; // Entry not found
        }

        // Update properties
        existingTitle.TConst = tConst;
        existingTitle.Ordering = updatedTitle.Ordering;
        existingTitle.NConst = category;
        existingTitle.Category = updatedTitle.Category;
        existingTitle.Job = updatedTitle.Job;
        existingTitle.Characters = updatedTitle.Characters;
        
        _context.SaveChanges();
        return true;
    }
    
    public bool DeleteTitlePrincipals(string tConst, string category)
    {
        var title = _context.TitlePrincipals.FirstOrDefault(tp => tp.TConst == tConst && tp.Category == category);

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
    
    public int GetTitlePrincipalsCount()
    {
        return _context.TitlePrincipals.Count();
    }

    
    
    // ********************
    // TitleAkas
    // ********************
    public IList<TitleAkas> GetTitleAkasList()
    {
        return _context.TitleAkas.ToList();
    }
    
    public TitleAkas GetTitleAkasById(string titleId, int ordering)
    {
        return _context.TitleAkas.Find(titleId, ordering);
    }
    
    public TitleAkas AddTitleAkas(TitleAkas newTitle)
    {
        _context.TitleAkas.Add(newTitle);
        _context.SaveChanges();
        return newTitle;
    }
    
    public bool UpdateTitleAkas(string titleId, int ordering, TitleAkas updatedTitle)
    {
        var existingTitle = _context.TitleAkas.Find(titleId, ordering);
        if (existingTitle == null)
        {
            return false; // Entry not found
        }
        
        // Update properties
        existingTitle.Ordering = updatedTitle.Ordering;
        existingTitle.Title = updatedTitle.Title;
        existingTitle.Region = updatedTitle.Region;
        existingTitle.Language = updatedTitle.Language;
        existingTitle.Types = updatedTitle.Types;
        existingTitle.Attributes = updatedTitle.Attributes;
        existingTitle.IsOriginalTitle = updatedTitle.IsOriginalTitle;
        
        _context.SaveChanges();
        return true;
    }
    
    public bool DeleteTitleAkas(string titleId, int ordering)
    {
        var title = _context.TitleAkas.Find(titleId, ordering);

        // Check if the entry exists
        if (title == null)
        {
            Console.WriteLine($"No TitleAkas entry found with Titleid '{titleId}' to delete.");
            return false; // Entry not found, nothing to delete
        }

        _context.TitleAkas.Remove(title);
        _context.SaveChanges();
        return true;
    }
    
    public int GetTitleAkasCount()
    {
        return _context.TitleAkas.Count();
    }

    
    
    // ********************
    // NameBasics
    // ********************
    public IList<NameBasics> GetNameBasicsList()
    {
        return _context.NameBasics.ToList();
    }
    
    public NameBasics GetNameBasicsById(string nConst)
    {
        return _context.NameBasics.FirstOrDefault(tp => tp.NConst == nConst);
    }

    public NameBasics AddNameBasics(NameBasics newTitle)
    {
        _context.NameBasics.Add(newTitle);
        _context.SaveChanges();
        return newTitle;
    }
    
    public bool UpdateNameBasics(string nConst, NameBasics updatedTitle)
    {
        var existingTitle = _context.NameBasics.FirstOrDefault(tp => tp.NConst == nConst);
        
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
        var title = _context.NameBasics.FirstOrDefault(tp => tp.NConst == nConst);

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
    
    public int GetNameBasicsCount()
    {
        return _context.NameBasics.Count();
    }
    
    
    
    // ********************
    // PrimaryProfession
    // ********************
    public IList<PrimaryProfession> GetPrimaryProfessionList()
    {
        return _context.PrimaryProfession.ToList();
    }
    
    public PrimaryProfession GetPrimaryProfessionById(string nConst)
    {
        return _context.PrimaryProfession.FirstOrDefault(tp => tp.NConst == nConst);
    }

    public PrimaryProfession AddPrimaryProfession(PrimaryProfession newTitle)
    {
        _context.PrimaryProfession.Add(newTitle);
        _context.SaveChanges();
        return newTitle;
    }
    
    public bool UpdatePrimaryProfession(string nConst, PrimaryProfession updatedTitle)
    {
        var existingTitle = _context.PrimaryProfession.FirstOrDefault(tp => tp.NConst == nConst);
        
        if (existingTitle == null)
        {
            return false; // Entry not found
        }

        // Update properties
        existingTitle.Role = updatedTitle.Role;

        _context.SaveChanges();
        return true;
    }
    
    public bool DeletePrimaryProfession(string nConst)
    {
        var title = _context.PrimaryProfession.FirstOrDefault(tp => tp.NConst == nConst);

        // Check if the entry exists
        if (title == null)
        {
            Console.WriteLine($"No PrimaryProfession entry found with NConst '{nConst}' to delete.");
            return false; // Entry not found, nothing to delete
        }

        _context.PrimaryProfession.Remove(title);
        _context.SaveChanges();
        return true;
    }
    
    public int GetPrimaryProfessionCount()
    {
        return _context.PrimaryProfession.Count();
    }

    
    
    // ********************
    // Users
    // ********************
    public IList<Users> GetUsersList()
    {
        return _context.Users.ToList();
    }

    
    public Users GetUserById(int userId)
    {
        return _context.Users.FirstOrDefault(u => u.UserId == userId);
    }
    
    public Users AddUser(Users newUser)
    {
        _context.Users.Add(newUser);
        _context.SaveChanges();
        return newUser;
    }
    
    public bool UpdateUser(int userId, Users updatedUser)
    {
        var user = _context.Users.Find(userId);
        if (user == null) return false;

        //user.UserId = updatedUser.UserId + "         ";
        user.Email = updatedUser.Email;
        user.Password = updatedUser.Password;

        _context.SaveChanges();
        return true;
    }
    
    public bool DeleteUser(int userId)
    {
        var user = _context.Users.Find(userId);
        if (user == null) return false;

        _context.Users.Remove(user);
        _context.SaveChanges();
        return true;
    }
    public Users GetUserWithSearchHistory(int userId)
    {
        return _context.Users.Include(u => u.SearchHistory).FirstOrDefault(u => u.UserId == userId);
    }
    
    
    public int GetUsersCount()
    {
        return _context.Users.Count();
    }

    
    
    // ********************
    // TitlePersonnel
    // ********************
    public IList<TitlePersonnel> GetTitlePersonnelList()
    {
        return _context.TitlePersonnel.ToList();
    }

    public TitlePersonnel GetTitlePersonnelById(string tConst)
    {
        return _context.TitlePersonnel.Find(tConst);
    }
    
    public TitlePersonnel AddTitlePersonnel(TitlePersonnel newTitle)
    {
        _context.TitlePersonnel.Add(newTitle);
        _context.SaveChanges();
        return newTitle;
    }
    
    public bool UpdateTitlePersonnel(string tConst, TitlePersonnel updatedTitle)
    {
        var existingTitle = _context.TitlePersonnel.Find(tConst);
        if (existingTitle == null)
        {
            return false; // Entry not found
        }
        
        // Update properties
        existingTitle.Role = updatedTitle.Role;

        _context.SaveChanges();
        return true;
    }
    
    public bool DeleteTitlePersonnel(string tConst)
    {
        var title = _context.TitlePersonnel.Find(tConst);

        // Check if the entry exists
        if (title == null)
        {
            Console.WriteLine($"No TitlePersonnel entry found with TConst '{tConst}' to delete.");
            return false; // Entry not found, nothing to delete
        }

        _context.TitlePersonnel.Remove(title);
        _context.SaveChanges();
        return true;
    }
    
    public int GetTitlePersonnelCount()
    {
        return _context.TitlePersonnel.Count();
    }

    
    
    // ********************
    // KnownForTitle
    // ********************
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
    
    public int GetKnownForTitleCount()
    {
        return _context.KnownForTitle.Count();
    }

    

    // ********************
    // ActorRating
    // ********************
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
    
    public int GetActorRatingCount()
    {
        return _context.ActorRating.Count();
    }

    
    
    // ********************
    // TitleGenre
    // ********************
    public IList<TitleGenre> GetTitleGenreList(int limit = 100)
    {
        return _context.TitleGenre.Take(limit).ToList();
    }
    
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
    
    public int GetTitleGenreCount()
    {
        return _context.TitleGenre.Count();
    }


    
    // ********************
    // TitleRatings
    // ********************
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
    
    public int GetTitleRatingsCount()
    {
        return _context.TitleRatings.Count();
    }


    
    // ********************
    // SearchHis
    // ********************
    public IList<SearchHis> GetSearchHisList()
    {
        return _context.SearchHis.ToList();
    }
    
    
    public SearchHis GetSearchHistoryById(int userId, DateTime timestamp)
    {
        return _context.SearchHis.FirstOrDefault(s => s.UserId == userId && s.SearchTimeStamp == timestamp);
    }
    

    public SearchHis GetSearchHisById(int searchId)
    {
        return _context.SearchHis.FirstOrDefault(s => s.SearchId == searchId);
    }
    
    public SearchHis AddSearchHistory(SearchHis newSearch)
    {
        _context.SearchHis.Add(newSearch);
        _context.SaveChanges();
        return newSearch;
    }
    
    
    public bool UpdateSearchHistory(int userId, DateTime timestamp, SearchHis updatedSearch)
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
    

    public bool UpdateSearchHis(int searchId, SearchHis updatedSearch)
    {
        var existingSearch = _context.SearchHis.FirstOrDefault(s => s.SearchId == searchId);

        if (existingSearch == null)
        {
            return false;
        }

        existingSearch.SearchQuery = updatedSearch.SearchQuery;
        //existingSearch.SearchTimeStamp = updatedSearch.SearchTimeStamp;
        _context.SaveChanges();
        return true;
    }
    
    
    public bool DeleteSearchHistory(int userId, DateTime timestamp)
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
    

    public bool DeleteSearchHis(int searchId)
    {
        var existingSearch = _context.SearchHis.FirstOrDefault(s => s.SearchId == searchId);

        if (existingSearch == null)
        {
            return false;
        }

        _context.SearchHis.Remove(existingSearch);
        _context.SaveChanges();
        return true;
    }
    
    public int GetSearchHisCount()
    {
        return _context.SearchHis.Count();
    }

    
    
    // ********************
    // UserRatings
    // ********************
    public IList<UserRating> GetUserRatingsList()
    {
        return _context.UserRating.ToList();
    }
    
    public UserRating GetUserRatingById(int userRatingId)
    {
        return _context.UserRating.FirstOrDefault(r => r.UserRatingId == userRatingId);
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
    
    public bool UpdateUserRating(int userRatingId, UserRating updatedRating)
    {
        var existingRating = _context.UserRating.FirstOrDefault(r => r.UserRatingId == userRatingId);

        if (existingRating == null)
        {
            return false; // No existing rating found
        }
        
        
        existingRating.UserId = updatedRating.UserId; //Foreign Key
        existingRating.TConst = updatedRating.TConst;
        existingRating.Rating = updatedRating.Rating;
        _context.SaveChanges();
        return true;
    }

    public bool DeleteUserRating(int userRatingId)
    {
        var existingRating = _context.UserRating.FirstOrDefault(r => r.UserRatingId == userRatingId);

        if (existingRating == null)
        {
            return false; // No existing rating found
        }

        _context.UserRating.Remove(existingRating);
        _context.SaveChanges();
        return true;
    }
    
    public int GetUserRatingsCount()
    {
        return _context.UserRating.Count();
    }
    
    
    
    // ********************
    // UserBookmarkings
    // ********************
    public IList<UserBookmarkings> GetUserBookmarkings()
    {
        return _context.UserBookmarkings.ToList(); //error 
    }
    
    public UserBookmarkings GetUserBookmarkingsById(int userBookmarkingsId)
    {
        return _context.UserBookmarkings.FirstOrDefault(r => r.UserBookmarkingsId == userBookmarkingsId);
    }

    public UserBookmarkings AddUserBookmarkings(UserBookmarkings newUserBookmarkings)
    {
        if (_context.UserBookmarkings.Any(r => r.UserId == newUserBookmarkings.UserId && r.TConst == newUserBookmarkings.TConst))
        {
            return null; // User bookmark for this movie already exists
        }

        _context.UserBookmarkings.Add(newUserBookmarkings);
        _context.SaveChanges();
        return newUserBookmarkings;
    }
    
    public bool UpdateUserBookmarkings(int userBookmarkingsId, UserBookmarkings updatedUserBookmarkings)
    {
        var existingUserBookmarkings = _context.UserBookmarkings.FirstOrDefault(r => r.UserBookmarkingsId == userBookmarkingsId);

        if (existingUserBookmarkings == null)
        {
            return false; // No existing rating found
        }
        
        
        existingUserBookmarkings.UserId = updatedUserBookmarkings.UserId; //Foreign Key
        existingUserBookmarkings.TConst = updatedUserBookmarkings.TConst;
        existingUserBookmarkings.Note = updatedUserBookmarkings.Note;
        _context.SaveChanges();
        return true;
    }

    public bool DeleteUserBookmarkings(int userBookmarkingsId)
    {
        var existingUserBookmarkings = _context.UserBookmarkings.FirstOrDefault(r => r.UserBookmarkingsId == userBookmarkingsId);

        if (existingUserBookmarkings == null)
        {
            return false; // No existing rating found
        }

        _context.UserBookmarkings.Remove(existingUserBookmarkings);
        _context.SaveChanges();
        return true;
    }
    
    public int GetUserBookmarkingsCount()
    {
        return _context.UserBookmarkings.Count();
    }
}
