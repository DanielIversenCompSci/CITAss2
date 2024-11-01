using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;

namespace DataAccessLayer;

public class UserRatingContext : DbContext
{
    public void MapUserRating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserRating>().ToTable("userRating").HasNoKey();
        modelBuilder.Entity<UserRating>().Property(x => x.UserId).HasColumnName("userid");
        modelBuilder.Entity<UserRating>().Property(x => x.TConst).HasColumnName("tconst");
        modelBuilder.Entity<UserRating>().Property(x => x.Rating).HasColumnName("userrating");
        
    }
}