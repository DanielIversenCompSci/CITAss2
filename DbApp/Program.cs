using BusinessLayer;
using DataAccessLayer;

var dataService = new DataService();

// PRINT TESTS
// ___________

//PrintTitleBasicsList(dataService);

//PrintTitlePrincipalsList(dataService);

//PrintTitleAkasList(dataService);

//PrintNameBasicsList(dataService);

//PrintUsersList(dataService);

PrintTitlePersonnelList(dataService);

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