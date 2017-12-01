using LearningPlatform.Data.EntityFramework.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace LearningPlatform.Data.EntityFramework.BaseRepository
{
    internal sealed class GenericRepository<TEntity> where TEntity : class
    {
        private readonly SurveyContextProvider _contextProvider;

        public GenericRepository(SurveyContextProvider contextProvider)
        {
            _contextProvider = contextProvider;
        }

        private DbSet<TEntity> DbSet
        {
            get
            {
                return Context.Set<TEntity>();
            }
        }

        internal DatabaseContext.SurveyContext Context
        {
            get { return _contextProvider.Get(); }
        }

        /// <summary>
        /// Not to be exposed directly from repositories. We do not want to expose IQueryable outside of repositories.
        /// </summary>
        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter)
        {
            return DbSet.Where(filter);
        }

        /// <summary>
        /// Not to be exposed directly from repositories. We do not want to expose IQueryable outside of repositories.
        /// </summary>
        public IQueryable<TEntity> GetAll<TProperty>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TProperty>> includePath)
        {
            return DbSet.Include(includePath).Where(filter);
        }


        public TEntity GetById<TProperty>(Expression<Func<TEntity, bool>> idFilter, Expression<Func<TEntity, TProperty>> includePath)
        {
            return GetAll(idFilter, includePath).SingleOrDefault();
        }

        /// <summary>
        /// Will return a "shallow" object. Use a method with includePath to fetch deeper.
        /// </summary>
        public TEntity GetById(object id)
        {
            return DbSet.Find(id);
        }

        public void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void Remove(object id)
        {
            TEntity entityToDelete = DbSet.Find(id);
            Remove(entityToDelete);
        }

        public void Remove(TEntity entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            DbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (Context.Entry(entity).State == EntityState.Detached)
                {
                    DbSet.Attach(entity);
                }
            }
            DbSet.RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            DbSet.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }
    }
}
