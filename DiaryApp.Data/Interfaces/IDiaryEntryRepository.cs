using Diary.Repository.Entities;

namespace Diary.Repository.Interfaces
{
    public interface IDiaryEntryRepository : IDisposable
    {
        void Create(DiaryEntryEntity entry);
        IEnumerable<DiaryEntryEntity> GetAll();
        DiaryEntryEntity? GetById(int id);
        void Update(DiaryEntryEntity entry);
        void Delete(int id);
        void SaveChanges();
    }
}
