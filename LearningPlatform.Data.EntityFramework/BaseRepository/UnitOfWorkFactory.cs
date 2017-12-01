using LearningPlatform.Data.EntityFramework.DatabaseContext;
using LearningPlatform.Domain.Common;

namespace LearningPlatform.Data.EntityFramework.BaseRepository
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly ContextManagement _contextManagement;

        public UnitOfWorkFactory(ContextManagement contextService)
        {
            _contextManagement = contextService;
        }

        public IUnitOfWork Create()
        {
            return new UnitOfWork(_contextManagement).Begin();
        }
    }
}
