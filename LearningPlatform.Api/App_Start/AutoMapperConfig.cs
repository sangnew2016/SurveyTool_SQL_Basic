using AutoMapper;
using LearningPlatform.Domain.Mapping;

namespace LearningPlatform.Api.App_Start
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(config =>
            {
                config.AddProfile<DomainAutoMapperProfile>();
            });
        }
    }
}