using BusinessLayer;
using DataAccessLayer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

// Set up Dependency Injection
var serviceProvider = new ServiceCollection()
    .AddDbContext<ImdbContext>(options =>
        options.UseNpgsql("host=cit.ruc.dk;db=cit04;uid=cit04;pwd=1paLIXo0SHSs"))
    .AddScoped<IDataService, DataService>()
    .BuildServiceProvider();

// Get an instance of IDataService (DataService) from the service provider
var dataService = serviceProvider.GetService<IDataService>();


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
//PrintUserRatingsList(dataService);

TestCreate(dataService);
TestRead(dataService);
TestUpdate(dataService);
TestRead(dataService);  // Read again to confirm the update
TestDelete(dataService);
TestRead(dataService);  // Read again to confirm the deletion

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

const string testTConst = "tt99999990";  // This will be the TConst used for testing

static void TestCreate(IDataService dataService)
{
    Console.WriteLine("Creating a new TitleBasics entry...");

    var newTitle = new TitleBasics
    {
        TConst = testTConst,
        PrimaryTitle = "New Movie Title",
        OriginalTitle = "Original Movie Title",
        IsAdult = false,
        StartYear = "2024",
        EndYear = null,
        RuntimeMinutes = 120,
        Plot = "A brand new movie for testing.",
        Poster = "poster_url"
    };

    var createdTitle = dataService.AddTitleBasics(newTitle);

    if (createdTitle != null)
    {
        Console.WriteLine($"Created Title ID: {createdTitle.TConst}, Title: {createdTitle.PrimaryTitle}");
    }
    else
    {
        Console.WriteLine("Create operation failed.");
    }
}

static void TestRead(IDataService dataService)
{
    Console.WriteLine("Reading the TitleBasics entry...");

    var titleById = dataService.GetTitleBasicsById(testTConst);
    if (titleById != null)
    {
        Console.WriteLine($"Found Title ID: {titleById.TConst}, Title: {titleById.PrimaryTitle}");
    }
    else
    {
        Console.WriteLine("Title not found.");
    }
}

static void TestUpdate(IDataService dataService)
{
    Console.WriteLine("Updating the TitleBasics entry...");

    var updatedTitle = new TitleBasics
    {
        PrimaryTitle = "Updated Movie Title",
        OriginalTitle = "Updated Original Title",
        IsAdult = true,
        StartYear = "2025",
        EndYear = null,
        RuntimeMinutes = 130,
        Plot = "An updated plot description.",
        Poster = "updated_poster_url"
    };

    bool updateSuccess = dataService.UpdateTitleBasics(testTConst, updatedTitle);

    if (updateSuccess)
    {
        var updated = dataService.GetTitleBasicsById(testTConst);
        Console.WriteLine($"Updated Title ID: {updated.TConst}, New Title: {updated.PrimaryTitle}");
    }
    else
    {
        Console.WriteLine("Update failed; title not found.");
    }
}

static void TestDelete(IDataService dataService)
{
    Console.WriteLine("Deleting the TitleBasics entry...");

    bool deleteSuccess = dataService.DeleteTitleBasics(testTConst);
    Console.WriteLine(deleteSuccess ? "Delete successful." : "Delete failed; title not found.");
}
