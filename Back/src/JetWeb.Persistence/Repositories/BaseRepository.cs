using System.Threading.Tasks;
using JetWeb.Domain.Interfaces.Repositories;
using JetWeb.Persistence.Context;

namespace JetWeb.Persistence.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        private readonly JetWebContext _context;
        public BaseRepository(JetWebContext context)
        {
            _context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.AddAsync(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void DeleteRange<T>(T[] entityArray) where T : class
        {
            _context.RemoveRange(entityArray);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}