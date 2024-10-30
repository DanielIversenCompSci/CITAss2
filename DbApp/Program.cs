using BusinessLayer;
using DataAccessLayer;

var dataService = new DataService();

// PRINT TESTS
// ___________

//PrintTitleBasicsList(dataService);

PrintTitlePrincipalsList(dataService);

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