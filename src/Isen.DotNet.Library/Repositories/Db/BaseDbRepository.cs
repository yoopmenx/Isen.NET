using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Isen.DotNet.Library.Context;
using Isen.DotNet.Library.Models;
using Isen.DotNet.Library.Repositories.Interfaces;

namespace Isen.DotNet.Library.Repositories.Db
{
    public abstract class BaseDbRepository<T> : IBaseRepository<T>
        where T : BaseModel<T>
    {
        // Contexte Entity Framework
        protected readonly ApplicationDbContext DbContext;
        // Contexte virtualisé (commun à Db et à InMemory)
        public IQueryable<T> Context =>
            DbContext.Set<T>().AsQueryable();

        // Constructeur IoC (injection de dépendances)
        public BaseDbRepository(
            ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public void SaveChanges() => DbContext.SaveChanges();

        // TODO : factoriser avec BaseInMemoryRepository
        public T Single(int id) => 
            Includes(Context).SingleOrDefault(c => c.Id == id);
        public T Single(string name) => 
            Includes(Context).SingleOrDefault(c => c.Name.Equals(name));

        public IEnumerable<T> GetAll() => Includes(Context);    
        public IEnumerable<T> Find(Func<T, bool> predicate) => 
            Includes(Context).AsEnumerable().Where(predicate);

        public void Update(T entity)
        {
            if (entity.IsNew) DbContext.Add(entity);
            else DbContext.Update(entity);
        }

        public void Delete(int id)
        {
            var entity = Single(id);
            if (entity != null) Delete(entity);
        }

        public void Delete(T entity) => DbContext.Remove(entity);

        public virtual IQueryable<T> Includes(IQueryable<T> includes)
            => includes;
    }
}
