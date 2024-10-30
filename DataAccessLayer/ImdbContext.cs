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
    
    public DbSet<TitlePrincipals> TitlePrincipals { get; set; }
    
    public DbSet<TitleAkas> TitleAkas { get; set; }
    
    public DbSet<NameBasics> NameBasics { get; set; }
    
    public DbSet<Users> Users { get; set; }
    
    public DbSet<TitlePersonnel> TitlePersonnel { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        optionsBuilder.UseNpgsql("host=cit.ruc.dk;db=cit04;uid=cit04;pwd=1paLIXo0SHSs");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // title_baics
        TitleBasicsContext titleBasicsContext = new TitleBasicsContext();
        titleBasicsContext.MapTitleBasics(modelBuilder);
        
        TitlePrincipalsContext titlePrincipalsContext = new TitlePrincipalsContext();
        titlePrincipalsContext.MapTitlePrincipals(modelBuilder);
        
        TitleAkasContext titleAkasContext = new TitleAkasContext();
        titleAkasContext.MapTitleAkas(modelBuilder);
        
        NameBasicsContext nameBasicsContext = new NameBasicsContext();
        nameBasicsContext.MapNameBasics(modelBuilder);
        
        UsersContext usersContext = new UsersContext();
        usersContext.MapUsers(modelBuilder);
        
        TitlePersonnelContext titlePersonnelContext = new TitlePersonnelContext();
        titlePersonnelContext.MapTitlePersonnel(modelBuilder);
    }
    
}