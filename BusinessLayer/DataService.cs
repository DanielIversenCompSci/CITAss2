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

    // TitlePrincipals
    public IList<TitlePrincipals> GetTitlePrincipalsList()
    {
        return _context.TitlePrincipals.ToList();
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
