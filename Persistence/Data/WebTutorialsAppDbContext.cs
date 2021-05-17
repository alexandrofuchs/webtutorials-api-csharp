using Microsoft.EntityFrameworkCore;
using WebTutorialsApp.Domain.Entities;
using WebTutorialsApp.Persistence.Data.Maps;

namespace WebTutorialsApp.Persistence.Data
{
    public class WebTutorialsAppDbContext : DbContext
    {
        #region ENTITIES
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Video> Videos { get; set; }
        #endregion ENTITIES

        #region OVERRIDES METHODS DBCONTEXT
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new SectionMap());
            modelBuilder.ApplyConfiguration(new VideoMap());
        }
        #endregion OVERRIDES METHODS DBCONTEXT

        #region DATABASE CONTEXT
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS; Database=WebTutorials-DB;Trusted_Connection=True;MultipleActiveResultSets=true"); 
        }
        #endregion DATABASE CONTEXT
    }
}
