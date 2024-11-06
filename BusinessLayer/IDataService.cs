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

    public TitlePrincipals GetTitlePrincipalsById(string tConst);

    public TitlePrincipals AddTitlePrincipals(TitlePrincipals newTitle);
    public bool UpdateTitlePrincipals(string tConst, TitlePrincipals updatedTitle);
    public bool DeleteTitlePrincipals(string tConst);
    
    //TitleAkas
    public IList<TitleAkas> GetTitleAkasList();
    public TitleAkas GetTitleAkasById(string tConst);
    public TitleAkas AddTitleAkas(TitleAkas newTitle);
    public bool UpdateTitleAkas(string tConst, TitleAkas updatedTitle);
    public bool DeleteTitleAkas(string tConst);
    
    //NameBasics
    public IList<NameBasics> GetNameBasicsList();
    public NameBasics GetNameBasicsById(string nConst);
    public NameBasics AddNameBasics(NameBasics newTitle);
    public bool UpdateNameBasics(string nConst, NameBasics updatedTitle);
    public bool DeleteNameBasics(string nConst);

    //Users
    IList<Users> GetUsersList();
    Users GetUserById(string userId);
    Users AddUser(Users newUser);
    bool UpdateUser(string userId, Users updatedUser);
    bool DeleteUser(string userId);
    Users GetUserWithSearchHistory(string userId);

    //TitlePersonnel
    public IList<TitlePersonnel> GetTitlePersonnelList();
    TitlePersonnel GetTitlePersonnelById(string tConst);
    TitlePersonnel AddTitlePersonnel(TitlePersonnel newTitle);
    bool UpdateTitlePersonnel(string tConst, TitlePersonnel updatedTitle);
    bool DeleteTitlePersonnel(string tConst);
    
    //KnownForTitle
    public IList<KnownForTitle> GetKnownForTitleList();
    public KnownForTitle GetKnownForTitleById(string nConst, string tConst);
    public KnownForTitle AddKnownForTitle(KnownForTitle newTitle);
    public bool UpdateKnownForTitle(string tConst, string nConst, KnownForTitle updatedTitle);
    public bool DeleteKnownForTitle(string tConst, string nConst);


    //PrimaryProfession
    public IList<PrimaryProfession> GetPrimaryProfessionList();
    public PrimaryProfession GetPrimaryProfessionById(string nConst);
    public PrimaryProfession AddPrimaryProfession(PrimaryProfession newTitle);
    public bool UpdatePrimaryProfession(string nConst, PrimaryProfession updatedTitle);
    public bool DeletePrimaryProfession(string nConst);
    
    //ActorRating
    IList<ActorRating> GetActorRatingList(int limit = 100);
    ActorRating GetActorRatingById(string nConst);
    ActorRating AddActorRating(ActorRating newActorRating);
    bool UpdateActorRating(string nConst, ActorRating updatedActorRating);
    bool DeleteActorRating(string nConst);
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
    UserRating GetUserRatingById(string userId, string tConst); // ITS RIGHT HERE
    UserRating AddUserRating(UserRating newUserRating);
    bool UpdateUserRating(string userId, string tConst, UserRating updatedRating);
    bool DeleteUserRating(string userId, string tConst);
}