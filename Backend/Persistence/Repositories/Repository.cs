using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class Repository<T>(ApplicationDbContext context) : IRepository<T> where T : Entity
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<IList<T>> GetItens()
            =>  await _context.Set<T>().Where(x => x.DeletedAt == null).ToListAsync();

        public async Task<IList<T>> GetItens(Expression<Func<T, bool>> filtro)
            => await _context.Set<T>().Where(filtro).ToListAsync();


        public async Task<T> Get(Expression<Func<T, bool>> filtro)
            => await _context.Set<T>().Where(filtro).FirstOrDefaultAsync();


        public async Task<bool> AlredyExist(Expression<Func<T, bool>> filtro)
            =>  await _context.Set<T>().AnyAsync(filtro);

        public async Task<T> Create(T obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<T> Update(T obj)
        {
            _context.Update(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<T> Remove(T obj)
        {
            obj.DeletedAt = DateTime.Now;
            _context.Update(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task<T> Delete(T obj)
        {
            _context.Remove(obj);
            await _context.SaveChangesAsync();
            return obj;
        }
    }
}
