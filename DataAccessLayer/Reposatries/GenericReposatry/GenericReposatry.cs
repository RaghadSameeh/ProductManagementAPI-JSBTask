using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Reposatries.GenericReposatry
{
    public class GenericReposatry <T> : IGenericReposatry<T> where T : class
    {
        private readonly Context context;

        public GenericReposatry(Context context)
        {
            this.context = context;
        }
        public List<T> GetAll(params System.Linq.Expressions.Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = context.Set<T>();
            if (includeProperties != null)
            {
                foreach (var item in includeProperties)
                {
                    query = query.Include(item);
                }
            }
            return query.ToList();
        }

        public T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public T GetById(string id)
        {
            return context.Set<T>().Find(id);
        }

        public void insert(T Entity)
        {
            context.Set<T>().Add(Entity);
        }
        public void update(T Entity)
        {
            context.Entry(Entity).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            T Entity = GetById(id);
            context.Set<T>().Remove(Entity);
        }

        public void Delete(string id)
        {
            T Entity = GetById(id);
            context.Set<T>().Remove(Entity);
        }

        public int save()
        {
            return context.SaveChanges();

        }
    }
}

