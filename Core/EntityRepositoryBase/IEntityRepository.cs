using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.EntityRepositoryBase
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        void Add(T entity);
        void AddList(List<T> entities);
        void Delete(T entity);
        void DeleteList(List<T> entities);
        void Update(T entity);
        void UpdateList(List<T> entities);
        T Get(Expression<Func<T, bool>> filter);
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        Task<List<T>> Search(string term, string[] arguments, int result);
    }
}
