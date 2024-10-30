using DataAccessLayer;

namespace BusinessLayer;

public interface IDataService
{
    public IList<TitleBasics> GetTitleBasicsList();

    public IList<User> GetUserList();
}