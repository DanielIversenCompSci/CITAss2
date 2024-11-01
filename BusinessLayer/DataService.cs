using System.Collections;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;


namespace BusinessLayer;

public class DataService : IDataService
{
    // Getters for all objects:
    public IList<TitleBasics> GetTitleBasicsList()
    {
        var db = new ImdbContext();
        return db.TitleBasics.ToList();
    }

    public IList<TitlePrincipals> GetTitlePrincipalsList()
    {
        var db = new ImdbContext();
        return db.TitlePrincipals.ToList();
    }

    public IList<TitleAkas> GetTitleAkasList()
    {
        var db = new ImdbContext();
        return db.TitleAkas.ToList();
    }

    public IList<NameBasics> GetNameBasicsList()
    {
        var db = new ImdbContext();
        return db.NameBasics.ToList();
    }

    public IList<Users> GetUsersList()
    {
        var db = new ImdbContext();
        return db.Users.ToList();
    }

    public IList<TitlePersonnel> GetTitlePersonnelList()
    {
        var db = new ImdbContext();
        return db.TitlePersonnel.ToList();
    }

    public IList<KnownForTitle> GetKnownForTitleList()
    {
        var db = new ImdbContext();
        return db.KnownForTitle.ToList();
    }

    public IList<PrimaryProfession> GetPrimaryProfessionList()
    {
        var db = new ImdbContext();
        return db.PrimaryProfession.ToList();
    }

    public IList<ActorRating> GetActorRatingList()
    {
        var db = new ImdbContext();
        return db.ActorRating.ToList();
    }

    public IList<TitleGenre> GetTitleGenreList()
    {
        var db = new ImdbContext();
        return db.TitleGenre.ToList();
    }

    public IList<TitleRatings> GetTitleRatingsList()
    {
        var db = new ImdbContext();
        return db.TitleRatings.ToList();
    }

    public IList<SearchHis> GetSearchHisList()
    {
        var db = new ImdbContext();
        return db.SearchHis.ToList();
    }

}