using DataAccessLayer;


namespace BusinessLayer;

public interface IDataService
{
    //TitleBasics
    public IList<TitleBasics> GetTitleBasicsList();
    TitleBasics GetTitleBasicsById(string tConst);
    TitleBasics AddTitleBasics(TitleBasics newTitle);
    bool UpdateTitleBasics(string tConst, TitleBasics updatedTitle);
    bool DeleteTitleBasics(string tConst);

    //TitlePrincipals
    public IList<TitlePrincipals> GetTitlePrincipalsList();
    //TitleAkas
    public IList<TitleAkas> GetTitleAkasList();
    //NameBasics
    public IList<NameBasics> GetNameBasicsList();
    //Users
    public IList<Users> GetUsersList();
    //TitlePersonnel
    public IList<TitlePersonnel> GetTitlePersonnelList();
    //KnownForTitle
    public IList<KnownForTitle> GetKnownForTitleList();
    //PrimaryProfession
    public IList<PrimaryProfession> GetPrimaryProfessionList();
    //ActorRating
    public IList<ActorRating> GetActorRatingList();
    //TitleGenre
    public IList<TitleGenre> GetTitleGenreList();
    //TitleRatings
    public IList<TitleRatings> GetTitleRatingsList();
    //SearchHis
    public IList<SearchHis> GetSearchHisList();
    //UserRating
    public IList<UserRating> GetUserRatingsList(); // be by id aswell ?
    UserRating GetUserRatingById(string userId, string tConst);
    UserRating AddUserRating(UserRating newUserRating);
    bool UpdateUserRating(string userId, string tConst, UserRating updatedRating);
    bool DeleteUserRating(string userId, string tConst);
}