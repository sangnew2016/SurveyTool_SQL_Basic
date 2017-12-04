using LearningPlatform.Data.EntityFramework.BaseRepository;
using LearningPlatform.Domain.SurveyDesign.LangageStrings;
using System.Collections.Generic;
using LearningPlatform.Data.EntityFramework.DatabaseContext;

namespace LearningPlatform.Data.EntityFramework.Repository
{
    class LanguageStringRepository : ILanguageStringRepository
    {
        private readonly GenericRepository<LanguageString> _genericRepository;

        public LanguageStringRepository(GenericRepository<LanguageString> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        private SurveyContext Context { get { return _genericRepository.Context; } }

        public void Add(LanguageString languageString)
        {
            _genericRepository.Add(languageString);
        }
        public void Update(LanguageString languageString)
        {
            _genericRepository.Update(languageString);
        }

        public void Delete(long id)
        {
            _genericRepository.Remove(id);
        }

        public void Delete(IEnumerable<LanguageString> languages)
        {
            Context.LanguageStrings.RemoveRange(languages);
        }
    }
}
