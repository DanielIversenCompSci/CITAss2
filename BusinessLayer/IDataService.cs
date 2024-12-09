using DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer;

public interface IDataService
{
    // The different entities af labelled like this for easier navigation:
    // ********************
    // EntityName
    // ********************



    // ********************
    //TitleBasics
    // ********************
    Task<IList<TitleBasics>> GetLimitedTitleBasicsAsync(int limit, int offset);

    public IList<TitleBasics> GetTitleBasicsList();
    TitleBasics GetTitleBasicsById(string tConst);
    TitleBasics AddTitleBasics(TitleBasics newTitle);
    bool UpdateTitleBasics(string tConst, TitleBasics updatedTitle);
    bool DeleteTitleBasics(string tConst);
    int GetTitleBasicsCount();
    // Gets a count of all TitleBasics entries
    Task<int> GetTitleBasicsCountAsync();



    // ********************
    //TitlePrincipals
    // ********************
    public IList<TitlePrincipals> GetTitlePrincipalsList();
    public TitlePrincipals GetTitlePrincipalsById(string nConst, string category);
    public TitlePrincipals AddTitlePrincipals(TitlePrincipals newTitle);
    public bool UpdateTitlePrincipals(string tConst, string category, TitlePrincipals updatedTitle);
    public bool DeleteTitlePrincipals(string tConst, string category);
    public int GetTitlePrincipalsCount();
    
    
    
    // ********************
    //TitleAkas
    // ********************
    public IList<TitleAkas> GetTitleAkasList();
    public TitleAkas GetTitleAkasById(string titleId, int category);
    public TitleAkas AddTitleAkas(TitleAkas newTitle);
    public bool UpdateTitleAkas(string titleId, int ordering, TitleAkas updatedTitle);
    public bool DeleteTitleAkas(string titleId, int ordering);
    int GetTitleAkasCount();
    
    
    
    // ********************
    //NameBasics
    // ********************
    public IList<NameBasics> GetNameBasicsList();
    public NameBasics GetNameBasicsById(string nConst);
    public NameBasics AddNameBasics(NameBasics newTitle);
    public bool UpdateNameBasics(string nConst, NameBasics updatedTitle);
    public bool DeleteNameBasics(string nConst);
    int GetNameBasicsCount();
    Task<IList<NameBasics>> GetLimitedNameBasicsAsync(int limit, int offset);
    Task<int> GetNameBasicsCountAsync();

    
    
    // ********************
    //Users
    // ********************
    IList<Users> GetUsersList();
    Users GetUserById(int userId);
    Users AddUser(Users newUser);
    bool UpdateUser(int userId, Users updatedUser);
    bool LoginUser(string username, string password);
    bool DeleteUser(int userId);
    Users GetUserWithSearchHistory(int userId);
    int GetUsersCount();

    
    
    // ********************
    //TitlePersonnel
    // ********************
    public IList<TitlePersonnel> GetTitlePersonnelList();
    TitlePersonnel GetTitlePersonnelById(int titlePersonnelId);
    TitlePersonnel AddTitlePersonnel(TitlePersonnel newTitle);
    bool UpdateTitlePersonnel(int titlePersonnelId, TitlePersonnel updatedTitle);
    bool DeleteTitlePersonnel(int titlePersonnelId);
    int GetTitlePersonnelCount();
    
    
    
    // ********************
    //KnownForTitle
    // ********************
    public IList<KnownForTitle> GetKnownForTitleList();
    public KnownForTitle GetKnownForTitleById(int knownForTitleId);
    public KnownForTitle AddKnownForTitle(KnownForTitle newTitle);
    public bool UpdateKnownForTitle(int knownForTitleId, KnownForTitle updatedTitle);
    public bool DeleteKnownForTitle(int knownForTitleId);
    int GetKnownForTitleCount();


    
    // ********************
    //PrimaryProfession
    // ********************
    public IList<PrimaryProfession> GetPrimaryProfessionList();
    public PrimaryProfession GetPrimaryProfessionById(int primaryProfessionId);
    public PrimaryProfession AddPrimaryProfession(PrimaryProfession newTitle);
    public bool UpdatePrimaryProfession(int primaryProfessionId, PrimaryProfession updatedTitle);
    public bool DeletePrimaryProfession(int primaryProfessionId);
    int GetPrimaryProfessionCount();
    
    
    
    // ********************
    //ActorRating
    // ********************
    IList<ActorRating> GetActorRatingList(int limit = 100);
    ActorRating GetActorRatingById(string nConst);
    ActorRating AddActorRating(ActorRating newActorRating);
    bool UpdateActorRating(string nConst, ActorRating updatedActorRating);
    bool DeleteActorRating(string nConst);
    int GetActorRatingCount();
    
    
    
    // ********************
    //TitleGenre
    // ********************
    IList<TitleGenre> GetTitleGenreList(int limit = 100);
    TitleGenre GetTitleGenreById(string tConst, string genre); 
    TitleGenre AddTitleGenre(TitleGenre newTitleGenre);
    bool UpdateTitleGenre(string tConst, string genre, TitleGenre updatedTitleGenre); 
    bool DeleteTitleGenre(string tConst, string genre);
    int GetTitleGenreCount();
    
    
    
    // ********************
    //TitleRatings
    // ********************
    IList<TitleRatings> GetTitleRatingsList(int limit = 100);
    TitleRatings GetTitleRatingById(string tConst);
    TitleRatings AddTitleRating(TitleRatings newTitleRating);
    bool UpdateTitleRating(string tConst, TitleRatings updatedTitleRating);
    bool DeleteTitleRating(string tConst);
    int GetTitleRatingsCount();
    
    
    
    // ********************
    //SearchHis
    // ********************
    public IList<SearchHis> GetSearchHisList();
    SearchHis GetSearchHistoryById(int userId, DateTime timestamp);
    SearchHis GetSearchHisById(int searchId); // Updated one
    SearchHis AddSearchHistory(SearchHis newSearch);
    bool UpdateSearchHistory(int userId, DateTime timestamp, SearchHis updatedSearch);
    bool UpdateSearchHis(int searchId, SearchHis updatedSearch);
    bool DeleteSearchHistory(int userId, DateTime timestamp);
    bool DeleteSearchHis(int searchId); // Updated one
    int GetSearchHisCount();
    
    
    
    // ********************
    //UserRating
    // ********************
    public IList<UserRating> GetUserRatingsList(); // be by id aswell ?
    UserRating GetUserRatingById(int userRatingId); // ITS RIGHT HERE
    UserRating AddUserRating(UserRating newUserRating);
    bool UpdateUserRating(int userRatingId, UserRating updatedRating);
    bool DeleteUserRating(int userRatingId);
    int GetUserRatingsCount();
    
    
    
    // ********************
    //UserBookmarkings
    // ********************
    IList<UserBookmarkings> GetUserBookmarkings();
    UserBookmarkings GetUserBookmarkingsById(int userBookmarkingsId);
    UserBookmarkings AddUserBookmarkings(UserBookmarkings newUserBookmarkings);
    bool UpdateUserBookmarkings(int userBookmarkingsId, UserBookmarkings updatedUserBookmarkings);
    bool DeleteUserBookmarkings(int userBookmarkingsId);
    int GetUserBookmarkingsCount();


    // ********************
    //MovieRankingWithDetails
    // ********************
    Task<IList<MovieRankingWithDetails>> GetRankedMoviesWithDetails(int limit, int minVotes);

}
