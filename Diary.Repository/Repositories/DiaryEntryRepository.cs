using Diary.Repository.Data;
using Diary.Repository.Entities;
using Diary.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Diary.Repository.Repositories
{
    public class DiaryEntryRepository : IDiaryEntryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private bool disposed = false;

        public DiaryEntryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<DiaryEntryEntity> GetAll()
        {
            return _dbContext.DiaryEntries;
        }

        public DiaryEntryEntity? GetById(int id)
        {
            return _dbContext.DiaryEntries.Find(id);
        }

        public void Create(DiaryEntryEntity entry)
        {
            _dbContext.DiaryEntries.Add(entry);
        }

        public void Update(DiaryEntryEntity entry)
        {
            _dbContext.Entry(entry).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var entry = GetById(id);
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry), "Diary Entry not found");
            }
            _dbContext.DiaryEntries.Remove(entry);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }


        // Implementing IDisposable pattern
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
                if (disposing)
                    _dbContext.Dispose();
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
