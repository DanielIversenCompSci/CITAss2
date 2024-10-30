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
}