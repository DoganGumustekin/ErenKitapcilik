using Core.Entities;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.EntityRepositoryBase.EfRepository
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void AddList(List<TEntity> entities)
        {
            using (TContext context = new TContext())
            {
                foreach (var entity in entities)
                {
                    var addedEntity = context.Entry(entity);
                    addedEntity.State = EntityState.Added;
                }

                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public void DeleteList(List<TEntity> entities)
        {
            using (TContext context = new TContext())
            {
                foreach (var entity in entities)
                {
                    var deletedEntity = context.Entry(entity);
                    deletedEntity.State = EntityState.Deleted;
                }
                context.SaveChanges();
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void UpdateList(List<TEntity> entities)
        {
            using (TContext context = new TContext())
            {
                foreach (var entity in entities)
                {
                    var updatedEntity = context.Entry(entity);
                    updatedEntity.State = EntityState.Modified;
                }
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                return filter == null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public async Task<List<TEntity>> Search(string term, string[] arguments ,int result)
        {
            if (string.IsNullOrWhiteSpace(term) || term.Length <= 3)
            {
                return new List<TEntity>();
            }

            using (TContext context = new TContext())
            {
                var query = context.Set<TEntity>().AsQueryable();

                var queryFilter = PredicateBuilder.New<TEntity>(false);

                foreach (var argument in arguments)
                {
                    queryFilter = queryFilter.Or(e => EF.Property<string>(e, argument).Contains(term));
                }

                query = query.Where(queryFilter);

                return await query
                     .Take(result)
                     .ToListAsync();
            }
        }
    }
}
