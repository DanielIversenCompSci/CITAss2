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
}