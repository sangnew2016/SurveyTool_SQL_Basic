using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Autofac;
using LearningPlatform.Data.EntityFramework.BaseRepository;
using LearningPlatform.Domain.SurveyDesign.Resources;

namespace LearningPlatform.Data.EntityFramework.Repository
{
    class ResourceStringRepository : IResourceStringRepository
    {
        private readonly GenericRepository<ResourceString> _genericRepository;
        private readonly IComponentContext _componentContext;

        public ResourceStringRepository(GenericRepository<ResourceString> genericRepository, IComponentContext componentContext)
        {
            _genericRepository = genericRepository;
            _componentContext = componentContext;
        }

        public void AddOrUpdate(ResourceString resourceString)
        {
            var resource = _componentContext.Resolve<ResourceString>();
            resource.Name = resourceString.Name;
            _genericRepository.Context.ResourceStrings.AddOrUpdate(r => new { r.Name, r.SurveyId }, resource);
            _genericRepository.Context.SaveChanges();

            foreach (var resourceStringItem in resourceString.Items)
            {
                resourceStringItem.ResourceStringId = resource.Id;
            }

            _genericRepository.Context.ResourceStringItems.AddOrUpdate(r => new { r.Language, r.ResourceStringId }, resourceString.Items.ToArray());
            _genericRepository.Context.SaveChanges();
        }


        public IList<ResourceString> GetByNameForSurvey(string name, long surveyId)
        {
            return _genericRepository.GetAll(p => p.Name == name && (p.SurveyId == surveyId || p.SurveyId == null), includePath => includePath.Items).ToList();
        }
    }
}
