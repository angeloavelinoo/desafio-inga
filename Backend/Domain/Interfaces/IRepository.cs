using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        Task<IList<T>> GetItens();
        Task<IList<T>> GetItens(Expression<Func<T, bool>> filtro);
        Task<T> Get(Expression<Func<T, bool>> filtro);
        Task<bool> AlredyExist(Expression<Func<T, bool>> filtro);
        Task<T> Create(T obj);
        Task<T> Update(T obj);
        Task<T> Remove(T obj);
        Task<T> Delete(T obj);

    }
}
