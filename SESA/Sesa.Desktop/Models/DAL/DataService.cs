using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Linq.Expressions;
using GalaSoft.MvvmLight.Ioc;

namespace Sesa.Desktop.Models
{
    public class DataService<TEntity> : Sesa.Desktop.Models.IDataService<TEntity> where TEntity : EntityObject, IBaseModel, new()
    {
        public DataService()
        {
            SyncContext(null);
        }

        public void SyncContext(string key)
        {
            DbContext = SimpleIoc.Default.GetInstance<SesaModelContainer>(key);
        }

        protected SesaModelContainer DbContext { get; private set; }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            var objectQuery = (ObjectQuery<TEntity>)DbContext.CreateObjectSet<TEntity>(typeof(TEntity).Name);

            foreach (var includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                objectQuery = objectQuery.Include(includeProperty);
            }

            var query = objectQuery.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            return query.AsEnumerable();
        }
        public virtual TEntity GetByID(Guid id)
        {
            return Get(p => p.Id == id).FirstOrDefault();
        }

        private static string GetEntitySetName()
        {
            return string.Format("{0}", typeof(TEntity).Name);
        }

        public virtual void Insert(TEntity entity)
        {
            if (entity.Id == Guid.Empty)
                entity.Id = Guid.NewGuid();
            DbContext.AddObject(GetEntitySetName(), entity);
        }

        public virtual void Delete(Guid id)
        {
            TEntity entityToDelete = GetByID(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (entityToDelete.EntityState == EntityState.Detached)
            {
                DbContext.Attach(entityToDelete);
            }
            DbContext.DeleteObject(entityToDelete);
        }

        public virtual void Delete(EntityObject entityToDelete)
        {
            if (entityToDelete.EntityState == EntityState.Detached)
            {
                DbContext.Attach(entityToDelete);
            }
            DbContext.DeleteObject(entityToDelete);
        }

        public virtual void Save()
        {
            var addedObjects = DbContext.ObjectStateManager.GetObjectStateEntries(EntityState.Added).Select(p => p.Entity).OfType<IBaseModel>();
            foreach (var material in addedObjects.Where(p => p != null && p.Id == Guid.Empty))
                material.Id = Guid.NewGuid();
            DbContext.SaveChanges();
        }
    }
}
