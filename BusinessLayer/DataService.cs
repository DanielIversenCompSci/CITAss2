using System.Collections;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;


namespace BusinessLayer;

public class DataService : IDataService
{
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
}