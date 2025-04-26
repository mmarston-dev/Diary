using Diary.Business.Interfaces;
using Diary.Business.Models;
using Diary.Repository.Entities;
using Diary.Repository.Interfaces;

namespace Diary.Business.Services
{
    public class DiaryEntryService : IDiaryEntryService
    {
        private readonly IDiaryEntryRepository _diaryEntryRepository;
        public DiaryEntryService(IDiaryEntryRepository diaryEntryRepository)
        {
            _diaryEntryRepository = diaryEntryRepository;
        }
        public void CreateDiaryEntry(DiaryEntryModel entry)
        {
            DiaryEntryEntity entryEntity = MapToEntity(entry);
            _diaryEntryRepository.Create(entryEntity);
            _diaryEntryRepository.SaveChanges();
        }
        public IEnumerable<DiaryEntryModel> GetAllDiaryEntries()
        {

            var entryEntities = _diaryEntryRepository.GetAll();
            return entryEntities.Select(entryEntity => MapToModel(entryEntity)).ToList();
        }
        public DiaryEntryModel? GetDiaryEntryById(int id)
        {
            var entryEntity = _diaryEntryRepository.GetById(id);
            return entryEntity != null ? MapToModel(entryEntity) : null;
        }
        public void UpdateDiaryEntry(DiaryEntryModel entry)
        {
            DiaryEntryEntity entryEntity = MapToEntity(entry);
            _diaryEntryRepository.Update(entryEntity);
            _diaryEntryRepository.SaveChanges();
        }
        public void DeleteDiaryEntry(int id)
        {
            _diaryEntryRepository.Delete(id);
            _diaryEntryRepository.SaveChanges();
        }

        private DiaryEntryEntity MapToEntity(DiaryEntryModel entry)
        {
            return new DiaryEntryEntity
            {
                Id = entry.Id,
                Title = entry.Title,
                Content = entry.Content,
                Created = entry.Created
            };
        }

        private DiaryEntryModel MapToModel(DiaryEntryEntity entry)
        {
            return new DiaryEntryModel
            {
                Id = entry.Id,
                Title = entry.Title,
                Content = entry.Content,
                Created = entry.Created
            };
        }
    }
}
