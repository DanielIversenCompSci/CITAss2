using DataLayer;
using Microsoft.EntityFrameworkCore;


namespace PostgresDataService;

public class DataService : IDataService
{
    public IList<TitleBasics> GetTitleBasicsList()
    {
        var db = new ImdbContext();
        return db.TitleBasics.ToList();
    }
}