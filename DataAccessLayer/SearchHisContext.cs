using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;

namespace DataAccessLayer;

public class SearchHisContext
{
    public void MapSearchHis(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SearchHis>().ToTable("search_his").HasNoKey();
        modelBuilder.Entity<SearchHis>().Property(x => x.UserId).HasColumnName("userid");
        modelBuilder.Entity<SearchHis>().Property(x => x.SearchQuery).HasColumnName("searchquery");
        modelBuilder.Entity<SearchHis>().Property(x => x.SearchTimeStamp).HasColumnName("searchtimestamp");

    }
}