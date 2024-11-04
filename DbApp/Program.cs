using BusinessLayer;
using DataAccessLayer;

var dataService = new DataService();

// PRINT TESTS
// Un-comment whichever print function you want to use and run this project w dotnet run
// Specify TConst or NConst in the print functions themselves below
// ___________

//PrintTitleBasicsList(dataService);

//PrintTitlePrincipalsList(dataService);

//PrintTitleAkasList(dataService);

//PrintNameBasicsList(dataService);

//PrintUsersList(dataService);

//PrintTitlePersonnelList(dataService);

//PrintKnownForTitleList(dataService);

//PrintPrimaryProfessionList(dataService);

//PrintActorRatingList(dataService);

//PrintTitleGenreList(dataService);

//PrintTitleRatingsList(dataService);

//PrintSearchHisList(dataService);
PrintUserRatingsList(dataService);

// ___________

static void PrintTitleBasicsList(IDataService dataService)
{
    var filteredList = dataService.GetTitleBasicsList()
        .Where(i => i.TConst == "tt12836288");

    foreach (var i in filteredList)
    {
        Console.WriteLine(
            $"{i.TConst ?? ""}, " +
            $"{i.TitleType ?? ""}, " +
            $"{i.PrimaryTitle ?? ""}, " +
            $"{i.OriginalTitle ?? ""}, " +
            $"{(i.IsAdult ? "True" : "False")}, " +  // Convert bool to True/False, can also chose another string fx yes/no
            $"{i.StartYear ?? ""}, " +
            $"{i.EndYear ?? ""}, " +
            $"{(i.RuntimeMinutes > 0 ? i.RuntimeMinutes.ToString() : "")}, " +  // Empty if RuntimeMinutes is 0
            $"{i.Plot ?? ""}, " +
            $"{i.Poster ?? ""}"
        );
    }
}


static void PrintTitlePrincipalsList(IDataService dataService)
{
    var filteredList = dataService.GetTitlePrincipalsList()
        .Where(i => i.TConst == "tt11735202");

    foreach (var i in filteredList)
    {
        Console.WriteLine(
            $"{i.TConst ?? ""}, " +
            $"{i.Ordering.ToString() ?? ""}, " +
            $"{i.NConst ?? ""}, " +
            $"{i.Category ?? ""}, " +
            $"{(i.Job ?? "")}, " +  // Convert bool to True/False, can also chose another string fx yes/no
            $"{i.Characters ?? ""}"
        );
    }
}


static void PrintTitleAkasList(IDataService dataService)
{
    var filteredList = dataService.GetTitleAkasList()
        .Where(i => i.TitleId == "tt0078672 "); // TitleId in title_akas han an empty space by every end of its TCONSTS?????

    foreach (var i in filteredList)
    {
        Console.WriteLine(
            $"{i.TitleId ?? ""}, " +
            $"{i.Ordering.ToString() ?? ""}, " +
            $"{i.Title ?? ""}, " +
            $"{i.Region ?? ""}, " +
            $"{(i.Language ?? "")}, " +  // Convert bool to True/False, can also chose another string fx yes/no
            $"{i.Types ?? ""}, " +
            $"{i.Attributes ?? ""}, " +
            $"{i.IsOriginalTitle}"
        );
    }
}


static void PrintNameBasicsList(IDataService dataService)
{
    var filteredList = dataService.GetNameBasicsList()
        .Where(i => i.Nconst == "nm0062362 "); // TitleId in title_akas han an empty space by every end of its TCONSTS?????

    foreach (var i in filteredList)
    {
        Console.WriteLine(
            $"{i.Nconst ?? ""}, " +
            $"{i.PrimaryName ?? ""}, " +
            $"{i.BirthYear ?? ""}, " +
            $"{i.DeathYear ?? ""}"
        );
    }
}


static void PrintUsersList(IDataService dataService)
{
    var filteredList = dataService.GetUsersList()
        .Where(i => i.UserId == "1         "); // TitleId in title_akas han an empty space by every end of its TCONSTS?????

    foreach (var i in filteredList)
    {
        Console.WriteLine(
            $"{i.UserId ?? ""}, " +
            $"{i.Email ?? ""}, " +
            $"{i.Password ?? ""}"
        );
    }
}


static void PrintTitlePersonnelList(IDataService dataService)
{
    var filteredList = dataService.GetTitlePersonnelList()
        .Where(i => i.TConst == "tt17719278"); // TitleId in title_akas han an empty space by every end of its TCONSTS?????

    foreach (var i in filteredList)
    {
        Console.WriteLine(
            $"{i.TConst ?? ""}, " +
            $"{i.NConst ?? ""}, " +
            $"{i.Role ?? ""}"
        );
    }
}


static void PrintKnownForTitleList(IDataService dataService)
{
    var filteredList = dataService.GetKnownForTitleList()
        .Where(i => i.TConst == "tt1595607 "); // TitleId in title_akas han an empty space by every end of its TCONSTS?????

    foreach (var i in filteredList)
    {
        Console.WriteLine(
            $"{i.TConst ?? ""}, " +
            $"{i.NConst ?? ""}"
        );
    }
}


static void PrintPrimaryProfessionList(IDataService dataService)
{
    var filteredList = dataService.GetPrimaryProfessionList()
        .Where(i => i.NConst == "nm0047522 "); // TitleId in title_akas han an empty space by every end of its TCONSTS?????

    foreach (var i in filteredList)
    {
        Console.WriteLine(
            $"{i.NConst ?? ""}, " +
            $"{i.Role ?? ""}"
        );
    }
}


static void PrintActorRatingList(IDataService dataService)
{
    var filteredList = dataService.GetActorRatingList()
        .Where(i => i.NConst == "nm6073771 "); // TitleId in title_akas han an empty space by every end of its TCONSTS?????

    foreach (var i in filteredList)
    {
        Console.WriteLine(
            $"{i.NConst ?? ""}, " +
            $"{i.ARating}"
        );
    }
}


static void PrintTitleGenreList(IDataService dataService)
{
    var filteredList = dataService.GetTitleGenreList()
        .Where(i => i.TConst == "tt9126600 "); // TitleId in title_akas han an empty space by every end of its TCONSTS?????

    foreach (var i in filteredList)
    {
        Console.WriteLine(
            $"{i.TConst ?? ""}, " +
            $"{i.Genre}"
        );
    }
}


static void PrintTitleRatingsList(IDataService dataService)
{
    var filteredList = dataService.GetTitleRatingsList()
        .Where(i => i.TConst == "tt0052520 "); // TitleId in title_akas han an empty space by every end of its TCONSTS?????

    foreach (var i in filteredList)
    {
        Console.WriteLine(
            $"{i.TConst ?? ""}, " +
            $"{i.AverageRating}, " +
            $"{i.NumVotes}"
        );
    }
}

static void PrintSearchHisList(IDataService dataService)
{
    var filteredList = dataService.GetSearchHisList()
        .Where(i => i.UserId == "1         ");
      
    foreach (var i in filteredList)
    {
        Console.WriteLine(
            $"{i.UserId}, " +
            $"{i.SearchQuery}, " +
            $"{i.SearchTimeStamp}"
        );
    }
}

static void PrintUserRatingsList(IDataService dataService)
{
    var filteredList = dataService.GetUserRatingsList()
        .Where(i => i.UserId == "1         "); // TitleId in title_akas han an empty space by every end of its TCONSTS?????

    foreach (var i in filteredList)
    {
        Console.WriteLine(
            $"{i.UserId}," +
            $"{i.TConst}," +
            $"{i.Rating}"
         );
    }
}
