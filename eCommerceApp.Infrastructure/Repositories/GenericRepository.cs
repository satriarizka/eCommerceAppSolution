using Microsoft.EntityFrameworkCore;
using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Infrastructure.Data;
using System.Linq.Expressions;

namespace eCommerceApp.Infrastructure.Repositories
{
    public class GenericRepository<TEntity>(AppDbContext context) : IGeneric<TEntity> where TEntity : class
    {
        private readonly AppDbContext _context = context;
        private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

        public async Task<int> AddAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                return 0;

            _dbSet.Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            return await _context.SaveChangesAsync();
        }

        // Tambahan untuk eager loading
        public async Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(Guid id, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
        }
    }
}
