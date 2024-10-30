using DataAccessLayer;

namespace BusinessLayer;

public interface IDataService
{
    public IList<TitleBasics> GetTitleBasicsList();

    public IList<TitlePrincipals> GetTitlePrincipalsList();

    public IList<TitleAkas> GetTitleAkasList();

    public IList<NameBasics> GetNameBasicsList();

    public IList<Users> GetUsersList();

    public IList<TitlePersonnel> GetTitlePersonnelList();
}