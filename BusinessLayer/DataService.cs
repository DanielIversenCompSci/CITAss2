using System.Collections;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;


namespace BusinessLayer;

public class DataService : IDataService
{
    // Getters for all objects:

    //TitleBasics
    public IList<TitleBasics> GetTitleBasicsList()
    {
        var db = new ImdbContext();
        return db.TitleBasics.ToList();
    }
    //TitlePrincipals
    public IList<TitlePrincipals> GetTitlePrincipalsList()
    {
        var db = new ImdbContext();
        return db.TitlePrincipals.ToList();
    }
    //TitleAkas
    public IList<TitleAkas> GetTitleAkasList()
    {
        var db = new ImdbContext();
        return db.TitleAkas.ToList();
    }
    //NameBasics
    public IList<NameBasics> GetNameBasicsList()
    {
        var db = new ImdbContext();
        return db.NameBasics.ToList();
    }
    //Users
    public IList<Users> GetUsersList()
    {
        var db = new ImdbContext();
        return db.Users.ToList();
    }
    //TitlePersonnel
    public IList<TitlePersonnel> GetTitlePersonnelList()
    {
        var db = new ImdbContext();
        return db.TitlePersonnel.ToList();
    }
    //KnownForTitle
    public IList<KnownForTitle> GetKnownForTitleList()
    {
        var db = new ImdbContext();
        return db.KnownForTitle.ToList();
    }
    //PrimaryProfession
    public IList<PrimaryProfession> GetPrimaryProfessionList()
    {
        var db = new ImdbContext();
        return db.PrimaryProfession.ToList();
    }
    //ActorRating
    public IList<ActorRating> GetActorRatingList()
    {
        var db = new ImdbContext();
        return db.ActorRating.ToList();
    }
    //TitleGenre
    public IList<TitleGenre> GetTitleGenreList()
    {
        var db = new ImdbContext();
        return db.TitleGenre.ToList();
    }
    //TitleRatings
    public IList<TitleRatings> GetTitleRatingsList()
    {
        var db = new ImdbContext();
        return db.TitleRatings.ToList();
    }
    //SearchHis
    public IList<SearchHis> GetSearchHisList()
    {
        var db = new ImdbContext();
        return db.SearchHis.ToList();
    }
    //UserRatings
    public IList<UserRating> GetUserRatingsList()
    {
        var db = new ImdbContext();
        return db.UserRating.ToList();
    }
    
}