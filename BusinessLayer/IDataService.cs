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

    public IList<KnownForTitle> GetKnownForTitleList();

    public IList<PrimaryProfession> GetPrimaryProfessionList();

    public IList<ActorRating> GetActorRatingList();

    public IList<TitleGenre> GetTitleGenreList();
}