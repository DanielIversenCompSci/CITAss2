using BusinessLayer;
using DataAccessLayer;

var dataService = new DataService();

PrintTitleBasicsList(dataService);

static void PrintTitleBasicsList(IDataService dataService)
{
    foreach (var i in dataService.GetTitleBasicsList())
    {
        Console.WriteLine($"{i.TConst}, {i.TitleType}, {i.PrimaryTitle}");
    }
}