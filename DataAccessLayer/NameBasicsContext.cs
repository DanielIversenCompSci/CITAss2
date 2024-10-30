using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class NameBasicsContext
    {
        public void MapNameBasics(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NameBasics>().ToTable("name_basics").HasKey(i => i.Nconst);
            modelBuilder.Entity<NameBasics>().Property(x => x.Nconst).HasColumnName("nconst");
            modelBuilder.Entity<NameBasics>().Property(x => x.PrimaryName).HasColumnName("primaryName");
            modelBuilder.Entity<NameBasics>().Property(x => x.BirthYear).HasColumnName("birthYear");
            modelBuilder.Entity<NameBasics>().Property(x => x.DeathYear).HasColumnName("deathYear");

        }
    }
}
