using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Db context
namespace DataAccessLayer;
public class ImdbContext : DbContext
{
    public DbSet<TitleBasics> TitleBasics { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        optionsBuilder.UseNpgsql("host=cit.ruc.dk;db=cit04;uid=cit04;pwd=1paLIXo0SHSs");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // title_baics mapping from TitleBasicsContext.cs
        TitleBasicsContext titleBasicsContext = new TitleBasicsContext();
        titleBasicsContext.MapTitleBasics(modelBuilder);

        // User mapping from UserContext.cs
        UserContext userContext = new UserContext();
        userContext.MapUser(modelBuilder);
        
    }

    
}