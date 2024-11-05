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
    public TitlePrincipals AddTitlePrincipals(TitlePrincipals newTitle);

    public bool UpdateTitlePrincipals(string tConst, TitlePrincipals updatedTitle);
    public bool DeleteTitlePrincipals(string tConst);
    
    
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
    public IList<UserRating> GetUserRatingsList();

    public TitlePrincipals GetTitlePrincipalsById(string tConst);
}