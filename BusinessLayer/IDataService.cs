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
    public KnownForTitle GetKnownForTitleById(string nConst, string tConst);
    public KnownForTitle AddKnownForTitle(KnownForTitle newTitle);
    public bool UpdateKnownForTitle(string tConst, string nConst, KnownForTitle updatedTitle);
    public bool DeleteKnownForTitle(string tConst, string nConst);


    //PrimaryProfession
    public IList<PrimaryProfession> GetPrimaryProfessionList();
    //ActorRating
    public IList<ActorRating> GetActorRatingList();
    //TitleGenre
    IList<TitleGenre> GetTitleGenreList(int limit = 100);
    TitleGenre GetTitleGenreById(string tConst, string genre); 
    TitleGenre AddTitleGenre(TitleGenre newTitleGenre);
    bool UpdateTitleGenre(string tConst, string genre, TitleGenre updatedTitleGenre); 
    bool DeleteTitleGenre(string tConst, string genre); 
    //TitleRatings
    IList<TitleRatings> GetTitleRatingsList(int limit = 100);
    TitleRatings GetTitleRatingById(string tConst);
    TitleRatings AddTitleRating(TitleRatings newTitleRating);
    bool UpdateTitleRating(string tConst, TitleRatings updatedTitleRating);
    bool DeleteTitleRating(string tConst);
    //SearchHis
    public IList<SearchHis> GetSearchHisList();
    SearchHis GetSearchHistoryById(string userId, DateTime timestamp);
    SearchHis AddSearchHistory(SearchHis newSearch);
    bool UpdateSearchHistory(string userId, DateTime timestamp, SearchHis updatedSearch);
    bool DeleteSearchHistory(string userId, DateTime timestamp);
    //UserRating
    public IList<UserRating> GetUserRatingsList(); // be by id aswell ?
    UserRating GetUserRatingById(string userId, string tConst);
    UserRating AddUserRating(UserRating newUserRating);
    bool UpdateUserRating(string userId, string tConst, UserRating updatedRating);
    bool DeleteUserRating(string userId, string tConst);
}