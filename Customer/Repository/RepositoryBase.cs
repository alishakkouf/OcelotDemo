using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Customer.Repository
{
    public class RepositoryBase<T> : ControllerBase, IRepository<T> where T : class
    {
        protected readonly CustomerDbContext _context;
        protected DbSet<T> dbSet;

        public RepositoryBase(CustomerDbContext context)
        {
            dbSet = context.Set<T>();
            _context = context;
        }

        //Get Request
        public async Task<T> Get(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }
        //Create Request
        public async Task<T> Create(T entity)
        {
            await dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        //Update Request
        public async Task Update(T entity)
        {
            dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        //Delete Request
        public async Task Delete(T entity)
        {
            dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
