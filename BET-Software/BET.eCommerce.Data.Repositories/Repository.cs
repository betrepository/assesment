using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BET.eCommerce.Data.repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly Repositories.BET context;

        private DbSet<T> entities;      
        public Repository(Repositories.BET context)
        {
            this.context = context;
            entities = context.Set<T>();
        }    
        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }
        public T Get(long id)
        {
             return entities.SingleOrDefault(s => 1 == id);
        }
        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }
        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.SaveChanges();
        }
        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            entities.Remove(entity);
            context.SaveChanges();
        }
    }
}
