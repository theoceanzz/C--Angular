using FINAL_INTERN.Data.BaseRepository;
using FINAL_INTERN.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINAL_INTERN.Business.BaseService
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly IBaseRepository<T> _baseRepository;
        // Constructor
        public BaseService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _baseRepository.GetAllAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _baseRepository.GetByIdAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _baseRepository.AddAsync(entity);
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            await _baseRepository.UpdateAsync(entity);
            return entity;
        }

        public async Task<T> DeleteAsync(int id)
        {
            var entity = await _baseRepository.GetByIdAsync(id);
            if (entity != null)
            {
                await _baseRepository.DeleteAsync(id);
            }
            return entity;
        }

        public async Task<IEnumerable<T>> GetPagedAsync(int pageNumber, int pageSize)
        {
            var skip = (pageNumber - 1) * pageSize;
            return await _baseRepository.GetPagedAsync(skip, pageSize);
        }

    }
}
