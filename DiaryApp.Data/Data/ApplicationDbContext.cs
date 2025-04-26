using Diary.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace Diary.Repository.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<DiaryEntryEntity> DiaryEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seeding Data
            modelBuilder.Entity<DiaryEntryEntity>().HasData(
                new DiaryEntryEntity
                {
                    Id = 1,
                    Title = "Went Hiking",
                    Content = "Went hiking with Joe!",
                    Created = new DateTime(2025, 4, 18)
                },
                new DiaryEntryEntity
                {
                    Id = 2,
                    Title = "Went Shopping",
                    Content = "Went shopping with Joe!",
                    Created = new DateTime(2025, 4, 19)
                },
                new DiaryEntryEntity
                {
                    Id = 3,
                    Title = "Went Diving",
                    Content = "Went diving with Joe!",
                    Created = new DateTime(2025, 4, 20)
                }
            ); 
        }
    }
}
