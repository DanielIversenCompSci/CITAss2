using BusinessLayer;
using DataAccessLayer;
// Dont work in rider
// git checkout -b you-branch-name

// git push -u origin your-branch-name

var dataService = new DataService();

PrintTitleBasicsList(dataService, "123");

static void PrintTitleBasicsList(IDataService dataService, string tconstValue)
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