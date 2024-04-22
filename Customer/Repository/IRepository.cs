namespace Customer.Repository
{
    public interface IRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> Get(int id);
        public Task<T> Create(T entity);
        public Task Update(T entity);
        public Task Delete(T entity);
    }
}
