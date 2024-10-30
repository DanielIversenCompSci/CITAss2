using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;

namespace DataAccessLayer;

public class UsersContext : DbContext
{
    public void MapUsers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Users>().ToTable("users").HasKey(i => i.UserId);
        modelBuilder.Entity<Users>().Property(x => x.UserId).HasColumnName("userid");
        modelBuilder.Entity<Users>().Property(x => x.Email).HasColumnName("email");
        modelBuilder.Entity<Users>().Property(x => x.Password).HasColumnName("password");
        
    }
}