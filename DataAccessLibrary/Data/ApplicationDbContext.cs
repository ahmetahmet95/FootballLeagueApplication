using Microsoft.EntityFrameworkCore;
using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Teams> Teams { get; set; }
        public DbSet<TeamsRank> TeamsRank { get; set; }
        public DbSet<PlayedMatches> PlayedMatches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teams>()
                .ToTable(tb => tb.HasTrigger("TeamsUpdateTrigger"));

            modelBuilder.Entity<Teams>()
           .ToTable(tb => tb.HasTrigger("TeamsInsertTrigger"));
        }
    }
}
