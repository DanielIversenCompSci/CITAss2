using System;

class Program
{
    static void Main(string[] args)
    {
        // Empty Main method
    }
}

/*
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

//TestCreate(dataService);
//TestRead(dataService);
//TestUpdate(dataService);
//TestRead(dataService);  // Read again to confirm the update
//TestDelete(dataService);
//TestRead(dataService);  // Read again to confirm the deletion
//CreateDBCRUD();

// ___________

static void CreateDBCRUD()
{
    // Implementation commented out
}

static void PrintTitleBasicsList(IDataService dataService)
{
    // Implementation commented out
}

static void PrintTitlePrincipalsList(IDataService dataService)
{
    // Implementation commented out
}

static void PrintTitleAkasList(IDataService dataService)
{
    // Implementation commented out
}

static void PrintNameBasicsList(IDataService dataService)
{
    // Implementation commented out
}

static void PrintUsersList(IDataService dataService)
{
    // Implementation commented out
}

static void PrintTitlePersonnelList(IDataService dataService)
{
    // Implementation commented out
}

static void PrintKnownForTitleList(IDataService dataService)
{
    // Implementation commented out
}

static void PrintPrimaryProfessionList(IDataService dataService)
{
    // Implementation commented out
}

static void PrintActorRatingList(IDataService dataService)
{
    // Implementation commented out
}

static void PrintTitleGenreList(IDataService dataService)
{
    // Implementation commented out
}

static void PrintTitleRatingsList(IDataService dataService)
{
    // Implementation commented out
}

static void PrintSearchHisList(IDataService dataService)
{
    // Implementation commented out
}

static void PrintUserRatingsList(IDataService dataService)
{
    // Implementation commented out
}

static void TestCreate(IDataService dataService)
{
    // Implementation commented out
}

static void TestRead(IDataService dataService)
{
    // Implementation commented out
}

static void TestUpdate(IDataService dataService)
{
    // Implementation commented out
}

static void TestDelete(IDataService dataService)
{
    // Implementation commented out
}
*/
