using MedicineInventoryApp.Data;
using MedicineInventoryApp.Interfaces.Repositories;
using MedicineInventoryApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MedicineInventoryApp.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly InventoryContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(InventoryContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public void Update(T entity) => _dbSet.Update(entity);

        public void Delete(T entity) => _dbSet.Remove(entity);

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
            => await _dbSet.Where(predicate).ToListAsync();
    }
}
