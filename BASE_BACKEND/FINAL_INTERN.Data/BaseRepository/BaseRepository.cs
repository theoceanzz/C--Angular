using FINAL_INTERN.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FINAL_INTERN.Data.BaseRepository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {

        private readonly finalInternDbContext _context;
        //private readonly DbSet<T> _DbSet;
        public BaseRepository(finalInternDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetPagedAsync(int skip, int take)
        {
            return await _context.Set<T>()
                                 .Skip(skip)
                                 .Take(take)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            _context.Add<T>(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));


            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {

            var entity = await _context.Set<T>().FindAsync(id);

            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Entity with id {id} not found.");
            }
        }

        public async Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }


        public async Task<IEnumerable<T>> SortAsync(List<T> list, Expression<Func<T, object>> sortByProperty, bool asc)
        {
            var sortedList = asc ? list.AsQueryable().OrderBy(sortByProperty).ToList(): list.AsQueryable().OrderByDescending(sortByProperty).ToList();

            return sortedList;
        }
    }
}
