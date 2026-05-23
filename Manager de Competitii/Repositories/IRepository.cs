using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manager_de_Competitii.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id); // Assuming entities have Id (or string for name if needed)
        Task AddAsync(T entity);
        Task UpdateAsync(int id, T entity);
        Task DeleteAsync(int id);
        Task SaveChangesAsync(List<T> entities); // Expose saving bulk list if necessary
    }
}
