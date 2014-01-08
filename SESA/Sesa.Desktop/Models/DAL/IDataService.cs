using System;
using System.Data.Objects.DataClasses;

namespace Sesa.Desktop.Models
{
    public interface IDataService<TEntity>
     where TEntity : System.Data.Objects.DataClasses.EntityObject, IBaseModel, new()
    {
        void Delete(Guid id);
        void Delete(TEntity entityToDelete);
        void Delete(EntityObject entityToDelete);
        System.Collections.Generic.IEnumerable<TEntity> Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter = null, Func<System.Linq.IQueryable<TEntity>, System.Linq.IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        TEntity GetByID(Guid id);
        void Insert(TEntity entity);
        void Save();
        void SyncContext(string key);

    }
}
