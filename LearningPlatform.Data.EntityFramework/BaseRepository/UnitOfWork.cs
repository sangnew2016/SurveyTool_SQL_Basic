using LearningPlatform.Data.EntityFramework.DatabaseContext;
using LearningPlatform.Domain.Common;
using System;
using System.Data.Entity;

namespace LearningPlatform.Data.EntityFramework.BaseRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ContextManagement _contextManagement;

        private DbContextTransaction Transaction { get; set; }

        public UnitOfWork(ContextManagement contextManagement)
        {
            _contextManagement = contextManagement;
        }

        public int SavePoint()
        {
            return _contextManagement.SaveChanges();
        }

        public IUnitOfWork Begin()
        {
            if (Transaction != null) throw new InvalidOperationException("Transaction already started");
            Transaction = _contextManagement.BeginTransaction();
            return this;
        }

        public void Commit()
        {
            if (Transaction != null)
            {
                SavePoint();
                Transaction.Commit();
                Transaction = null;
            }
        }

        public void Rollback()
        {
            if (Transaction != null)
            {
                Transaction.Rollback();
                Transaction = null;
            }
        }


        public void Dispose()
        {
            Rollback();
        }
    }
}
