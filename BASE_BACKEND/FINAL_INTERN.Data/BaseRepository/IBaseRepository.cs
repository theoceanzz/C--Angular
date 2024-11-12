using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FINAL_INTERN.Data.BaseRepository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetPagedAsync(int skip, int take);
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<T>> SortAsync(List<T> list, Expression<Func<T, object>> sortByProperty, bool asc);
    }
}
