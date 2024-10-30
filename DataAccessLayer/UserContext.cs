using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;

namespace DataAccessLayer;

public class UserContext
{
    public void MapUser(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("users").HasKey(i => i.UserId);
        modelBuilder.Entity<User>().Property(x => x.UserId).HasColumnName("userid");
        modelBuilder.Entity<User>().Property(x => x.Email).HasColumnName("email");
        modelBuilder.Entity<User>().Property(x => x.Password).HasColumnName("password");
        
    }
}