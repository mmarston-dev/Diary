using Diary.Business.Models;

namespace Diary.Business.Interfaces
{
    public interface IDiaryEntryService
    {
        void CreateDiaryEntry(DiaryEntryModel entry);
        IEnumerable<DiaryEntryModel> GetAllDiaryEntries();
        DiaryEntryModel? GetDiaryEntryById(int id);
        void UpdateDiaryEntry(DiaryEntryModel entry);
        void DeleteDiaryEntry(int id);
    }
}
