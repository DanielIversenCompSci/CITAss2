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

    public IList<User> GetUserList()
    {
        var db = new ImdbContext();
        return db.Users.ToList();
    }

    public IList<NameBasics> GetNameBasicsList()
    {
        var db = new ImdbContext();
        return db.NameBasics.ToList();
    }

}