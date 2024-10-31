using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

public class ActorRatingContext : DbContext
{
    public void MapActorRating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActorRating>().ToTable("actor_rating").HasNoKey();
        modelBuilder.Entity<ActorRating>().Property(x => x.NConst).HasColumnName("nconst");
        modelBuilder.Entity<ActorRating>().Property(x => x.ARating).HasColumnName("arating");
    }
}